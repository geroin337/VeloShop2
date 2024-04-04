using System;
using System.Collections.Generic;

namespace Krytoisaitmega3d.Models
{
    public partial class Client
    {
        public int IdClient { get; set; }
        public string Login { get; set; } = null!;
        public string EMail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
