using DataAccess.DAL;
using DataAccess.DAL.Entities;
using DataAccess.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private IUnitOfWork _uow;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow)
        {
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
              List<UserEntity> Lu =  _uow.UserRepository.GetAll().ToList();
            }
            return View("Info", user);
        }
    }
}
