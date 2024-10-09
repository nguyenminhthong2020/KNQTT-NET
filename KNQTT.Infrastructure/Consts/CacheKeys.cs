using Infrastructure.Core.Consts;

namespace KNQTT.Infrastructure.Consts
{
    public static class CacheKeys
    {
        /// <summary>
        /// Key Cache cho lương hiện tại của sale
        /// </summary>
        public const string GetCurrentSalary = CoreConsts.Prefix + "KNQTT_CurrentSalary_{0}";

        /// <summary>
        /// Key cache chống spam đổi mật khẩu
        /// </summary>
        public const string AntiChangePass = CoreConsts.Prefix + "KNQTT_Anti_ChangePass_{0}";

        /// <summary>
        /// Key dùng để rate limit khi tạo thông tin 
        /// </summary>
        public const string CreateLimit = "KNQTT_NET_Create_{0}";
        /// <summary>
        /// Key dùng để bật cờ 
        /// </summary>
        public const string MyFlag = "KNQTT_NET_MyFlag_{0}{1}";

        /// <summary>
        /// Key cache cho token 
        /// </summary>

        public const string Token = CoreConsts.Prefix + "NET_Token";
    }
}
