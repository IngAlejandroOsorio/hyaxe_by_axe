using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class Weed
    {
        public int id { get; set; }
        public string type { get; set; }
        public int status { get; set; }
        public Vector3 position { get; set; }
        public int dimension { get; set; }
        public TextLabel label { get; set; }
        public GTANetworkAPI.Object prop { get; set; }
    }
}
