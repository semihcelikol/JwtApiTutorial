﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtApiTutorial.Model
{
    public class TokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
