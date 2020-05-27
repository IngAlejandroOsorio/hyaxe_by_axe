using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game
{
    public class pcu : Events.Script
    {
        private static RAGE.Ui.HtmlWindow window = null;
        public pcu()
        {
            Events.Add("abrirpcu", OpenAdsBrowser);
            Events.Add("cerrarpcu", CloseAdsBrowser);
        }


        private void OpenAdsBrowser(object[] args)
        {
            if (window != null) window.Destroy();
            window = new RAGE.Ui.HtmlWindow("package://statics/tablet/ads.html");
            window.ExecuteJs("setURL(\"" + args[0] + "\");");
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
