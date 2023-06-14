using HospitalAPI.Utils.Extensions;
using HospitalAPI.Utils.Roles.Enums;
using Microsoft.AspNetCore.Authorization;

namespace HospitalAPI.Utils.Roles.Attributes
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public RoleAuthorizeAttribute(EUserRole userRoleFlags)
        {
            Roles = userRoleFlags.ConvertToString(", ");
        }
    }
}