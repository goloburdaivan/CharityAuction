using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RzhadBids.Auth;
using RzhadBids.Services;

namespace RzhadBids.Controllers
{
    public class BaseController : Controller
    {
        protected readonly DatabaseContext databaseContext;

        [FromServices]
        public UserManager<ApplicationUser> UserManager { get; set; }
        [FromServices]
        public SignInManager<ApplicationUser> SignInManager { get; set; }
        public BaseController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
    }
}
