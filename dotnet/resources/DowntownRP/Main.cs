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

            NAPI.Task.Run(async () =>
            {
                // Blips sueltos
                Blip mina = NAPI.Blip.CreateBlip(653, new Vector3(2942.931, 2790.593, 40.29169), 1, 28, "Mina");
                mina.ShortRange = true;
            });

            /*while (true)
            {
                await Task.Delay(59000);
                Utilities.Discord.TcpPlayer.SendSocketTcp();
            }*/
        }

    }
}
