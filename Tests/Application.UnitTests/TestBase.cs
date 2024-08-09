using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;
using Application.Accounts;
using Application.Common.Interfaces;

namespace Application.UnitTests
{
    public class TestBase : IDisposable
    {
        protected IServiceProvider ServiceProvider { get; private set; }
        protected IAccountService AccountService { get; private set; }

        public TestBase()
        {
            var services = new ServiceCollection();

            // Register your application services
            services.AddScoped<IAccountService, AccountService>();

            // Build the service provider
            ServiceProvider = services.BuildServiceProvider();

            // Initialize services
            AccountService = ServiceProvider.GetRequiredService<IAccountService>();

            SystemDB.Instance.Reset();
        }

        public void Dispose()
        {
            SystemDB.Instance.Reset();
            GC.SuppressFinalize(this);
        }
    }
}
