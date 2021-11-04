using DataAccess.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        InsecureIRepository<UserEntity, int> UserRepository { get; }
        InsecureIRepository<InfoPersoEntity, int> InfoPersoRepository { get; }
        bool Commit();
    }

}
