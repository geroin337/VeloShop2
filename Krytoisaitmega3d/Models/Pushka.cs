using System;
using System.Collections.Generic;

namespace Krytoisaitmega3d.Models
{
    public partial class Pushka
    {
        public Pushka()
        {
            Guns = new HashSet<Gun>();
        }

        public int IdPushka { get; set; }
        public string NamePushka { get; set; } = null!;

        public virtual ICollection<Gun> Guns { get; set; }
    }
}
