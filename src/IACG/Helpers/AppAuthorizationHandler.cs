using IACG.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IACG.Helpers
{
    public class AppAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, App>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AppAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, App resource)
        {
            if (context.User.IsInRole(nameof(UserRoles.Administrator)))
            {
                context.Succeed(requirement);
            }
            else
            {if (requirement.Name == ModelOperations.Create.Name
                    || requirement.Name == ModelOperations.Update.Name
                    || requirement.Name == ModelOperations.Delete.Name)
                {
                    if (context.User.IsInRole(nameof(UserRoles.Enterprise))
                        && resource.UserId == _userManager.GetUserId(context.User))
                    {
                        context.Succeed(requirement);
                    }
                }
                else if(requirement.Name == ModelOperations.Read.Name)
                {
                    if (context.User.IsInRole(nameof(UserRoles.Enterprise))
                        && resource.UserId == _userManager.GetUserId(context.User)
                        || context.User.IsInRole(nameof(UserRoles.Professional)))
                    {
                        context.Succeed(requirement);
                    }
                }
                else if(requirement.Name == ModelAppOperations.Review.Name
                    || requirement.Name == ModelAppOperations.Grade.Name)
                {
                    if (context.User.IsInRole(nameof(UserRoles.Professional)))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}