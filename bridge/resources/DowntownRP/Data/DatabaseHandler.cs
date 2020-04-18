using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Data
{
    public class DatabaseHandler : Script
    {
        public static string connectionHandle;

        [ServerEvent(Event.ResourceStart)]
        public async void ResourceStartEvent()
        {
            string host = NAPI.Resource.GetSetting<string>(this, "host");
            string user = NAPI.Resource.GetSetting<string>(this, "username");
            string pass = NAPI.Resource.GetSetting<string>(this, "password");
            string db = NAPI.Resource.GetSetting<string>(this, "database");
            string ssl = NAPI.Resource.GetSetting<string>(this, "ssl");

            connectionHandle = "SERVER=" + host + "; DATABASE=" + db + "; UID=" + user + "; PASSWORD=" + pass + "; SSLMODE=" + ssl + ";";

            World.Banks.DatabaseFunctions.SpawnBank();
            World.Companies.DbFunctions.SpawnCompanies();
            World.Business.DbFunctions.SpawnBusiness();
            World.Factions.Vehicles.DbFunctions.SpawnFactionVehicles();
            World.Houses.DbFunctions.SpawnHouses();

            await Task.Delay(1000);
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - -");
            Console.WriteLine($"{Data.Info.serverName} by Muphy || Versión {Data.Info.serverVersion}");
            Console.WriteLine($"=> {Data.Info.banksSpawned} bancos/atms spawneados en el mapa");
            Console.WriteLine($"=> {Data.Info.companiesSpawned} empresas spawneadas en el mapa");
            Console.WriteLine($"=> {Data.Info.businessSpanwed} negocios spawneados en el mapa");
            Console.WriteLine($"=> {Data.Info.vehiclesFactionSpawned} vehiculos faccionarios spawneados en el mapa");
            Console.WriteLine($"=> {Data.Info.housesSpawned} casas spawneadas en el mapa");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - -");
        }
    }
}
