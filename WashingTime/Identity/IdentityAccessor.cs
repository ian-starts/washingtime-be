using Microsoft.AspNetCore.Http;

namespace WashingTime.Identity
{
    public class IdentityAccessor : IIdentityAccessor
    {
        public IHttpContextAccessor HttpContextAccessor { get; }

        public IdentityAccessor(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }


        public string UserId => HttpContextAccessor.HttpContext?.User?
            .FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
    }
}