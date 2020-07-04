using System;
using System.Collections.Generic;
using System.Text;

namespace LinkerPad.Task.Utils
{ 
    public sealed class JwtSettings
    {
        public string Secret { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
    }
}
