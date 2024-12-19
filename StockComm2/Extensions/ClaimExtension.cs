using System.Security.Claims;

namespace StockComm.Extensions
{
    public static class ClaimExtension
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "ClaimsPrincipal cannot be null.");
            }

            var claim = user.Claims.SingleOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")); 
            
            return claim?.Value;
        }
    }
}
