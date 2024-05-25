using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanDesign.Domain.Repositoryes;

namespace HanDesign.System.Domain.Users
{
    public interface IUserRepository: IBaseRepository<User,Guid>
    {

    }
}
