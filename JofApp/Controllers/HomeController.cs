using JofApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JofApp.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }

       
    }
}