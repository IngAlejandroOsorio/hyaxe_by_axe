using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.vehicles
{
    public class VehicleMenu : Events.Script
    {
        private static RAGE.Ui.HtmlWindow window;
        public VehicleMenu()
        {
            Events.Add("OpenVehicleMenu", OpenVehicleMenu);
            Events.Add("CloseVehicleMenu", CloseVehicleMenu);
            Events.Add("SeatbellOn", SeatbellOn);
            Events.Add("SeatbellOff", SeatbellOff);
            Events.Add("CapoOn", CapoOn);
            Events.Add("CapoOff", CapoOff);
            Events.Add("MaleteroOn", MaleteroOn);
            Events.Add("MaleteroOff", MaleteroOff);
            Events.Add("LockOn", LockOn);
            Events.Add("LockOff", LockOff);            
        }


        private void SeatbellOn(object[] args)
        {
            Events.CallRemote("SS_SeatbellOn");
        }

        private void SeatbellOff(object[] args)
        {
            Events.CallRemote("SS_SeatbellOff");
        }

        private void CapoOn(object[] args)
        {
            Events.CallRemote("SS_CapoOn");
        }

        private void CapoOff(object[] args)
        {
            Events.CallRemote("SS_CapoOff");
        }

        private void MaleteroOn(object[] args)
        {
            Events.CallRemote("SS_MaleteroOn");
        }

        private void MaleteroOff(object[] args)
        {
            Events.CallRemote("SS_MaleteroOff");
        }

        private void LockOn(object[] args)
        {
            Events.CallRemote("SS_LockOn");
        }

        private void LockOff(object[] args)
        {
            Events.CallRemote("SS_LockOff");
        }

        private void OpenVehicleMenu(object[] args)
        {
            window = new RAGE.Ui.HtmlWindow("package://statics/main/vehiclemenu.html");
            RAGE.Ui.Cursor.Visible = true;
        }

        private void CloseVehicleMenu(object[] args)
        {
            window.Destroy();
            RAGE.Ui.Cursor.Visible = false;
        }
    }
}
