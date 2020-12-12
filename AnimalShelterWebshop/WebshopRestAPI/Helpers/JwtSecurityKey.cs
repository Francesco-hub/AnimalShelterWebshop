using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebshopRestAPI.Helpers
{
    
    public static class JwtSecurityKey
    {
        private static byte[] secretBytes = Encoding.UTF8.GetBytes("A secret");
    
        public static SymmetricSecurityKey Key
        {
            get { return new SymmetricSecurityKey(secretBytes); }
        }
    public static void SetSecret (string secret)
        {
            secretBytes = Encoding.UTF8.GetBytes(secret);
        }
    
    
    }
    
}
