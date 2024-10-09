using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace KNQTT.API.Controllers
{
    [ApiVersion(2.0)]
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        public StudentController()
        {
            
        }

        /// <summary>
        /// Test thử lấy list student
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/get-list")]
        public async Task<IActionResult> GetListStudent()
        {

            return Ok(new object[]
            {
                new
                {
                    ID = 1,
                    Name = "Khánh"
                },
                new
                {
                    ID = 2,
                    Name = "Nhân"
                },
                new
                {
                    ID = 3,
                    Name = "Quyết"
                },
                new
                {
                    ID = 4,
                    Name = "Thông"
                },
                new
                {
                    ID = 5,
                    Name = "Tú"
                },
            });
        }
    }
}
