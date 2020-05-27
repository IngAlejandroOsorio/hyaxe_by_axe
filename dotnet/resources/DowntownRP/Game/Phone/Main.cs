using GTANetworkAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Game.Phone
{
    public class Main : Script
    {
        [RemoteEvent("ActionOpenPhone")]
        public void RE_ActionOpenPhone(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.phone != 0)
            {
                if (!user.isPhoneOpen)
                {
                    player.PlayAnimation("anim@cellphone@in_car@ps", "cellphone_text_in", (int)(Utilities.AnimationFlags.StopOnLastFrame | Utilities.AnimationFlags.OnlyAnimateUpperBody | Utilities.AnimationFlags.AllowPlayerControl));
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.AttachPhone", "prop_amb_phone", player.Value, 28422);
                    player.TriggerEvent("OpenPhoneCef", JsonConvert.SerializeObject(user.phoneBook));
                    user.isPhoneOpen = true;
                    return;
                }

                player.TriggerEvent("ClosePhoneCef");
                player.StopAnimation();
                NAPI.ClientEvent.TriggerClientEventForAll("Object.Delete", player.Value);
                user.isPhoneOpen = false;
            }
        }

        public static void ClosePhone(Data.Entities.User user)
        {
            user.entity.TriggerEvent("ClosePhoneCef");
            user.entity.StopAnimation();
            NAPI.ClientEvent.TriggerClientEventForAll("Object.Delete", user.entity.Value);
            user.isPhoneOpen = false;
        }
    }
}
