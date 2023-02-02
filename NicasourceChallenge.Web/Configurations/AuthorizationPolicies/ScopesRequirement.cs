using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;

namespace NicasourceChallenge.Web.Configurations.AuthorizationPolicies
{
    public class ScopesRequirement : AuthorizationHandler<ScopesRequirement>, IAuthorizationRequirement
    {
        private readonly string[] _acceptedScopes;

        public ScopesRequirement(params string[] acceptedScopes)
        {
            _acceptedScopes = acceptedScopes;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopesRequirement requirement)
        {
            if (context.User.Claims.All(x => x.Type != ClaimConstants.Scope) && context.User.Claims.All(y => y.Type != ClaimConstants.Scp))
            {
                return Task.CompletedTask;
            }

            var scopeClaim = context?.User?.FindFirst(ClaimConstants.Scp) ?? context?.User?.FindFirst(ClaimConstants.Scope);

            if (scopeClaim != null && scopeClaim.Value.Split(' ').Intersect(requirement._acceptedScopes).Any())
            {
                context?.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
