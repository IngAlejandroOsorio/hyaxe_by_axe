using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class VehicleFaction
    {
        public int id { get; set; }
        public Vehicle entity { get; set; }
        public string model { get; set; }
        public int faction { get; set; }
    }
}
