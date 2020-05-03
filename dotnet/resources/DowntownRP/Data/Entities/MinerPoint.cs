using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class MinerPoint
    {
        public int id { get; set; }
        public TextLabel label { get; set; }
        public ColShape shape { get; set; }
        public int recursos { get; set; } = 25;
    }
}
