using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanDesign.Authorization.Domain.Users;

namespace HanDesign.Authorization.Application.Users
{
    public interface IUserAppService
    {
        Task<List<User>> GetListAsync();
    }
    public class UserAppService(IUserRepository userRepository) : IUserAppService
    {
        public async Task<List<User>> GetListAsync()
        {
            return await userRepository.GetListAsync(x=>x.UserName.Contains(""));
        }
    }
}
