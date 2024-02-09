using Microsoft.AspNetCore.Mvc;
using RzhadBids.Models;
using System.Diagnostics;

namespace RzhadBids.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/lots?active=true");
        }
    }
}