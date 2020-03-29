using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace WashingTime.Identity.Dummy
{
    public class DummyUser : ClaimsIdentity
    {
        private static readonly Guid UniversalDummyUserUuid = Guid.Parse("cf84c3fa-af6e-4ebf-8f24-8d1677a2956e");

        public override IEnumerable<Claim> Claims { get; } = (IEnumerable<Claim>)new Claim[6]
        {
            new Claim(
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                UniversalDummyUserUuid.ToString()),
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "Dummy"),
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "User"),
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn", "Dummy"),
            new Claim("scope", "all"),
            new Claim("client_id", "dummy"),
        };

        public override string AuthenticationType { get; } = DummyAuthenticationOptions.Scheme;

        public override bool IsAuthenticated { get; } = true;
    }
}