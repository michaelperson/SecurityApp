using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL.Interfaces
{
    public interface IRepository<T, TKey>
        where T : IEntities<TKey>, new()
        where TKey :struct
    {

        T GetOne(TKey id);
        IEnumerable<T> GetAll();
        bool Add(T Entity);
        bool Update(T Entity);
        bool Delete(TKey id);
    }

}
