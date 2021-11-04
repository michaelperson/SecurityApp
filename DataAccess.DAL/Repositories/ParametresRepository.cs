using DataAccess.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL.Repositories
{
    public class ParametresRepository : BaseRepository<ParametresEntity, int>
    {
        public ParametresRepository(IDbTransaction transaction) : base(transaction)
        {

        }
        protected override string SelectCommand
        {
            get
            {
                return "Select * from Parametres";
            }
        }

        protected override string InsertCommand
        {
            get
            {
                return "INSERT INTO Parametres VALUES {}";
            }
        }

        protected override string UpdateCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override string DeleteCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
