using Microsoft.AspNetCore.Authorization;

namespace TheExchangeApi.Policies
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public string Scope { get; }

        public HasScopeRequirement(
            string scope
            )
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }
    }
}
