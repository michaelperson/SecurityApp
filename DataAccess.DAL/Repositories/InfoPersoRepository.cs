using DataAccess.DAL.Entities;
using DataAccess.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL.Repositories
{
    public class InfoPersoRepository : BaseRepository<InfoPersoEntity, int>
    {
        
        public InfoPersoRepository(IDbTransaction transaction):base(transaction)
        {

        }
        protected override string SelectCommand
        {
            get
            {
                return "Select * from InfosPerso";
            }
        }

        protected override string InsertCommand
        {
            get
            {
                return "INSERT INTO InfosPerso VALUES ({0},'{1}',{2},'{3}','{4}',{5})";
            }
        }

        protected override string UpdateCommand
        {
            get
            {
                return "Update InfosPerso set IsMarried={0},CompteEnBanque={1}, NbEnfants={2}, Rue={3}, Ville={4},  IdUser={5} WHERE ID={6}";
            }
        }

        protected override string DeleteCommand
        {
            get
            {
                return "DELETE FROM InfosPerso WHERE ID={0}";
            }
        }

        public bool Add(InfoPersoEntity Entity)
        {

            return base.Add(string.Format(InsertCommand, Entity.IsMarried, Entity.CompteEnBanque, Entity.NbEnfants, Entity.Rue, Entity.Ville, Entity.IdUser));
        }


        public bool Delete(InfoPersoEntity Entity)
        {
            return base.Delete(Entity.ID);
        }


        public  bool Update(InfoPersoEntity Entity)
        {

            return this.Update(string.Format(UpdateCommand, Entity.IsMarried, Entity.CompteEnBanque, Entity.NbEnfants, Entity.Rue, Entity.Ville, Entity.IdUser, Entity.ID));
        }

        

        public InfoPersoEntity GetInfoFromUser(int id, out string query)
        {
            query = $"{SelectCommand} WHERE IdUser={id}";
           return base.GetAll(query).FirstOrDefault();
        }
      


    }
}
