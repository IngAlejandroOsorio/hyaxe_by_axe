using RAGE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP_cs.game.company.taxi
{
    public class Taximeter : Events.Script
    {
        private static RAGE.Ui.HtmlWindow browser = null;
        private static bool isDriver = true;

        public Taximeter()
        {
            Events.Add("StartTaximeter", StartTaximeter);
            Events.Add("StopTaximeter", StopTaximeter);
            Events.Add("UpdateCounterTaxi", UpdateCounterTaxi);
        }

        private void UpdateCounterTaxi(object[] args)
        {
            if (!isDriver) Events.CallRemote("ChargeTaxiRide");
        }

        private void StartTaximeter(object[] args)
        {
            if ((int)args[0] == 1) isDriver = false;
            else isDriver = true;

            browser = new RAGE.Ui.HtmlWindow("package://statics/main/taximetro.html");
            browser.ExecuteJs("addMorecosto();");
        }

        private void StopTaximeter(object[] args)
        {
            browser.Destroy();
        }
    }
}
