using System;
using System.Collections.Generic;

namespace Krytoisaitmega3d.Models
{
    public partial class Gun
    {
        public int IdGun { get; set; }
        public string NameGun { get; set; } = null!;
        public int PushkaId { get; set; }
        public int PistolId { get; set; }
        public int MachineId { get; set; }
        public string View { get; set; } = null!;
        public int Price { get; set; }
        public string Description { get; set; } = null!;
        public byte[]? Picture { get; set; } 

        public virtual Machine Machine { get; set; } = null!;
        public virtual Pistol Pistol { get; set; } = null!;
        public virtual Pushka Pushka { get; set; } = null!;
    }
}
