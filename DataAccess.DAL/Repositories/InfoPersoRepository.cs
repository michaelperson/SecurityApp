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
                return "INSERT INTO InfosPerso VALUES (@IsMarried,@CompteEnBanque,@NbEnfants,@Rue,@Ville,@IdUser)";
            }
        }

        protected override string UpdateCommand
        {
            get
            {
                return "Update InfosPerso set IsMarried=@IsMarried,CompteEnBanque=@CompteEnBanque, NbEnfants=@NbEnfants, Rue=@Rue, Ville=@Ville,  IdUser=@IdUser WHERE ID=@ID";
            }
        }

        protected override string DeleteCommand
        {
            get
            {
                return "DELETE FROM InfosPerso WHERE ID=@ID";
            }
        }

         

        

        public InfoPersoEntity GetInfoFromUser(int id)
        {
            
           return base.GetAll().Where(u=>u.IdUser==id).FirstOrDefault();
        }
      


    }
}
