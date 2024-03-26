using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanDesign.Authorization.Domain.Users;
using HanDesign.EntityFrameworkCore.Repositoryes;

namespace HanDesign.Authorization.Infrastructure.Users
{
    public class UserRepository(IAuthorizationContext context):BasicRepository<IAuthorizationContext,User,Guid>(context),IUserRepository
    {

    }
}
