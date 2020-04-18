using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.factions.md
{
    public class death : Events.Script
    {
        public static RAGE.Ui.HtmlWindow window;
        private static RAGE.Elements.Blip lsmdblip;

        public death()
        {
            Events.Add("OpenDeathUI", OpenDeathUI);
            Events.Add("CloseDeathUI", CloseDeathUI);
            Events.Add("AcceptDeath", AcceptDeath);
            Events.Add("AdviceLSMD", AdviceLSMD);
            Events.Add("AdviceLSMDBlip", AdviceLSMDBlip);
            Events.Add("DestroyAdviceLSMDBlip", DestroyAdviceLSMDBlip);
        }

        private void DestroyAdviceLSMDBlip(object[] args)
        {
            lsmdblip.Destroy();
        }

        private void AdviceLSMDBlip(object[] args)
        {
            Vector3 position = (Vector3)args[0];

            lsmdblip = new RAGE.Elements.Blip(270, position, "Aviso LSMD");
            lsmdblip.SetColour(49);
            lsmdblip.SetFlashesAlternate(true);
            lsmdblip.SetFlashInterval(500);
            lsmdblip.SetFlashTimer(50);
        }

        private void AcceptDeath(object[] args)
        {
            Events.CallRemote("SS_AcceptDeath");
        }

        private void AdviceLSMD(object[] args)
        {
            Events.CallRemote("SS_AdviceLSMD");
        }

        private void OpenDeathUI(object[] args)
        {
            window = new RAGE.Ui.HtmlWindow("package://statics/main/muerte.html");
            RAGE.Ui.Cursor.Visible = true;
        }

        private void CloseDeathUI(object[] args)
        {
            window.Destroy();
            RAGE.Ui.Cursor.Visible = false;
        }
    }
}
