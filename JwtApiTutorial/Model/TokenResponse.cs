using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtApiTutorial.Model
{
    public class TokenResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Value { get; set; }
    }
}
