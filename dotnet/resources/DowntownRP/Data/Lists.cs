using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data
{
    public class Lists
    {
        public static List<Entities.User> playersConnected = new List<Entities.User>();
        public static List<Entities.Company> Companies = new List<Entities.Company>();
        public static List<Entities.FineLSPD> finesPD = new List<Entities.FineLSPD>();
        public static List<Entities.House> houses = new List<Entities.House>();
        public static List<Entities.Business> business = new List<Entities.Business>();
        public static List<Entities.AsistenciaStaff> aStaff = new List<Entities.AsistenciaStaff>();
        public static List<Entities.MinerPoint> minerPoints = new List<Entities.MinerPoint>();
        public static List<int> playersFaceSync = new List<int>();

        public static List<Entities.Faction> factions = new List<Entities.Faction>()
        {
            new Entities.Faction(){ id = 1 , name = "LSPD" },
            new Entities.Faction(){ id = 2, name = "LSMD" }
        };
        public static List<Entities.Entorno> entornos = new List<Entities.Entorno>();
    }
}
