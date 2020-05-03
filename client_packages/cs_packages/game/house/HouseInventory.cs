using Newtonsoft.Json;
using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.house
{
    public class HouseInventory : Events.Script
    {
        private static RAGE.Ui.HtmlWindow invBrowser;
        public HouseInventory()
        {
            Events.Add("OpenHouseInventory", OpenHouseInventory);
            Events.Add("CloseHouseInventory", CloseHouseInventory);
            Events.Add("CloseHouseInventoryButton", CloseHouseInventoryButton);
            Events.Add("MoveItemFromInvH", MoveItemFromInv);
            Events.Add("MoveItemToInvH", MoveItemToInv);
        }

        private void OpenHouseInventory(object[] args)
        {
            data.Inventory pjInv = JsonConvert.DeserializeObject<data.Inventory>(args[0] as string);
            data.HouseInventory vehInv = JsonConvert.DeserializeObject<data.HouseInventory>(args[1] as string);

            invBrowser = new RAGE.Ui.HtmlWindow("package://statics/inventory/sixteen.html");
            RAGE.Ui.Cursor.Visible = true;

            invBrowser.ExecuteJs($"setInventoryItems('{pjInv.slot1.name}', '{pjInv.slot2.name}', '{pjInv.slot3.name}', '{pjInv.slot4.name}', '{pjInv.slot5.name}', '{pjInv.slot6.name}', '{pjInv.slot7.name}', '{pjInv.slot8.name}', '{pjInv.slot9.name}', '{pjInv.slot10.name}', '{pjInv.slot11.name}', '{pjInv.slot12.name}');");
            invBrowser.ExecuteJs($"setOtherItems('{vehInv.slot1.name}', '{vehInv.slot2.name}', '{vehInv.slot3.name}', '{vehInv.slot4.name}', '{vehInv.slot5.name}', '{vehInv.slot6.name}', '{vehInv.slot7.name}', '{vehInv.slot8.name}', '{vehInv.slot9.name}', '{vehInv.slot10.name}', '{vehInv.slot11.name}', '{vehInv.slot12.name}', '{vehInv.slot13.name}', '{vehInv.slot14.name}', '{vehInv.slot15.name}', '{vehInv.slot16.name}');");
        }

        private void CloseHouseInventory(object[] args)
        {
            invBrowser.Destroy();
            RAGE.Ui.Cursor.Visible = false;
        }

        private void CloseHouseInventoryButton(object[] args)
        {
            invBrowser.Destroy();
            RAGE.Ui.Cursor.Visible = false;
        }

        private void MoveItemFromInv(object[] args)
        {
            Events.CallRemote("ItemFromInvMoveH", (int)args[0]);
        }

        private void MoveItemToInv(object[] args)
        {
            Events.CallRemote("ItemToInvMoveH", (int)args[0]);
        }
    }
}
