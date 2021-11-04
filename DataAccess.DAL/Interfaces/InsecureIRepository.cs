using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL.Interfaces
{
    public interface InsecureIRepository<T, TKey>
        where T : IEntities<TKey>, new()
        where TKey :struct
    {

        T GetOne(TKey id);
        IEnumerable<T> GetAll(string selectCommand);
        bool Add(string insertCommand);
        bool Update(string updateCommand);
        bool Delete(TKey id);
    }

}
