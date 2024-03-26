using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using HanDesign.Domain;

namespace HanDesign.Authorization.Domain.Users
{
    public class User:Entity<Guid>
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string AccountNumber { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
