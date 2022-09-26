using Api.Tests.IntegrationTest.Utils;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Linq;

namespace Api.Tests.IntegrationTest.Config2
{
    public class CompanyApiFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<TStartup>();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's DbContext registration.
                var descriptor = services.SingleOrDefault(
                       d => d.ServiceType ==
                           typeof(DbContextOptions<CompanyContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add DbContext using an in-memory database for testing.
                services.AddDbContext<CompanyContext>(options =>
                {
                    // Use in memory db to not interfere with the original db.
                    options.UseInMemoryDatabase("CompanyDB.db");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();
                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<CompanyContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<CompanyApiFactory<TStartup>>>();

                    // Ensure the database is created.
                    appDb.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with some specific test data.
                        DBUtilities.InitializeDbForTestsAsync(appDb);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError("An error occurred with database. Error: " + ex.Message);
                    }
                }
            });
        }
    }
}
