using Infrastructure.Core.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace KNQTT.Infrastructure.Configuration
{
    public class VaultConfiguration : IConfigurationSource
    {
        private bool _optional { get; set; }
        private IFileProvider _fileProvider { get; set; }
        private string _filePath { get; set; }


        public VaultConfiguration(string path, bool optional)
        {
            _optional = optional;
            _filePath = path;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            if (_optional == true && !File.Exists(_filePath))
            {
                CoreUtility.PushMessage("Đọc file thất bại " + Environment.MachineName);
                throw new Exception("Đã xảy ra lỗi khi đọc file Vault.");
            }
            else if (!string.IsNullOrWhiteSpace(_filePath) && File.Exists(_filePath))
            {
                _fileProvider = new PhysicalFileProvider(Path.GetDirectoryName(_filePath));
            }
            else
            {
                _fileProvider = null;
            }
            return new VaultConfigurationProvider(_fileProvider, _filePath);
        }
    }

    public class VaultConfigurationProvider : ConfigurationProvider, IDisposable
    {
        private string _filePath { get; set; }
        private readonly IDisposable? _changeTokenRegistration;
        private IFileProvider _fileProvider { get; set; }

        /// <summary>
        /// Đọc dữ liệu từ tệp tin cấu hình và cung cấp các giá trị cấu hình cho ứng dụng
        /// </summary>
        /// <param name="fileProvider"></param>
        /// <param name="filePath"></param>
        public VaultConfigurationProvider(IFileProvider fileProvider, string filePath)
        {
            _filePath = filePath;
            _fileProvider = fileProvider;
            if (fileProvider != null)
            {
                // Đăng ký một hàm callback sẽ được gọi khi tệp tin cấu hình thay đổi
                _changeTokenRegistration = ChangeToken.OnChange(
                      () => fileProvider.Watch(Path.GetFileName(filePath)),
                      () =>
                      {
                          Thread.Sleep(250);
                          LoadData();
                      });
            }
        }

        public override void Load()
        {
            if (_fileProvider != null)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            try
            {
                string content = string.Empty;
                using (StreamReader reader = new StreamReader(_filePath))
                {
                    content = reader.ReadToEnd();
                }
                if (content != null)
                {
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                    Data = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

                    foreach (var keyValuePair in dict)
                    {
                        if (keyValuePair.Value != null)
                        {
                            Data[keyValuePair.Key] = keyValuePair.Value.ToString();
                        }
                        else
                        {
                            Data[keyValuePair.Key] = null;
                        }
                    }
                    OnReload();
                    CoreUtility.PushMessage("Đọc file thành công " + Environment.MachineName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi đọc file Vault ");
            }
        }

        public void Dispose()
        {
            _changeTokenRegistration?.Dispose();
        }
    }

    public static class Extensions
    {
        public static IConfigurationBuilder AddVaultConfiguration(this IConfigurationBuilder builder, string path, bool optional = true)
        {
            return builder.Add(new VaultConfiguration(path, optional));
        }
    }
}
