using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game
{
    public class ads : Events.Script
    {
        private static RAGE.Ui.HtmlWindow window = null;
        public ads()
        {
            Events.Add("OpenAdsBrowser", OpenAdsBrowser);
            Events.Add("CloseAdsBrowser", CloseAdsBrowser);
            Events.Add("SendAdsToServer", SendAdsToServer);
        }

        private void SendAdsToServer(object[] args)
        {
            Events.CallRemote("AdsSendToServer", args[0].ToString());
        }

        private void OpenAdsBrowser(object[] args)
        {
            if (window != null) window.Destroy();
            window = new RAGE.Ui.HtmlWindow("package://statics/main/ads.html");
            RAGE.Ui.Cursor.Visible = true;
            RAGE.Chat.Show(false);
        }

        private void CloseAdsBrowser(object[] args)
        {
            window.Destroy();
            RAGE.Ui.Cursor.Visible = false;
            window = null;
            RAGE.Chat.Show(true);
        }
    }
}
