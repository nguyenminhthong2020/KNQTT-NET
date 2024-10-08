namespace Infrastructure.Core.Consts
{
    public partial class CoreConsts
    {
        /// <summary>
        /// Account đăng nhập của user
        /// VD: thong
        /// </summary>
        public const string ClaimAccountName = "account-name";  

        /// <summary>
        /// Id đăng nhập
        /// </summary>
        public const string ClaimAccountId = "account-id";

        /// <summary>
        /// Tỉnh/thành phố
        /// </summary>
        public const string ClaimLocationId = "location-id";

        /// <summary>
        /// Phạm vi các service được phép access
        /// </summary>
        public const string Scope = "scope";

        /// <summary>
        /// Id token
        /// </summary>
        public const string ClaimIdToken = "jid";

        /// <summary>
        /// Authentication mode
        /// </summary>
        public const string ClaimAuthenticationMode = "authentication-mode";
    }
}
