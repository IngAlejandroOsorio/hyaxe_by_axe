using Discord.WebSocket;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data
{
    public class Info
    {
        public static string serverName = "Hyaxe";
        public static string serverVersion = "0.1.6";
        public static int playersConnected = 0;
        public static int banksSpawned = 0;
        public static int companiesSpawned = 0;
        public static int businessSpanwed = 0;
        public static int vehiclesFactionSpawned = 0;
        public static int housesSpawned = 0;

        public static Vector3 lastDeathAdviceLSMD = new Vector3(0, 0, 0);

        public static int AiTaxiRaces = 0;
    }
}
