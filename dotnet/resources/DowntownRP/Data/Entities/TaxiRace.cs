using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class TaxiRace
    {
        public int id { get; set; }
        public Vector3 position { get; set; }
        public string mensaje { get; set; }
        public bool isAccepted { get; set; } = false;
        public Player solicitador { get; set; }
        public Player driver { get; set; }
        public double money { get; set; } = 0;
    }
}
