using DataAccess.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace SecurityApp.Infra
{
    public static class SessionUtils
    {
        private static IHttpContextAccessor _HttpContextAccessor;
        public static IHttpContextAccessor HttpContextAccessor
        {
            set { _HttpContextAccessor = value; }
        }
         

        public static bool IsLogged
        {
            get {
                if (_HttpContextAccessor.HttpContext.Session.GetString("IsLogged") != null)
                    return bool.Parse(_HttpContextAccessor.HttpContext.Session.GetString("IsLogged"));
                else
                    return false;
            }
            set { _HttpContextAccessor.HttpContext.Session.SetString("IsLogged", value.ToString()); }
        }

        /// <summary>
        /// Variable de session servant a stocker l'id de l'utilisateur connecté
        /// </summary>
        public static int UserId
        {
            get
            {
                if (_HttpContextAccessor.HttpContext.Session.GetInt32("UserId") != null)
                    return _HttpContextAccessor.HttpContext.Session.GetInt32("UserId").Value;
                else
                    return -1;
            }
            set { _HttpContextAccessor.HttpContext.Session.SetInt32("UserId", value); }
        }


        public static UserEntity ConnectedUser
        {
            get 
            {
                UserEntity Ue = null;
                if (_HttpContextAccessor.HttpContext.Session.GetString("ConnectedUser") != null)
                {
                    Ue = JsonConvert.DeserializeObject<UserEntity>(_HttpContextAccessor.HttpContext.Session.GetString("ConnectedUser"));
                }
                return Ue;
            }

            set 
            {
                _HttpContextAccessor.HttpContext.Session.SetString("ConnectedUser", JsonConvert.SerializeObject(value));
            }
        }
    }
}
