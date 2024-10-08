using System.ComponentModel;

namespace Infrastructure.Core.Enums
{
    public enum ResultCode
    {
        // success
        /// <summary>
        /// success
        /// </summary>
        [Description("Thông báo")]
        Ok = 200,

        // system error
        /// <summary>
        /// lỗi chứng thực  
        /// </summary>
        [Description("Thông báo")]
        ErrorAuthenticationFail = -120,

        /// <summary>
        /// lỗi input
        /// </summary>
        [Description("Đã có lỗi xảy ra!")]
        ErrorInputInvalid = -121,

        /// <summary>
        /// checksum fail
        /// </summary>
        [Description("Đã có lỗi xảy ra!")]
        ErrorChecksumFail = -122,

        /// <summary>
        /// access-token hết hạn
        /// </summary>
        [Description("Đã có lỗi xảy ra!")]
        ErrorTokenExpired = -123,

        /// <summary>
        /// lỗi phân quyền
        /// </summary>
        [Description("Đã có lỗi xảy ra!")]
        ErrorAuthorizationFail = -124,

        /// <summary>
        /// bảo trì
        /// </summary>
        [Description("Thông báo bảo trì!")]
        ErrorMaintenance = -199,

        // error
        /// <summary>
        /// lỗi chung business
        /// </summary>
        [Description("Đã có lỗi xảy ra!")]
        ErrorFail = -110,

        /// <summary>
        /// lỗi exception
        /// </summary>
        [Description("Đã có lỗi xảy ra!")]
        ErrorException = -130,

        /// <summary>
        /// lỗi không tìm thấy dữ liệu
        /// </summary>
        [Description("Đã có lỗi xảy ra!")]
        ErrorNotFound = -140,

        /// <summary>
        /// lỗi có dữ liệu
        /// </summary>
        [Description("Đã có lỗi xảy ra!")]
        ErrorNoContent = -141,

        /// <summary>
        /// dữ liệu đã tồn tại
        /// </summary>
        [Description("Thông báo")]
        ErrorConflict = -142,

        // warning
        /// <summary>
        /// Cảnh báo chung
        /// </summary>
        [Description("Cảnh báo!!!")]
        Warning = -301,

        /// <summary>
        /// Không có data
        /// </summary>
        [Description("Cảnh báo!!!")]
        WarningNoContent = -302,

        /// <summary>
        /// Không có quyền truy cập
        /// </summary>
        [Description("Cảnh báo!!!")]
        WarningForbidden = -303,

        /// <summary>
        /// Cảnh báo có 2 action
        /// </summary>
        [Description("Cảnh báo!!!")]
        WarningWithTwoAction = -351
    }
}
