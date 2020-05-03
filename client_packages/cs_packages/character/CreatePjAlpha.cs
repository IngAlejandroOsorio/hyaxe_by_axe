using RAGE;
using RAGE.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.character
{
    public class CreatePjAlpha : Events.Script
    {
        private static RAGE.Ui.HtmlWindow browser;
        public CreatePjAlpha()
        {
            Events.Add("CreatePjAlphaEvent", CreatePjAlphaEvent);
            Events.Add("FinishPjAlphaEvent", FinishPjAlphaEvent);
            Events.Add("FinishAlphaCreationPj", FinishAlphaCreationPj);
            Events.Add("LoadCharacterFace", LoadCharacterFace);
        }

        private void LoadCharacterFace(object[] args)
        {
            Events.CallRemote("playerLoadCharacter");
        }

        private void FinishAlphaCreationPj(object[] args)
        {
            Events.CallRemote("PjFinishAlphaCreation");
        }

        private void FinishPjAlphaEvent(object[] args)
        {
            browser.Destroy();
            RAGE.Ui.Cursor.Visible = false;

            RAGE.Elements.Player.LocalPlayer.FreezePosition(false);
            Cam.RenderScriptCams(false, false, 0, true, false, 0);

            Events.CallRemote("PjAlphaCreationFinish", args[0].ToString());
            Events.CallRemote("creatorStart");
        }

        private void CreatePjAlphaEvent(object[] args)
        {
            browser = new RAGE.Ui.HtmlWindow("package://statics/login/pj.html");
            RAGE.Ui.Cursor.Visible = true;
        }
    }
}
