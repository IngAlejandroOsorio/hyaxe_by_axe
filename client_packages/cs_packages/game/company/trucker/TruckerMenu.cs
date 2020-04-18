using RAGE;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.company.trucker
{
    public class TruckerMenu : Events.Script
    {
        private bool isOpen = false;
        private MenuPool _menuPool;

        public TruckerMenu()
        {
            Events.Add("StartTruckerJob", StartTruckerJob);
            RAGE.Events.Tick += DrawMenu;
        }

        private void DrawMenu(List<Events.TickNametagData> nametags)
        {
            if (isOpen) _menuPool.ProcessMenus();
        }

        private void StartTruckerJob(object[] args)
        {
            isOpen = true;
            RAGE.Chat.Show(false);

            _menuPool = new MenuPool();
            var mainMenu = new UIMenu(args[0].ToString(), "~b~Empieza un recorrido");
            mainMenu.FreezeAllInput = true;

            _menuPool.Add(mainMenu);
            SupermarketRace(mainMenu);
            ClothesRace(mainMenu);
            ElectronicRace(mainMenu);
            _menuPool.RefreshIndex();
            mainMenu.Visible = true;
            mainMenu.OnItemSelect += MainMenu_OnItemSelect;

            mainMenu.OnMenuClose += MainMenu_OnMenuClose;
        }

        private void SupermarketRace(UIMenu mainMenu)
        {
            var supermarket = new UIMenuItem("Alimentos", "Entrega alimentos a un supermercado (las ganancias dependen de la distancia del recorrido)");
            mainMenu.AddItem(supermarket);
        }

        private void ClothesRace(UIMenu mainMenu)
        {
            var ropa = new UIMenuItem("Ropa", "Entrega mercancía a una tienda de ropa (las ganancias dependen de la distancia del recorrido)");
            mainMenu.AddItem(ropa);
        }

        private void ElectronicRace(UIMenu mainMenu)
        {
            var electronic = new UIMenuItem("Electronica", "Entrega mercancía a una tienda de electrónica (las ganancias dependen de la distancia del recorrido)");
            mainMenu.AddItem(electronic);
        }

        private void MainMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (selectedItem.Text == "Ropa" || selectedItem.Text == "Electronica") Events.CallRemote("throwNotImplemented");
            else Events.CallRemote("TruckerStartRace", 1);

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
