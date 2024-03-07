using FullStackReference.ReferenceRequests.Data;
using FullStackReference.ReferenceRequests.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;  // Add this line
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FullStackReference.ReferenceRequests.IService;

namespace FullStackReference
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Include a Database Context service
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddScoped<IReferenceRequestsRepository, ReferenceRequestsRepository>();
            services.AddScoped<IReferenceRequestsService, ReferenceRequestService>();
        }
    }
}
