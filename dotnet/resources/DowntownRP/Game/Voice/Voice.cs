using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;

namespace DowntownRP.Game.Voice
{
    public class Voice : Script
    {
        [RemoteEvent("add_voice_listener")]
        public void AddVoiceListener(Player player, Player target)
        {
            if (target != null)
            {
                player.EnableVoiceTo(target);
            }
        }

        [RemoteEvent("remove_voice_listener")]
        public void RemoveVoiceListener(Player player, Player target)
        {
            if (target != null)
            {
                player.DisableVoiceTo(target);
            }

        }

        [RemoteEvent("MicroSetData")]
        public void RE_MicroSetData(Player player, int type)
        {
            if (type == 0) player.SetSharedData("MICRO_STATUS", false);
            else player.SetSharedData("MICRO_STATUS", true);
        }

        [RemoteEvent("sendVoiceChangedNotification")]
        public async Task RE_sendVoiceChangedNotification(Player player, int type)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (type == 0)
            {
                await DatabaseFunctions.UpdateVoiceMode(user.idpj, 0);
                Utilities.Notifications.SendNotificationOK(player, "Has cambiado el modo de voz a transmisión continua");
                user.enableMicrophone = 0;
                player.SetSharedData("MICRO_STATUS", true);
            }
            else
            {
                await DatabaseFunctions.UpdateVoiceMode(user.idpj, 1);
                Utilities.Notifications.SendNotificationOK(player, "Has cambiado el modo de voz a push to talk");
                user.enableMicrophone = 1;
                player.SetSharedData("MICRO_STATUS", false);
            }
        }
    }
}
