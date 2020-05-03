using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class Race
    {
        public int step { get; set; }
        public Vector3 position { get; set; }
        public int ganancies { get; set; }
        public Vehicle vehicle { get; set; }
        public int businessId { get; set; }
        public ColShape shape { get; set; }
        public Vector3 startPosition { get; set; }
    }
}
