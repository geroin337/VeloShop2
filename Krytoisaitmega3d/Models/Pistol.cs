using System;
using System.Collections.Generic;

namespace Krytoisaitmega3d.Models
{
    public partial class Pistol
    {
        public Pistol()
        {
            Guns = new HashSet<Gun>();
        }

        public int IdPistol { get; set; }
        public string NamePistol { get; set; } = null!;

        public virtual ICollection<Gun> Guns { get; set; }
    }
}
