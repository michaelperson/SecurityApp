using DataAccess.DAL.Extensions; 
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
    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey>
       where T : IEntities<TKey>, new()
        where TKey : struct
    {
        protected IDbTransaction _transaction;
        protected IDbConnection _connection { get { return _transaction.Connection; } }

        protected abstract string SelectCommand { get; }
        protected abstract string InsertCommand { get; }
        protected abstract string UpdateCommand { get; }
        protected abstract string DeleteCommand { get; }
        protected virtual string OneCommand
        {
            get
            {
                return string.Format("{0} where  ID = @id", SelectCommand); 

            }
        }




        public BaseRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }




        public virtual T GetOne(TKey id)
        {
            return _connection.QuerySingleOrDefault<T>(OneCommand, null, _transaction);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _connection.Query<T>(SelectCommand, null, _transaction);
        }

         
        public virtual bool Add(T Entity)
        {
            try
            {
                _connection.ExecuteScalar<TKey>(InsertCommand
                                                          , FieldToCollumn(Entity)
                                                          , _transaction);

                return true;
            }
            catch (Exception ex)
            {
                //TODO: Log ex
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public virtual bool Update(T Entity)
        {
            try
            {
                _connection.Execute(UpdateCommand
                                                                   , FieldToCollumn(Entity)
                                                                   , _transaction);
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
                _connection.Execute(DeleteCommand,
                                       new
                                       {
                                           Id = id
                                       }, _transaction);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        protected object FieldToCollumn(T element)
        {
            var eo = new ExpandoObject();
            var eoColl = (ICollection<KeyValuePair<string, object>>)eo;

            foreach (PropertyInfo pInfo in typeof(T).GetProperties())
            {
                if (pInfo.Name != "Id")
                {
                    eoColl.Add(new KeyValuePair<string, object>(pInfo.Name, pInfo.GetValue(element)));
                }
            }

            dynamic eoDynamic = eo;

            return eoDynamic;
        }



    }
}

 