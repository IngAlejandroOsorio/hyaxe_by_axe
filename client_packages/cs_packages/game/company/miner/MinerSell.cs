using RAGE;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.company.miner
{
    public class MinerSell : Events.Script
    {
        private bool isOpen = false;
        private MenuPool _menuPool;
        public MinerSell()
        {
            Events.Add("OpenMinerSellMenu", OpenMinerSellMenu);
            RAGE.Events.Tick += DrawMenu;
        }

        private void OpenMinerSellMenu(object[] args)
        {
            isOpen = true;
            RAGE.Chat.Show(false);

            _menuPool = new MenuPool();
            var mainMenu = new UIMenu(args[0].ToString(), "~b~Venta de minerales");
            mainMenu.FreezeAllInput = true;

            _menuPool.Add(mainMenu);
            VentaBronce(mainMenu);
            VentaPlata(mainMenu);
            VentaOro(mainMenu);
            VentaDiamante(mainMenu);
            SalirMenu(mainMenu);
            _menuPool.RefreshIndex();
            mainMenu.Visible = true;
            mainMenu.OnItemSelect += MainMenu_OnItemSelect;

            mainMenu.OnMenuClose += MainMenu_OnMenuClose;
        }

        private void SalirMenu(UIMenu mainMenu)
        {
            var salir = new UIMenuItem("Salir", $"Salir del menu");
            mainMenu.AddItem(salir);
        }

        private void VentaBronce(UIMenu mainMenu)
        {
            var bronce = new UIMenuItem("Bronce", $"Cobra por el bronce recogido. Precio por unidad: ~g~$17");
            mainMenu.AddItem(bronce);
        }

        private void VentaPlata(UIMenu mainMenu)
        {
            var plata = new UIMenuItem("Plata", $"Cobra por la plata recogida. Precio por unidad: ~g~$35");
            mainMenu.AddItem(plata);
        }

        private void VentaOro(UIMenu mainMenu)
        {
            var oro = new UIMenuItem("Oro", $"Cobra por el oro recogido. Precio por unidad: ~g~$65");
            mainMenu.AddItem(oro);
        }

        private void VentaDiamante(UIMenu mainMenu)
        {
            var oro = new UIMenuItem("Diamantes", $"Cobra por los diamantes. Precio por unidad: ~g~$180");
            mainMenu.AddItem(oro);
        }

        private void MainMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if(selectedItem.Text == "Salir")
            {
                sender.Visible = false;
                isOpen = false;
                RAGE.Chat.Show(true);
                return;
            }

            Events.CallRemote("SellMinerCompany", selectedItem.Text);
        }

        private void MainMenu_OnMenuClose(UIMenu sender)
        {
            RAGE.Chat.Show(true);
            isOpen = false;
        }

        private void DrawMenu(List<Events.TickNametagData> nametags)
        {
            if (isOpen) _menuPool.ProcessMenus();
        }
    }
}
