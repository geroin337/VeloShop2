using System;
using System.Collections.Generic;

namespace Krytoisaitmega3d.Models
{
    public partial class Order
    {
        public int IdOrder { get; set; }
        public int GunId { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Gun Gun { get; set; } = null!;
    }
}
