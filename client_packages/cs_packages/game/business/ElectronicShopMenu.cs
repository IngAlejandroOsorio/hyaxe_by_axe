using RAGE;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.business
{
    public class ElectronicShopMenu : Events.Script
    {
        private bool isOpen = false;
        private MenuPool _menuPool;
        public ElectronicShopMenu()
        {
            Events.Add("OpenElectronicShopMenu", OpenElectronicShopMenu);
            RAGE.Events.Tick += DrawMenu;
        }

        private void OpenElectronicShopMenu(object[] args)
        {
            isOpen = true;
            RAGE.Chat.Show(false);

            _menuPool = new MenuPool();
            var mainMenu = new UIMenu("Electronica", "~b~Selecciona un producto");
            mainMenu.FreezeAllInput = true;

            _menuPool.Add(mainMenu);
            ComprarMovil(mainMenu);
            ComprarBoombox(mainMenu);
            _menuPool.RefreshIndex();
            mainMenu.Visible = true;
            mainMenu.OnItemSelect += MainMenu_OnItemSelect;

            mainMenu.OnMenuClose += MainMenu_OnMenuClose;
        }

        private void ComprarBoombox(UIMenu mainMenu)
        {
            var boombox = new UIMenuItem("Boombox", "Reproduce música en cualquier lugar ($500)");
            mainMenu.AddItem(boombox);
        }

        private void ComprarMovil(UIMenu mainMenu)
        {
            var movil = new UIMenuItem("Movil", "Comunícate con cualquier persona en cualquier lugar ($100)");
            mainMenu.AddItem(movil);
        }

        private void MainMenu_OnMenuClose(UIMenu sender)
        {
            RAGE.Chat.Show(true);
        }

        private void MainMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (selectedItem.Text == "Boombox") Events.CallRemote("BuyBoombox");
            if (selectedItem.Text == "Movil") Events.CallRemote("BuyMobilePhone");

            sender.Visible = false;
            isOpen = false;

            RAGE.Chat.Show(true);
        }

        private void DrawMenu(List<Events.TickNametagData> nametags)
        {
            if (isOpen) _menuPool.ProcessMenus();
        }
    }
}
