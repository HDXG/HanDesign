using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanDesign.EntityFrameworkCore.Repositoryes;
using HanDesign.System.Domain.Users;

namespace HanDesign.System.Infrastructure.Users
{
    public class UserRepository(ISystemContext context): BasicRepository<ISystemContext, User,Guid>(context),IUserRepository
    {
    }
}
