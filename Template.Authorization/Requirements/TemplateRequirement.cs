using Microsoft.AspNetCore.Authorization;

namespace Template.Authorization.Requirements
{
    public class TemplateRequirement : IAuthorizationRequirement
    {
        public string DomainName { get; }

        public TemplateRequirement(string domainName)
        {
            DomainName = domainName;
        }
    }
}
