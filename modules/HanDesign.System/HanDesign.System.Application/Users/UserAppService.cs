using HanDesign.System.Application.Users.Dtos;
using HanDesign.System.Domain.Users;
using Mapster;

namespace HanDesign.System.Application.Users
{
    public interface IUserAppService
    {
        Task<List<UserDto>> GetListAsync();
    }
    public class UserAppService(IUserRepository userRepository) : IUserAppService
    {
        public async Task<List<UserDto>> GetListAsync()
        {
            var UserList =await userRepository.GetListAsync(x=>x.UserName.Contains(""));
            return UserList.Adapt<List<UserDto>>();
        }
    }
}
