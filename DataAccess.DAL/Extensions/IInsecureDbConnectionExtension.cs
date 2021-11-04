using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL.Extensions.Insecure
{
    public static class IInsecureDbConnectionExtension
    {
        public static T QuerySingleOrDefault<T>(this IDbConnection DbCon, string Command, IDbTransaction transaction)
        {
            if (DbCon == null) throw new InvalidOperationException("Your Connection must be set");

            T retour = default(T);
            if (DbCon.State == ConnectionState.Closed) DbCon.Open();

            IDbCommand myCommand = DbCon.CreateCommand();

            try
            {
                if (transaction != null)
                {
                    myCommand.Transaction = transaction;
                }
                myCommand.CommandText = Command;
              
                IDataReader Idr = myCommand.ExecuteReader();
                if (Idr.Read())
                {
                    retour = mapTo<T>(Idr);
                }
                Idr.Close();

            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);

            }
            return retour;
        }
        public static IEnumerable<T> Query<T>(this IDbConnection DbCon, string Command, IDbTransaction transaction)
        {
            if (DbCon == null) throw new InvalidOperationException("Your Connection must be set");

            Type listType = typeof(List<>);
            Type constructedListType = listType.MakeGenericType(typeof(T));

            List<T> retour = (List<T>)Activator.CreateInstance(constructedListType);

            if (DbCon.State == ConnectionState.Closed) DbCon.Open();

            IDbCommand myCommand = DbCon.CreateCommand();
            try
            {
                if (transaction != null)
                {
                    myCommand.Transaction = transaction;
                }
                myCommand.CommandText = Command;
               
                IDataReader Idr = myCommand.ExecuteReader();
                while (Idr.Read())
                {
                    retour.Add(mapTo<T>(Idr));
                }
                Idr.Close();
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);

            }
            return retour;
        }

        public static TKey ExecuteScalar<TKey>(this IDbConnection DbCon, string Command,  IDbTransaction transaction)
        {
            if (DbCon == null) throw new InvalidOperationException("Your Connection must be set");

            TKey retour = default(TKey);
            if (DbCon.State == ConnectionState.Closed) DbCon.Open();

            IDbCommand myCommand = DbCon.CreateCommand();
            if (transaction != null)
            {
                myCommand.Transaction = transaction;
            }
            try
            {
                myCommand.CommandText = Command;
              

                TKey rowsAffected = (TKey)myCommand.ExecuteScalar();

            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                Debug.WriteLine(e.Message);

            }
            return retour;
        }

        public static bool Execute(this IDbConnection DbCon, string Command,   IDbTransaction transaction)
        {
            if (DbCon == null) throw new InvalidOperationException("Your Connection must be set");
            bool result = false;

            if (DbCon.State == ConnectionState.Closed) DbCon.Open();

            IDbCommand myCommand = DbCon.CreateCommand();
            if (transaction != null)
            {
                myCommand.Transaction = transaction;
            }
            try
            {
                myCommand.CommandText = Command;
                 
                myCommand.ExecuteNonQuery();
                result = true;
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                Debug.WriteLine(e.Message);

            }
            return result;
        }


         
        private static T mapTo<T>(IDataReader idr)
        {
            T retour = Activator.CreateInstance<T>();
            for (int i = 0; i < idr.FieldCount; i++)
            {
                string Name = idr.GetName(i);
                object value = idr.GetValue(i);
                if (value != DBNull.Value)
                {
                    try
                    {
                        typeof(T).GetProperty(Name).SetValue(retour, value);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        throw;
                    }
                }
            }
            return retour;
        }


    }
}
