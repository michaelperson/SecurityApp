using DataAccess.DAL.Extensions.Insecure;
using DataAccess.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL
{
    public abstract class BaseRepository<T, TKey> : InsecureIRepository<T, TKey>
       where T : IEntities<TKey>, new()
        where TKey : struct
    {
        protected IDbTransaction _transaction;
        protected IDbConnection _connection { get { return _transaction.Connection; } }

        protected abstract string SelectCommand { get; }
        protected abstract string InsertCommand { get; }
        protected abstract string UpdateCommand { get; }
        protected abstract string DeleteCommand { get; }
       


        public BaseRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }




        public virtual T GetOne(TKey id)
        {
            return _connection.QuerySingleOrDefault<T>(string.Format(SelectCommand +" WHERE ID={0}",id),_transaction);
        }
        public virtual IEnumerable<T> GetAll(string selectCommand)
        {
            return _connection.Query<T>(selectCommand,_transaction);
        }
        public virtual bool Add(string insertCommand)
        {
            try
            {
                _connection.ExecuteScalar<TKey>(insertCommand, _transaction);

                return true;
            }
            catch (Exception ex)
            {
                //TODO: Log ex
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public virtual bool Update(string updateCommand)
        {
            try
            {
                _connection.Execute(updateCommand, _transaction);
                return true;
            }
            catch (Exception ex)
            {
                //TODO: Log ex

                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public virtual bool Delete(TKey id)
        {
            try
            {
                _connection.Execute(string.Format(DeleteCommand + " WHERE ID={0}", id), _transaction);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


         

    }
}

 