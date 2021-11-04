using DataAccess.DAL.Entities;
using DataAccess.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL.Repositories
{
    public class UserRepository : BaseRepository<UserEntity, int>
    {
        
        public UserRepository(IDbTransaction transaction):base(transaction)
        {

        }
        
        protected override string SelectCommand
        {
            get
            {
                return "Select * from Users";
            }
        }

        protected override string InsertCommand
        {
            get
            {
                return "INSERT INTO Users VALUES (@Login, @Password,@Email, @Role)";
            }
        }

        protected override string UpdateCommand
        {
            get
            {
                return "Update Users set Login=@Login,Password=@Password,Email=@Email, 'Role'=@Role WHERE ID=@ID";
            }
        }

        protected override string DeleteCommand
        {
            get
            {
                return "DELETE FROM Users WHERE ID=@ID";
            }
        }

         
        public UserEntity Auth(string login, string password )
        {
             
            return base.GetAll().Where(u=>u.Login==login && u.Password==password).FirstOrDefault();
        }

        
      


    }
}
