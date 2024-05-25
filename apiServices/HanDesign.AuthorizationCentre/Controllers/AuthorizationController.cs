using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HanDesign.Authorization.Application.Users;
using HanDesign.Authorization.Application.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HanDesign.AuthorizationCentre.Controllers
{
    /// <summary>
    /// 授权中心内容
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizationController(
        IConfiguration configuration,
        IUserAppService userAppService) : ControllerBase
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
        /// 创建token
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public string CreateToken()
        {
            List<Claim> claims= new List<Claim>();
            claims.Add(new Claim( "UserName" ,"admin"));
            claims.Add(new Claim("UserId", "123"));
            claims.Add(new Claim("Role", "管理员"));
            return CreateToken(claims);
        }
        /// <summary>
        /// 测试autofac
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public  Task<List<UserDto>> GetUsers() => userAppService.GetListAsync();

        private string CreateToken(List<Claim> Listclaims)
        {
            var signingAlogorithm = SecurityAlgorithms.HmacSha256;
            //取出私钥并以utf8编码字节输出
            var secretByte = Encoding.UTF8.GetBytes(configuration["AuthenticationDemo:SecretKeyDemo"]);
            //使用非对称算法对私钥进行加密
            var signingKey = new SymmetricSecurityKey(secretByte);
            //使用HmacSha256来验证加密后的私钥生成数字签名
            var signingCredentials = new SigningCredentials(signingKey, signingAlogorithm);
            //生成Token
            var Token = new JwtSecurityToken(
                    issuer: configuration["AuthenticationDemo:IssuerDemo"],        //发布者
                    audience: configuration["AuthenticationDemo:AudienceDemo"],    //接收者
                    claims: Listclaims,                                         //存放的用户信息
                    notBefore: DateTime.UtcNow,                             //发布时间
                    expires: DateTime.UtcNow.AddHours(2),                      //有效期设置为2小时
                    signingCredentials                                      //数字签名
                );
            //生成字符串token
            var TokenStr = new JwtSecurityTokenHandler().WriteToken(Token);
            return TokenStr;
        }
    }
}
