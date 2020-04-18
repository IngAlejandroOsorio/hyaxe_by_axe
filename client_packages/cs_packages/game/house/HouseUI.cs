using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.house
{
    public class HouseUI : Events.Script
    {
        private static RAGE.Ui.HtmlWindow window;
        public HouseUI()
        {
            Events.Add("OpenBuyHouseUI", OpenBuyHouseUI);
            Events.Add("CloseBuyMenuUI", CloseBuyMenuUI);
            Events.Add("BuyHouseFromMenu", BuyHouseFromMenu);
            Events.Add("VisitHouseFromMenu", VisitHouseFromMenu);
            Events.Add("CloseBuyUIFromMenu", CloseBuyUIFromMenu);
        }

        private void VisitHouseFromMenu(object[] args)
        {
            Events.CallRemote("SS_VisitHouseMenu");
        }

        private void CloseBuyUIFromMenu(object[] args)
        {
            Events.CallRemote("SS_CloseBuyHouseMenu");
        }

        private void OpenBuyHouseUI(object[] args)
        {
            window = new RAGE.Ui.HtmlWindow("package://statics/house/venta.html");
            RAGE.Ui.Cursor.Visible = true;
            window.ExecuteJs($"setHousesInfo('{args[0].ToString()}', '{args[1].ToString()}', '{args[2].ToString()}')");
        }

        private void CloseBuyMenuUI(object[] args)
        {
            window.Destroy();
            RAGE.Ui.Cursor.Visible = false;
        }

        private void BuyHouseFromMenu(object[] args)
        {
            Events.CallRemote("SS_BuyHouse");
        }
    }
}
