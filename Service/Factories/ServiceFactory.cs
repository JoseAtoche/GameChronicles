using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Factories
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUserService CreateUserService()
        {
            return _serviceProvider.GetRequiredService<IUserService>();
        }

        public IGameService CreateGameService()
        {
            return _serviceProvider.GetRequiredService<IGameService>();
        }

        public IUserGameService CreateUserGameService()
        {
            return _serviceProvider.GetRequiredService<IUserGameService>();
        }
    }

}
