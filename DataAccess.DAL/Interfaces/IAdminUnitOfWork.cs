using DataAccess.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL.Interfaces
{
    public interface IAdminUnitOfWork : IDisposable
    {
        IRepository<UserEntity, int> UserRepository { get; }
        IRepository<InfoPersoEntity, int> InfoPersoRepository { get; }
        
        bool Commit();
    }

}
