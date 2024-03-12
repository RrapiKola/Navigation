using System.Security.Claims;

namespace api.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            var givenNameClaim = user?.Claims?.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"));

            // Check if the givenNameClaim is not null before accessing its Value property
            return givenNameClaim?.Value;
        }
    }
}