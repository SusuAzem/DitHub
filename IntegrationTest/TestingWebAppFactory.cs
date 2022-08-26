using DitHub;
using DitHub.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;


namespace IntegrationTest
{
    //The WebApplicationFactory class is a factory that we can use to bootstrap an application in memory for functional end-to-end tests.
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Startup> where TEntryPoint : Startup
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryEmployeeTest");
                });
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                try
                {
                    appContext.Database.EnsureCreated();
                    //appContext.Database
                }
                catch
                {
                    //Log errors or do anything you think it's needed
                    throw;
                }
            });

        }
    }
}
