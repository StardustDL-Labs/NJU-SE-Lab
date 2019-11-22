using IACG.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IACG.Helpers
{
    public class ReviewAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Review>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Review resource)
        {
            if (context.User.IsInRole(nameof(UserRoles.Administrator)))
            {
                context.Succeed(requirement);
            }
            else
            {
                if (requirement.Name == ModelOperations.Create.Name
                       || requirement.Name == ModelOperations.Update.Name
                       || requirement.Name == ModelOperations.Delete.Name
                       || requirement.Name == ModelOperations.Read.Name)
                {
                    if (context.User.IsInRole(nameof(UserRoles.Professional))
                        && resource.UserId == _userManager.GetUserId(context.User))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}