using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.factions.pd
{
    public class controlpd : Events.Script
    {
        private static RAGE.Ui.HtmlWindow proplspd;
        private static string username_blip;
        private static RAGE.Elements.Blip roscoblip;
        private static int color = 0;
        public controlpd()
        {
            Events.Add("open_proplspd_panel", open_proplspd_panel);
            Events.Add("close_proplspd_panel", close_proplspd_panel);
            Events.Add("objeto_lspd", objeto_lspd);
            Events.Add("rosco_blip", rosco_blip);
            Events.Add("rosco_blip_destroy", rosco_blip_destroy);
            Events.Tick += Tick;
        }

        private void rosco_blip_destroy(object[] args)
        {
            username_blip = null;
            roscoblip.Destroy();
        }

        private void rosco_blip(object[] args)
        {
            username_blip = args[0].ToString();
            roscoblip = new RAGE.Elements.Blip(270, new Vector3(1, 1, 1));
            roscoblip.SetColour(49);
            roscoblip.SetFlashesAlternate(true);
            roscoblip.SetFlashInterval(500);
            roscoblip.SetFlashTimer(50);
        }

        public void Tick(List<Events.TickNametagData> nametags)
        {
            if (username_blip != null)
            {
                if (roscoblip != null)
                {
                    foreach (var player in RAGE.Elements.Entities.Players.All)
                    {
                        if (player.Name == username_blip)
                        {
                            roscoblip.Position = player.Position;
                        }
                    }
                }
            }
        }

        private void open_proplspd_panel(object[] args)
        {
            proplspd = new RAGE.Ui.HtmlWindow("package://statics/controlpd.html");
            RAGE.Ui.Cursor.Visible = true;
        }

        private void close_proplspd_panel(object[] args)
        {
            proplspd.Destroy();
            RAGE.Ui.Cursor.Visible = false;
        }

        private void objeto_lspd(object[] args)
        {
            int type = (int)args[0];
            Events.CallRemote("S_PropsPolicia", type);
        }

    }
}
