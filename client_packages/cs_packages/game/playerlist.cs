using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game
{
    public class playerlist : Events.Script
    {
        private static RAGE.Ui.HtmlWindow window;
        private static bool isOpen = false;
        public playerlist()
        {
            Events.Add("OpenPlayerList", OpenPlayerList);
        }

        private void OpenPlayerList(object[] args)
        {
            bool lol = (bool)RAGE.Elements.Player.LocalPlayer.GetSharedData("isLogged");

            if (lol)
            {
                if(RAGE.Elements.Player.LocalPlayer.GetSharedData("CHAT_STATUS") != null)
                {
                    if (!(bool)RAGE.Elements.Player.LocalPlayer.GetSharedData("CHAT_STATUS"))
                    {
                        if (!isOpen)
                        {
                            window = new RAGE.Ui.HtmlWindow("package://statics/main/playerlist.html");

                            foreach (var player in RAGE.Elements.Entities.Players.All)
                            {
                                window.ExecuteJs($"usersOnline({player.RemoteId}, `{player.Name}`)");
                            }
                            isOpen = true;
                            return;
                        }
                        else
                        {
                            isOpen = false;
                            window.Destroy();
                        }
                        return;
                    }
                }
                else
                {
                    if (!isOpen)
                    {
                        window = new RAGE.Ui.HtmlWindow("package://statics/main/playerlist.html");

                        foreach (var player in RAGE.Elements.Entities.Players.All)
                        {
                            window.ExecuteJs($"usersOnline({player.RemoteId}, `{player.Name}`)");
                        }
                        isOpen = true;
                        return;
                    }
                    else
                    {
                        isOpen = false;
                        window.Destroy();
                    }
                }
            }
        }
    }
}
