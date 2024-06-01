using HanDesign.System.Application.Users;
using HanDesign.System.Application.Users.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HanDesign.SystemCenter.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    /// <param name="userAppService"></param>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController(IUserAppService userAppService) : ControllerBase
    {
        /// <summary>
        /// 返回用户集合
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Task<List<UserDto>> GetUserList()=> userAppService.GetListAsync();

        /// <summary>
        /// 测试一下返回
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public int Demo()
        {
            return Int32.Parse("afa");
        }
    }
}
