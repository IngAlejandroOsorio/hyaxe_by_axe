using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Utilities
{
    public class Chat : Script
    {
        [RemoteEvent("ActionPressT")]
        public void ActionPressT(Player player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            user.chatStatus = true;
            player.SetSharedData("CHAT_STATUS", true);
        }

        [RemoteEvent("ActionPressEnterOrEsc")]
        public void ActionPressEnterOrEsc(Player player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            user.chatStatus = false;
            player.SetSharedData("CHAT_STATUS", false);
        }

        public static void EntornoMe(Player player, string message)
        {
            var msg = "~p~" + player.Name + " " + message;
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        public static void EntornoDo(Player player, string message)
        {
            var msg = "~g~" + message + " (" + player.Name + ")";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }
    }
}
