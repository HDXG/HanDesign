using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanDesign.Authorization.Application.Users.Dtos;
using HanDesign.Authorization.Domain.Users;
using Mapster;

namespace HanDesign.Authorization.Application.Users
{
    public interface IUserAppService
    {
        Task<List<UserDto>> GetListAsync();
    }
    public class UserAppService(IUserRepository userRepository) : IUserAppService
    {
        public async Task<List<UserDto>> GetListAsync()
        {
            var user= await userRepository.GetListAsync(x => x.UserName.Contains(""));
            return user.Adapt<List<UserDto>>();
        }
    }
}
