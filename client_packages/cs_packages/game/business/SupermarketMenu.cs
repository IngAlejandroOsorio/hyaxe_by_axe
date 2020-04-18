using RAGE;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.business
{
    public class SupermarketMenu : Events.Script
    {
        private bool isOpen = false;
        private MenuPool _menuPool;

        public SupermarketMenu()
        {
            Events.Add("OpenSupermarketMenu", OpenSupermarketMenu);
            RAGE.Events.Tick += DrawMenu;
        }

        private void DrawMenu(List<Events.TickNametagData> nametags)
        {
            if (isOpen) _menuPool.ProcessMenus();
        }

        private void OpenSupermarketMenu(object[] args)
        {
            isOpen = true;
            RAGE.Chat.Show(false);

            _menuPool = new MenuPool();
            var mainMenu = new UIMenu("Supermercado", "~b~Selecciona un producto");
            mainMenu.FreezeAllInput = true;

            _menuPool.Add(mainMenu);
            MenuItems(mainMenu);
            MenuRecargar(mainMenu);
            _menuPool.RefreshIndex();
            mainMenu.Visible = true;
            mainMenu.OnItemSelect += MainMenu_OnItemSelect;

            mainMenu.OnMenuClose += MainMenu_OnMenuClose;
        }

        private void MainMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (selectedItem.Text == "Productos") return;
            if (selectedItem.Text == "Recargar saldo") Events.CallRemote("throwNotImplemented");

            sender.Visible = false;
            isOpen = false;
            RAGE.Chat.Show(true);
        }

        private void MainMenu_OnMenuClose(UIMenu sender)
        {
            RAGE.Chat.Show(true);
        }

        private void MenuItems(UIMenu mainMenu)
        {
            var submenu = _menuPool.AddSubMenu(mainMenu, "Productos");
            submenu.AddItem(new UIMenuItem("Cigarros", "20 cigarros. ¡Cuidado, que fumar mata! ~g~($20)"));
            submenu.AddItem(new UIMenuItem("Agua", "Una botella de agua. ~g~($20)"));
            submenu.AddItem(new UIMenuItem("Manzana", "¡Aliméntate siempre sano! ~g~($20)"));
            submenu.AddItem(new UIMenuItem("Botiquin", "Kit de primeros auxilios ~g~($20)"));

            submenu.OnItemSelect += Submenu_OnItemSelect;
        }

        private void Submenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            Events.CallRemote("BuyItemSupermarket", selectedItem.Text);
            sender.Visible = false;
            isOpen = false;
            RAGE.Chat.Show(true);
        }

        private void MenuRecargar(UIMenu mainMenu)
        {
            var agua = new UIMenuItem("Recargar saldo", "Recarga el saldo de tu teléfono");
            mainMenu.AddItem(agua);
        }
    }
}
