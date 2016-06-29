using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.AuthorizationPolicies
{
    public class HasPermissionRequirement : IAuthorizationRequirement
    {
        public string PermissionName { get; set; }
    }
}
