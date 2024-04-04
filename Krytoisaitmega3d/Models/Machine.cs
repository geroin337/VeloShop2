using System;
using System.Collections.Generic;

namespace Krytoisaitmega3d.Models
{
    public partial class Machine
    {
        public Machine()
        {
            Guns = new HashSet<Gun>();
        }

        public int IdMachine { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Gun> Guns { get; set; }
    }
}
