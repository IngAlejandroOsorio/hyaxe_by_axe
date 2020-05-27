using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class Faction
    {
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; } // 0 legal, 1 pandilla, 2 mafia
        public int safeBox { get; set; } = 0;
        public int owner { get; set; }
        public string rank1 { get; set; } = "UNDEFINED";
        public string rank2 { get; set; } = "UNDEFINED";
        public string rank3 { get; set; } = "UNDEFINED";
        public string rank4 { get; set; } = "UNDEFINED";
        public string rank5 { get; set; } = "UNDEFINED";
        public string rank6 { get; set; } = "UNDEFINED";
        public Vector3 position { get; set; }
        public HouseInventory inventory { get; set; }
        public List<VehicleFaction> vehicles { get; set; } = new List<VehicleFaction>();
        public bool armasCortas { get; set; }
        public bool armasLargas { get; set; }
        public Blip trafico { get; set; }

        public VehicleFaction traficov { get; set; }
        public DateTime coolDown { get; set; }
    }
}
