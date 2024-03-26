using HanDesign.Authorization.Application.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HanDesign.AuthorizationCentre.Controllers
{
    /// <summary>
    /// 授权中心内容
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizationController(IUserAppService userAppService) : ControllerBase
    {
        /// <summary>
        /// 测试方法使用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "测试使用";
        }

        /// <summary>
        /// 测试autofac
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> GetUsers()
        {
            return Ok(await userAppService.GetListAsync());
        }
    }
}
