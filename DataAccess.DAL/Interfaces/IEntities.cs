using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL.Interfaces
{
    /// <summary>
    /// Interface to create contract for entities
    /// </summary>
    /// <typeparam name="TKey">The type of the uniqueIdentifier</typeparam>
    public interface IEntities<TKey>
        where TKey : struct
    {
        TKey ID { get; }
    }

}
