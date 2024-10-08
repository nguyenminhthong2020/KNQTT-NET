namespace Infrastructure.Core.Models
{
    public class BaseResultDto
    {
        /// <summary>
        /// 1: success
        /// còn lại: thất bại
        /// </summary>
        public int ResultId { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
    }
}
