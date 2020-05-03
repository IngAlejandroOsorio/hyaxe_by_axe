using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class VehicleCharacter
    {
        public int id { get; set; }
        public Vehicle entity { get; set; }
        public User owner { get; set; }
        public Inventory trunk { get; set; }
    }
}
