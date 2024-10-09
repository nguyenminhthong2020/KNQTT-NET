using Newtonsoft.Json;
using System.Text;

namespace KNQTT.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static T DeepClone<T>(this T originalObject)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(originalObject));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex">Vị trí bắt đầu che</param>
        /// <param name="maskLength">Số ký tự cần che</param>
        /// <param name="maxthLength">Ký tự tối đa của chuỗi</param>
        /// <returns></returns>
        public static string MaskCharacter(this string str, int startIndex, int maskLength, int maxthLength)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    return str;
                }

                if (maxthLength > 0)
                {
                    if (str.Length != maxthLength)
                    {
                        return str;
                    }
                }

                if (str.Length < startIndex + maskLength)
                {
                    return new string('*', str.Length);
                }

                StringBuilder maskedPhoneNumber = new StringBuilder(str);
                for (int i = startIndex; i < startIndex + maskLength; i++)
                {
                    maskedPhoneNumber[i] = '*';
                }

                return maskedPhoneNumber.ToString();
            }
            catch
            {
                return str;
            }
        }

        /// <summary>
        /// Ẩn bớt thông tin trong chuỗi, từ trước dấu phẩy đầu tiên trở về trước
        /// - VD1: "123A, đường Nguyễn Văn A, HCM" --> "***, đường Nguyễn Văn A, HCM"
        /// - VD2: "Nhà không số ấp Tân Phong, xã Tân Hội" --> "***, xã Tân Hội"
        /// </summary>
        /// <param name="str"></param>
        /// <param name="mask"></param>
        /// <param name="maskLength"></param>
        /// <returns></returns>
        public static string MaskCharacterByComma(this string str, char mask = '*', int maskLength = 3)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";

            int commaIndex = str.IndexOf(','); // commaIndex >= -1
            if (commaIndex > 0)
            {
                string prefix = new string(mask, maskLength);
                string suffix = str.Substring(commaIndex);
                return prefix + suffix;
            }
            else
            {
                return str;
            }
        }

    }
}
