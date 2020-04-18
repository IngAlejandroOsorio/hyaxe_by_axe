using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.NUI;

namespace DowntownRP_cs.game.business
{
    public class FuelMenu
            : RAGE.Events.Script
    {
        private int litrosFull = 0;
        private int dineroFull = 0;
        private bool isOpen = false;

        public FuelMenu()
        {
            Events.Add("OpenGasoilMenu", OpenGasoilMenu);
            RAGE.Events.Tick += DrawMenu;
        }

        private void OpenGasoilMenu(object[] args)
        {
            litrosFull = (int)args[0];
            dineroFull = (int)args[1];
            isOpen = true;

            RAGE.Chat.Show(false);

            _menuPool = new MenuPool();
            var mainMenu = new UIMenu("Gasolinera", "~b~¿Cuanto quieres repostar?");
            mainMenu.FreezeAllInput = true;

            _menuPool.Add(mainMenu);
            LlenarTanque(mainMenu);
            _menuPool.RefreshIndex();
            mainMenu.Visible = true;

            mainMenu.OnMenuClose += MainMenu_OnMenuClose;
        }

        private void MainMenu_OnMenuClose(UIMenu sender)
        {
            RAGE.Chat.Show(true);
        }

        private MenuPool _menuPool;

        public void LlenarTanque(UIMenu menu)
        {
            var newitem = new UIMenuItem($"Llenar tanque", $"{litrosFull} litros de gasolina para tu vehículo (~g~${dineroFull}~s~)");
            menu.AddItem(newitem);
            menu.OnItemSelect += Menu_OnItemSelect;
        }

        private void Menu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            Events.CallRemote("PlayerPayFuel");

            sender.Visible = false;
            isOpen = false;
            RAGE.Chat.Show(true);
        }

        public void DrawMenu(System.Collections.Generic.List<RAGE.Events.TickNametagData> nametags)
        {
            if (isOpen) _menuPool.ProcessMenus();
        }
    }
}
