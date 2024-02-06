using Microsoft.AspNetCore.Mvc;
using RzhadBids.Services;

namespace RzhadBids.Controllers
{
    public class DbController : Controller
    {
        protected readonly DatabaseContext databaseContext;
        public DbController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
    }
}
