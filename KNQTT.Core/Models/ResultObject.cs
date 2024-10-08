using Infrastructure.Core.Enums;
using Infrastructure.Core.Utilities;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public class ResultObject<T>
    {
        private ResultCode _code;

        /// <summary>
        /// Constructor
        /// </summary>
        public ResultObject()
        {
            this._code = ResultCode.Ok;
            Message = new ResultMessage(this._code, PopupOption.Default);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ResultObject(ResultCode code, ResultMessage message, T data)
        {
            this.Data = new ResultData<T>();
            this._code = code;
            this.Message = message;
            this.Data.Result = data;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="pagination"></param>
        public ResultObject(ResultCode code, ResultMessage message, T data, ResultPagination pagination)
        {
            this.Data = new ResultData<T>();
            this._code = code;
            this.Message = message;
            this.Data.Result = data;
            this.Data.Pagination = pagination;
        }

        /// <summary>
        /// 
        /// </summary>
        public ResultCode Code
        {
            set
            {
                this._code = value;
            }
            get
            {
                if (this.Message != null)
                {
                    try
                    {
                        if (this.Message.Popup.Equals(PopupOption.Default))
                        {
                            switch ((int)this._code)
                            {
                                // system error
                                case -120:
                                    this.Message.Popup = PopupOption.Hide;
                                    break;
                                // warning confirm
                                case int n when (n >= -399 && n <= -350):
                                    this.Message.Popup = PopupOption.Show;
                                    break;
                                // warning without confirmation
                                case int n when (n >= -349 && n <= -300):
                                    this.Message.Popup = PopupOption.Show;
                                    break;
                                // error
                                case int n when (n >= -199 && n <= -100):
                                    this.Message.Popup = PopupOption.Show;
                                    break;
                                // success
                                case 200:
                                    this.Message.Popup = PopupOption.Hide;
                                    break;
                                default:
                                    break;
                            }
                            this.Message.Title = this.Message.Title.Equals(this._code.GetDescription()) ? this._code.GetDescription() : this.Message.Title;
                        }
                    }
                    catch
                    {
                        Message = new ResultMessage(ResultCode.ErrorException, PopupOption.Show);
                    }
                }
                return this._code;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ResultMessage Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ResultData<T> Data { set; get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultData<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public T Result { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public ResultPagination Pagination { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ResultMessage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="popup"></param>
        public ResultMessage(ResultCode resultCode = ResultCode.Ok, PopupOption popup = PopupOption.Default)
        {
            Title = resultCode.GetDescription();
            Message = resultCode.GetDescription();
            Popup = popup;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ExMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PopupOption Popup { get; set; }
    }

    public class ResultPagination
    {
        public ResultPagination(int currentPage, int pageSize = 20, bool isLastPage = false)
        {
            this.Current = currentPage;
            this.PageSize = pageSize;
            this.Next = isLastPage ? currentPage : currentPage + 1;
            this.Previous = currentPage > 1 ? currentPage - 1 : 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Next { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Previous { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NullDataType
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class ResultObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="errorMsg"></param>
        /// <param name="data"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        public static ResultObject<T> Fail<T>(
            string message = "Đã xảy ra lỗi trong quá trình xử lý.",
            string title = "",
            string errorMsg = "",
            ResultCode code = ResultCode.ErrorFail,
            T data = default,
            PopupOption popup = PopupOption.Default)
        {
            var msg = new ResultMessage()
            {
                Title = string.IsNullOrWhiteSpace(title)
                ? $"{code.GetDescription()}\n({CoreUtility.GetErrorCode("b")})"
                : $"{title}\n({CoreUtility.GetErrorCode("b")})",
                Message = message,
                Popup = popup,
                ExMessage = errorMsg
            };
            return new ResultObject<T>(code, msg, data);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exMessage"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        public static ResultObject<T> Error<T>(
            string exMessage,
            string message = "Đã xảy ra lỗi trong quá trình xử lý.",
            T data = default,
            PopupOption popup = PopupOption.Default)
        {
            var msg = new ResultMessage()
            {
                Title = $"{ResultCode.ErrorException.GetDescription()}\n({CoreUtility.GetErrorCode("b")})",
                Message = message,
                ExMessage = exMessage,
                Popup = popup
            };
            return new ResultObject<T>(ResultCode.ErrorException, msg, data);
        }

        /// <summary>
        /// Ok
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        public static ResultObject<T> Ok<T>(T data, string message = "Thành công.", PopupOption popup = PopupOption.Default, string title = "")
        {
            var msg = new ResultMessage()
            {
                Title = !string.IsNullOrWhiteSpace(title) ? title : ResultCode.Ok.GetDescription(),
                Message = message,
                Popup = popup
            };
            return new ResultObject<T>(ResultCode.Ok, msg, data);
        }

        /// <summary>
        /// Ok
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="pagination"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        public static ResultObject<T> Ok<T>(T data, ResultPagination pagination, string message = "Thành công.",
            PopupOption popup = PopupOption.Default)
        {
            var msg = new ResultMessage()
            {
                Title = ResultCode.Ok.GetDescription(),
                Message = message,
                Popup = popup
            };
            return new ResultObject<T>(ResultCode.Ok, msg, data, pagination);
        }

        /// <summary>
        /// Warning
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        public static ResultObject<T> Warning<T>(string message, T data = default, PopupOption popup = PopupOption.Default,
            ResultCode code = ResultCode.Warning)
        {
            var msg = new ResultMessage()
            {
                Title = $"{code.GetDescription()}\n({CoreUtility.GetErrorCode("b")})",
                Message = message,
                Popup = popup
            };
            return new ResultObject<T>(code, msg, data);
        }

        /// <summary>
        /// Fail
        /// </summary>
        /// <param name="message"></param>
        /// <param name="popup"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ResultObject<NullDataType> Fail(
            string message = "Đã xảy ra lỗi trong quá trình xử lý.",
            PopupOption popup = PopupOption.Default,
            ResultCode code = ResultCode.ErrorFail)
        {
            var msg = new ResultMessage()
            {
                Title = $"{code.GetDescription()}\n({CoreUtility.GetErrorCode("b")})",
                Message = message,
                Popup = popup
            };
            return new ResultObject<NullDataType>(code, msg, default);
        }

        /// <summary>
        /// Ok
        /// </summary>
        /// <param name="message"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        public static ResultObject<NullDataType> Ok(string message = "Thành công.",
            PopupOption popup = PopupOption.Default)
        {
            var msg = new ResultMessage()
            {
                Title = ResultCode.Ok.GetDescription(),
                Message = message,
                Popup = popup
            };
            return new ResultObject<NullDataType>(ResultCode.Ok, msg, default);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exMessage"></param>
        /// <param name="popup"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ResultObject<NullDataType> Error(string message,
            string exMessage,
            PopupOption popup = PopupOption.Default,
            ResultCode code = ResultCode.ErrorException)
        {
            var msg = new ResultMessage()
            {
                Title = $"{code.GetDescription()}\n({CoreUtility.GetErrorCode("b")})",
                Message = message,
                ExMessage = exMessage,
                Popup = popup
            };
            return new ResultObject<NullDataType>(code, msg, default);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exMessage"></param>
        /// <param name="errorId"></param>
        /// <param name="popup"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ResultObject<NullDataType> Error(string message,
            string exMessage,
            string errorId,
            PopupOption popup = PopupOption.Default,
            ResultCode code = ResultCode.ErrorException)
        {
            var msg = new ResultMessage()
            {
                Title = $"{code.GetDescription()}\n({errorId})",
                Message = message,
                ExMessage = exMessage,
                Popup = popup
            };
            return new ResultObject<NullDataType>(code, msg, default);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exMessage"></param>
        /// <param name="popup"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ResultObject<NullDataType> Warning(string message,
            PopupOption popup = PopupOption.Default,
            ResultCode code = ResultCode.Warning)
        {
            var msg = new ResultMessage()
            {
                Title = $"{code.GetDescription()}\n({CoreUtility.GetErrorCode("b")})",
                Message = message,
                Popup = popup
            };
            return new ResultObject<NullDataType>(code, msg, default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="errorMsg"></param>
        /// <param name="data"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        public static Task<ResultObject<T>> FailAsync<T>(
            string message = "Đã xảy ra lỗi trong quá trình xử lý.",
            string title = "",
            string errorMsg = "",
            ResultCode code = ResultCode.ErrorFail,
            T data = default,
            PopupOption popup = PopupOption.Default)
        {

            return Task.FromResult(Fail(message, title, errorMsg, code, data, popup));
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exMessage"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        public static Task<ResultObject<T>> ErrorAsync<T>(
            string exMessage,
            string message = "Đã xảy ra lỗi trong quá trình xử lý.",
            T data = default,
            PopupOption popup = PopupOption.Default)
        {
            return Task.FromResult(Error(exMessage, message, data, popup));
        }

        /// <summary>
        /// Ok
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        public static Task<ResultObject<T>> OkAsync<T>(T data,
            string message = "Thành công.",
            PopupOption popup = PopupOption.Default,
            string title = "")
        {
            return Task.FromResult(Ok(data, message, popup, title));
        }

        /// <summary>
        /// Ok
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="pagination"></param>
        /// <param name="message"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        public static Task<ResultObject<T>> OkAsync<T>(T data,
            ResultPagination pagination,
            string message = "Thành công.",
            PopupOption popup = PopupOption.Default)
        {
            return Task.FromResult(Ok(data, pagination, message, popup));
        }

        /// <summary>
        /// Warning
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        public static Task<ResultObject<T>> WarningAsync<T>(string message, T data = default,
            PopupOption popup = PopupOption.Default, ResultCode code = ResultCode.Warning)
        {
            return Task.FromResult(Warning(message, data, popup, code));
        }

        /// <summary>
        /// Fail
        /// </summary>
        /// <param name="message"></param>
        /// <param name="popup"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Task<ResultObject<NullDataType>> FailAsync(
            string message = "Đã xảy ra lỗi trong quá trình xử lý.",
            PopupOption popup = PopupOption.Default,
            ResultCode code = ResultCode.ErrorFail)
        {
            var msg = new ResultMessage()
            {
                Title = $"{code.GetDescription()}\n({CoreUtility.GetErrorCode("b")})",
                Message = message,
                Popup = popup
            };
            return Task.FromResult(new ResultObject<NullDataType>(code, msg, default));
        }

        /// <summary>
        /// Ok
        /// </summary>
        /// <param name="message"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        public static Task<ResultObject<NullDataType>> OkAsync(string message = "Thành công.",
            PopupOption popup = PopupOption.Default)
        {
            var msg = new ResultMessage()
            {
                Title = ResultCode.Ok.GetDescription(),
                Message = message,
                Popup = popup
            };
            return Task.FromResult(new ResultObject<NullDataType>(ResultCode.Ok, msg, default));
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exMessage"></param>
        /// <param name="popup"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Task<ResultObject<NullDataType>> ErrorAsync(string message, string exMessage,
            PopupOption popup = PopupOption.Default,
            ResultCode code = ResultCode.ErrorException)
        {
            var msg = new ResultMessage()
            {
                Title = $"{code.GetDescription()}\n({CoreUtility.GetErrorCode("b")})",
                Message = message,
                ExMessage = exMessage,
                Popup = popup
            };
            return Task.FromResult(new ResultObject<NullDataType>(code, msg, default));
        }

        /// <summary>
        /// WarningAsync
        /// </summary>
        /// <param name="message"></param>
        /// <param name="popup"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Task<ResultObject<NullDataType>> WarningAsync(string message,
            PopupOption popup = PopupOption.Default,
            ResultCode code = ResultCode.Warning)
        {
            var msg = new ResultMessage()
            {
                Title = $"{code.GetDescription()}\n({CoreUtility.GetErrorCode("b")})",
                Message = message,
                Popup = popup
            };
            return Task.FromResult(new ResultObject<NullDataType>(code, msg, default));
        }

        /// <summary>
        /// FailWithoutErrorCode
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="errorMsg"></param>
        /// <param name="code"></param>
        /// <param name="data"></param>
        /// <param name="popup"></param>
        /// <returns></returns>
        public static Task<ResultObject<T>> FailWithoutErrorCodeAsync<T>(
           string message = "Đã xảy ra lỗi trong quá trình xử lý.",
           string title = "",
           string errorMsg = "",
           ResultCode code = ResultCode.ErrorFail,
           T data = default,
           PopupOption popup = PopupOption.Default)
        {
            var msg = new ResultMessage()
            {
                Title = title,
                Message = message,
                Popup = popup
            };
            return Task.FromResult(new ResultObject<T>(code, msg, default));
        }
    }
}
