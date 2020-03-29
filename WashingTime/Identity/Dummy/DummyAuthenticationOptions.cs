using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace WashingTime.Identity.Dummy
{
    public class DummyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public static string Scheme => "dummy";

        public ClaimsIdentity Identity { get; set; } = (ClaimsIdentity)new DummyUser();
    }
}