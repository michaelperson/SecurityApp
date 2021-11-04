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
                return "INSERT INTO Users VALUES ('{0}', '{1}','{2}', '{3}')";
            }
        }

        protected override string UpdateCommand
        {
            get
            {
                return "Update Users set Login='{0}',Password='{1}',Email='{2}', 'Role'={3} WHERE ID={4}";
            }
        }

        protected override string DeleteCommand
        {
            get
            {
                return "DELETE FROM Users WHERE ID={0}";
            }
        }

        public bool Add(UserEntity Entity)
        {

            return base.Add(string.Format(InsertCommand, Entity.Login, Entity.Password, Entity.Email, Entity.Role));
        }


        public bool Delete(UserEntity Entity)
        {
            return base.Delete(Entity.ID);
        }


        public  bool Update(UserEntity Entity)
        {

            return this.Update(string.Format(UpdateCommand,Entity.Login, Entity.Password,Entity.Email,Entity.Role, Entity.ID));
        }

        public List<UserEntity> Auth(string login, string password, out string Query)
        {
          

            Query = $"{SelectCommand} WHERE login='{login}' and Password='{password}'";
            return base.GetAll(Query).ToList();
        }

        
      


    }
}
