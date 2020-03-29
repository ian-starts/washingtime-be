using System;
using Microsoft.AspNetCore.Authentication;

namespace WashingTime.Identity.Dummy
{
    public static class DummyAuthenticationExtensions
    {
        public static AuthenticationBuilder AddTestAuth(
            this AuthenticationBuilder builder,
            Action<DummyAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<DummyAuthenticationOptions, DummyAuthenticationHandler>(
                DummyAuthenticationOptions.Scheme,
                DummyAuthenticationOptions.Scheme,
                configureOptions);
        }
    }
}