using RAGE;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.business
{
    public class GrowShopMenu : Events.Script
    {
        private bool isOpen = false;
        private MenuPool _menuPool;
        public GrowShopMenu()
        {
            Events.Add("OpenGrowShopMenu", OpenGrowShopMenu);
            RAGE.Events.Tick += DrawMenu;
        }

        private void OpenGrowShopMenu(object[] args)
        {
            isOpen = true;
            RAGE.Chat.Show(false);

            _menuPool = new MenuPool();
            var mainMenu = new UIMenu("GrowShop", "~b~Selecciona un producto");
            mainMenu.FreezeAllInput = true;

            _menuPool.Add(mainMenu);
            AmnesiaHaze(mainMenu);
            MobyDick(mainMenu);
            OGKush(mainMenu);
            Blueberry(mainMenu);
            Blackdomina(mainMenu);
            Cheese(mainMenu);
            Crecimiento(mainMenu);
            Floracion(mainMenu);
            _menuPool.RefreshIndex();
            mainMenu.Visible = true;
            mainMenu.OnItemSelect += MainMenu_OnItemSelect;

            mainMenu.OnMenuClose += MainMenu_OnMenuClose;
        }

        private void AmnesiaHaze(UIMenu mainMenu)
        {
            var boombox = new UIMenuItem("Amnesia Haze", "Semilla de Amnesia Haze. 70% sativa 30% indica ($30)");
            mainMenu.AddItem(boombox);
        }

        private void MobyDick(UIMenu mainMenu)
        {
            var movil = new UIMenuItem("Moby Dick", "Semilla de Amnesia Haze. 60% sativa 40% indica ($30)");
            mainMenu.AddItem(movil);
        }

        private void OGKush(UIMenu mainMenu)
        {
            var movil = new UIMenuItem("OG Kush", "Semilla de OG Kush. 25% sativa 75% indica ($30)");
            mainMenu.AddItem(movil);
        }

        private void Blueberry(UIMenu mainMenu)
        {
            var movil = new UIMenuItem("Blueberry", "Semilla de Blueberry. 20% sativa 80% indica ($30)");
            mainMenu.AddItem(movil);
        }

        private void Blackdomina(UIMenu mainMenu)
        {
            var movil = new UIMenuItem("Black domina", "Semilla de Black domina. 5% sativa 95% indica ($30)");
            mainMenu.AddItem(movil);
        }

        private void Cheese(UIMenu mainMenu)
        {
            var movil = new UIMenuItem("Cheese", "Semilla de Cheese. 40% sativa 60% indica ($30)");
            mainMenu.AddItem(movil);
        }

        private void Crecimiento(UIMenu mainMenu)
        {
            var movil = new UIMenuItem("Crecimiento organico", "Crecimiento orgánico para tu planta (x1) ($15)");
            mainMenu.AddItem(movil);
        }

        private void Floracion(UIMenu mainMenu)
        {
            var movil = new UIMenuItem("Floracion organica", "Floración orgánica para tu planta (x1) ($30)");
            mainMenu.AddItem(movil);
        }

        private void MainMenu_OnMenuClose(UIMenu sender)
        {
            RAGE.Chat.Show(true);
        }

        private void MainMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (selectedItem.Text == "Amnesia Haze") Events.CallRemote("BuyGrowShop", "Amnesia Haze");
            if (selectedItem.Text == "Moby Dick") Events.CallRemote("BuyGrowShop", "Moby Dick");
            if (selectedItem.Text == "OG Kush") Events.CallRemote("BuyGrowShop", "OG Kush");
            if (selectedItem.Text == "Blueberry") Events.CallRemote("BuyGrowShop", "Blueberry");
            if (selectedItem.Text == "Black domina") Events.CallRemote("BuyGrowShop", "Black domina");
            if (selectedItem.Text == "Cheese") Events.CallRemote("BuyGrowShop", "Cheese");
            if (selectedItem.Text == "Crecimiento organico") Events.CallRemote("BuyGrowShop", "Crecimiento organico");
            if (selectedItem.Text == "Floracion organica") Events.CallRemote("BuyGrowShop", "Floracion organica");

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
