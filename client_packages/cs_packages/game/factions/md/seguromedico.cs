using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.factions.md
{
    public class seguromedico : Events.Script
    {
        private static RAGE.Ui.HtmlWindow window;
        public seguromedico()
        {
            Events.Add("OpenSeguroMedico", OpenSeguroMedico);
            Events.Add("CloseSeguroMedico", CloseSeguroMedico);
            Events.Add("BuySeguroMedico", BuySeguroMedico);
        }

        private void OpenSeguroMedico(object[] args)
        {
            window = new RAGE.Ui.HtmlWindow("package://statics/main/hospital.html");
            RAGE.Ui.Cursor.Visible = true;
        }

        private void CloseSeguroMedico(object[] args)
        {
            window.Destroy();
            RAGE.Ui.Cursor.Visible = false;
        }

        private void BuySeguroMedico(object[] args)
        {
            Events.CallRemote("RS_BuySeguroMedico");
        }
    }
}
