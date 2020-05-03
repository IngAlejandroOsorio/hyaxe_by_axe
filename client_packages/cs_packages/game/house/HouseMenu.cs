using RAGE;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.house
{
    public class HouseMenu : Events.Script
    {
        private bool isOpen = false;
        private MenuPool _menuPool;
        private static RAGE.Ui.HtmlWindow browser;

        public HouseMenu()
        {
            Events.Add("OpenHouseMenu", OpenHouseMenu);
            RAGE.Events.Tick += DrawMenu;

            Events.Add("OpenSafeBoxHouse", OpenSafeBoxHouse);
            Events.Add("CloseSafeBoxHouse", CloseSafeBoxHouse);
            Events.Add("CloseSafeBoxHouseBtn", CloseSafeBoxHouseBtn);
            Events.Add("DepositarSafeBoxHouse", DepositarSafeBoxHouse);
            Events.Add("RetirarSafeBoxHouse", RetirarSafeBoxHouse);
        }

        private void OpenSafeBoxHouse(object[] args)
        {
            browser = new RAGE.Ui.HtmlWindow("package://statics/house/cajafuerte.html");
            browser.ExecuteJs($"setMoney({(int)args[0]})");

            RAGE.Ui.Cursor.Visible = true;
        }

        private void CloseSafeBoxHouse(object[] args)
        {
            browser.Destroy();
            RAGE.Ui.Cursor.Visible = false;
        }

        private void CloseSafeBoxHouseBtn(object[] args)
        {
            Events.CallLocal("CloseSafeBoxHouse");
        }

        private void DepositarSafeBoxHouse(object[] args)
        {
            Events.CallRemote("SafeBoxHouseDepositar", args[0].ToString());
        }

        private void RetirarSafeBoxHouse(object[] args)
        {
            Events.CallRemote("SafeBoxHouseRetirar", args[0].ToString());
        }

        private void DrawMenu(List<Events.TickNametagData> nametags)
        {
            if (isOpen) _menuPool.ProcessMenus();
        }

        private void OpenHouseMenu(object[] args)
        {
            isOpen = true;
            RAGE.Chat.Show(false);

            _menuPool = new MenuPool();
            var mainMenu = new UIMenu("Casa", "~b~Selecciona una opción");
            mainMenu.FreezeAllInput = true;

            _menuPool.Add(mainMenu);
            ArmarioMenu(mainMenu);
            SafeboxMenu(mainMenu);
            _menuPool.RefreshIndex();
            mainMenu.Visible = true;
            mainMenu.OnItemSelect += MainMenu_OnItemSelect;

            mainMenu.OnMenuClose += MainMenu_OnMenuClose;
        }

        private void SafeboxMenu(UIMenu mainMenu)
        {
            var safebox = new UIMenuItem("Caja fuerte", "Agrega/retira dinero de la caja fuerte");
            mainMenu.AddItem(safebox);
        }

        private void ArmarioMenu(UIMenu mainMenu)
        {
            var armario = new UIMenuItem("Armario", "Guarda/saca items");
            mainMenu.AddItem(armario);
        }

        private void MainMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (selectedItem.Text == "Armario") Events.CallRemote("OpenInventoryHouse");
            else Events.CallRemote("OpenSafeboxHouse");

            sender.Visible = false;
            isOpen = false;
            RAGE.Chat.Show(true);
        }

        private void MainMenu_OnMenuClose(UIMenu sender)
        {
            RAGE.Chat.Show(true);
        }
    }
}
