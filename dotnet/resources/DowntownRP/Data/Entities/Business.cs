﻿using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class Business
    {
        public int id { get; set; }
        public int owner { get; set; }
        public string name { get; set; }
        public int type { get; set; } // 1 supermarket, 2 tienda de ropa, 3 gasolinera, 4 tatuador, 5 peluquero, 6 renta de veh, 7 conce, 8 conce empresa, 9 bar, 10 tienda de electronica, 11 central de anuncios, 12 taller mecánicos, 13 growshop
        public double safeBox { get; set; }
        public int price { get; set; }
        public string area { get; set; }
        public int number { get; set; }

        // Gasolineras: 0 sin supermercado | 1 con supermercado
        public int specialType { get; set; } 
        public TextLabel label { get; set; }
        public Marker marker { get; set; }
        public Blip blip { get; set; }
        public ColShape shape { get; set; }


        // Vehicles carshop/rent
        public Vector3 spawn { get; set; }
        public float spawnRot { get; set; }
    }
}
