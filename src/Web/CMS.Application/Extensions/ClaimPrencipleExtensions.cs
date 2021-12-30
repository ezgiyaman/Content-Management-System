using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CMS.Application.Extensions
{
    public static class ClaimPrencipleExtensions
    {
        //Buradaki extension method authenticate olmuş kullanıcıyı usernamesinden Id'sini yakalayıp , teslim edecek.

        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
