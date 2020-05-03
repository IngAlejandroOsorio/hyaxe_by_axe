using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class Policia
    {
        public int Placa { get; set; }
        public int Rango { get; set; }
        public bool PuedeContratar { get; set; } = false;
        public Inventory Taquilla { get; set; }
        public int PuedeTocar { get; set; }
        public bool SWAT { get; set; }
    }
}
