using DataAccess.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL.Entities
{
    public class UserEntity: IEntities<int>
    {
         

        public int ID{ get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
