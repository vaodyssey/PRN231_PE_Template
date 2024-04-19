﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Payload.Response.Login
{
    public class LoginResponse
    {
        public string Result { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object ReturnData { get; set; }
    }
}
