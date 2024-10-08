using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Core.Authorization
{
    /// <summary>
    /// check policy
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Tên chức năng cần kiểm tra
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="function"></param>
        public PermissionRequirement(string function)
        {
            Function = function;
        }
    }
}
