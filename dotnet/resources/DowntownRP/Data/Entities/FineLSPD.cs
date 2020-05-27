using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class FineLSPD
    {
        public int IdDatabase { get; set; }
        public int userid { get; set; }
        public string reason { get; set; }
        public int price { get; set; }
        public bool isPaid { get; set; } = false;
    }
}
