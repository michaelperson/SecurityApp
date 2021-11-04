using DataAccess.DAL.Entities;
using DataAccess.DAL.Interfaces;
using DataAccess.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public InsecureIRepository<UserEntity, int> UserRepository
        {
            get
            {
                return new UserRepository(_transaction);
            }
        }
        public InsecureIRepository<InfoPersoEntity, int> InfoPersoRepository
        {
            get
            {
                return new InfoPersoRepository(_transaction);
            }
        }


        public UnitOfWork(string connectionString)
        {
            try
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
                _transaction = _connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

        }


        public bool Commit()
        {
            bool isOk = false;
            try
            {
                _transaction.Commit();
                isOk = true;
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
            }
            return isOk;
        }

        protected virtual void Dispose(bool info = true)
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {


            Dispose(true);
        }
    }
}

 
