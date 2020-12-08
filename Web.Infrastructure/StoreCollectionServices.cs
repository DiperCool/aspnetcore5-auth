using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Auth;
using Web.Infrastructure.JWT;
using Web.Infrastructure.Validations.ValidationUser;

namespace Web.Infrastructure
{
    public static class StoreCollectionServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IAuth, Auth.Auth>()
                .AddTransient<IJWT, JWT.JWT>() 
                .AddTransient<IValidationUser, ValidationUser>();
        }
    }
}