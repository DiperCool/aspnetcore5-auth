using Microsoft.AspNetCore.Http;
using Web.Models.Db;

namespace Web.Infrastructure.ExtensionMethods
{
    public static class ContextExtensionMethods
    {
        public static string GetFullUrl(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}{request.PathBase}";
        }
    }
}