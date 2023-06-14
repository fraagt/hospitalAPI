using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace HospitalAPI.Utils.Identity.Extensions
{
    public static class ClaimsExtensions
    {
        public static int? GetClaimIntValue(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var claim = claimsPrincipal.FindFirstValue(claimType);

            if (claim.IsNullOrEmpty())
                return null;

            //disabled warning only because check for IsNull already done on 12 line
#pragma warning disable CS8604
            return int.Parse(claim);
#pragma warning restore CS8604
        }
    }
}