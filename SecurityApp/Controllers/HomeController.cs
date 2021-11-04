using DataAccess.DAL;
using DataAccess.DAL.Entities;
using DataAccess.DAL.Interfaces;
using DataAccess.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecurityApp.Infra;
using SecurityApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityApp.Controllers
{
    [AutoValidateAntiforgeryToken] //Pour que TOUS les forms valident le forgery par défaut pour ce controller
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _uow;
        private ISession _session;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow, IHttpContextAccessor contextAccessor)
        {
            SessionUtils.HttpContextAccessor = contextAccessor;
            _session = contextAccessor.HttpContext.Session;
            _logger = logger;
            this._uow = uow; 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Permet de vérifier que le token de vérification est envoyé et valides
        public IActionResult Login(UserEntity user)
        {
            if(ModelState.IsValid)
            {
                user = (_uow.UserRepository as UserRepository).Auth(user.Login, user.Password);
               if ( user !=null)
                {
                    SessionUtils.ConnectedUser = user;

                    SessionUtils.IsLogged = true;
                    // _session.SetString("IsLogged", "true");
                    SessionUtils.UserId = user.ID;
                    //_session.SetInt32("UserId", user.ID);
                    return RedirectToAction("InfoUser", new { id=user.ID });
                }
            }
            return View("Info", user);
        }


        public IActionResult InfoUser(int id)
        {
            if (SessionUtils.UserId == id)
            {
                if (SessionUtils.IsLogged)
                {
                    UserEntity UE = SessionUtils.ConnectedUser;
                    InfoPersoEntity IP = (_uow.InfoPersoRepository as InfoPersoRepository).GetInfoFromUser(id);
                    return View(IP);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
