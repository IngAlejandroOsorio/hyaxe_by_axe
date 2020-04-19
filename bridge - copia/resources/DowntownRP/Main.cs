using System;
using System.Threading.Tasks;
using System.Threading;
using GTANetworkAPI;

namespace DowntownRP
{
    public class Main : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public async void ResourceStartMain()
        {
            await Task.Delay(2000);

            NAPI.Server.SetGlobalServerChat(false);
            NAPI.Server.SetGamemodeName(Data.Info.serverName + " v" + Data.Info.serverVersion);
            NAPI.Server.SetCommandErrorMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
            NAPI.Server.SetAutoRespawnAfterDeath(false);
        }

    }
}
