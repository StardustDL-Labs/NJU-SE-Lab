using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace IACG.Helpers
{
    public static class ModelAppOperations
    {
        public static OperationAuthorizationRequirement Review =
            new OperationAuthorizationRequirement { Name = nameof(Review) };
        public static OperationAuthorizationRequirement Grade =
            new OperationAuthorizationRequirement { Name = nameof(Grade) };
    }
}
