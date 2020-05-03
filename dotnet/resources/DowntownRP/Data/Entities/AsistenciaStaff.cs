using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class AsistenciaStaff
    {
        public int userid { get; set; }
        public string mensaje { get; set; }
        public Vector3 posicion { get; set; }
        public string Pj { get; set; }
        public int idStaff { get; set; }
        public int estado { get; set; } = 0; //0-Sin leer || 1- Solucionado

    }
}
