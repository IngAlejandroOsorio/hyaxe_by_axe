using Newtonsoft.Json;
using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.vehicles
{
    public class VehicleInventory : Events.Script
    {
        private static RAGE.Ui.HtmlWindow invBrowser;
        public VehicleInventory()
        {
            Events.Add("OpenVehicleInventory", OpenVehicleInventory);
            Events.Add("CloseVehicleInventory", CloseVehicleInventory);
            Events.Add("CloseVehicleInventoryButton", CloseVehicleInventoryButton);
            Events.Add("MoveItemFromInv", MoveItemFromInv);
            Events.Add("MoveItemToInv", MoveItemToInv);
        }

        private void OpenVehicleInventory(object[] args)
        {
            data.Inventory pjInv = JsonConvert.DeserializeObject<data.Inventory>(args[0] as string);
            data.Inventory vehInv = JsonConvert.DeserializeObject<data.Inventory>(args[1] as string);

            invBrowser = new RAGE.Ui.HtmlWindow("package://statics/inventory/twelve.html");
            RAGE.Ui.Cursor.Visible = true;

            invBrowser.ExecuteJs($"setInventoryItems('{pjInv.slot1.name}', '{pjInv.slot2.name}', '{pjInv.slot3.name}', '{pjInv.slot4.name}', '{pjInv.slot5.name}', '{pjInv.slot6.name}', '{pjInv.slot7.name}', '{pjInv.slot8.name}', '{pjInv.slot9.name}', '{pjInv.slot10.name}', '{pjInv.slot11.name}', '{pjInv.slot12.name}');");
            invBrowser.ExecuteJs($"setOtherItems('{vehInv.slot1.name}', '{vehInv.slot2.name}', '{vehInv.slot3.name}', '{vehInv.slot4.name}', '{vehInv.slot5.name}', '{vehInv.slot6.name}', '{vehInv.slot7.name}', '{vehInv.slot8.name}', '{vehInv.slot9.name}', '{vehInv.slot10.name}', '{vehInv.slot11.name}', '{vehInv.slot12.name}');");
        }

        private void CloseVehicleInventory(object[] args)
        {
            invBrowser.Destroy();
            RAGE.Ui.Cursor.Visible = false;
        }

        private void CloseVehicleInventoryButton(object[] args)
        {
            Events.CallRemote("OpenTruckInventory");
        }

        private void MoveItemFromInv(object[] args)
        {
            Events.CallRemote("ItemFromInvMove", (int)args[0]);
        }

        private void MoveItemToInv(object[] args)
        {
            Events.CallRemote("ItemToInvMove", (int)args[0]);
        }
    }
}
