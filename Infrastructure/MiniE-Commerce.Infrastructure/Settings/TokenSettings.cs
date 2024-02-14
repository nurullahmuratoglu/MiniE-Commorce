using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Infrastructure.Settings
{
    public class TokenSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecurityKey { get; set; }
        public int TokenExpirationMunitues { get; set; }
        public int RefreshTokenExpiratioMunitues { get; set; }
    }
}
