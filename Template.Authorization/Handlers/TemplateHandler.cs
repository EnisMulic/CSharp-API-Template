using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Template.Authorization.Requirements;

namespace Template.Authorization.Handlers
{
    public class TemplateHandler : AuthorizationHandler<TemplateRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TemplateRequirement requirement)
        {
            var userEmailAddress = context.User?.FindFirst(ClaimTypes.Email).Value ?? string.Empty;
            if (userEmailAddress.EndsWith(requirement.DomainName))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
