using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RzhadBids.Services;

namespace RzhadBids
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }
    }
}
