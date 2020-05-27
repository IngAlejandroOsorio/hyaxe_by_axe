using RAGE;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game
{
    public class Boombox : Events.Script
    {
        private bool isOpen = false;
        private MenuPool _menuPool;
        public Boombox()
        {
            Events.Add("OpenBoomboxMenu", OpenBoomboxMenu);
            RAGE.Events.Tick += DrawMenu;
        }

        private void OpenBoomboxMenu(object[] args)
        {
            isOpen = true;
            RAGE.Chat.Show(false);

            _menuPool = new MenuPool();
            var mainMenu = new UIMenu("Boombox", "~b~Selecciona una emisora");
            mainMenu.FreezeAllInput = true;

            _menuPool.Add(mainMenu);
            HipHop1(mainMenu);
            HipHop2(mainMenu);
            Reggaeton(mainMenu);
            Trap(mainMenu);
            Edm1(mainMenu);
            Edm2(mainMenu);
            Techno1(mainMenu);
            Techno2(mainMenu);
            PararMusica(mainMenu);
            RecogerBoombox(mainMenu);
            _menuPool.RefreshIndex();
            mainMenu.Visible = true;
            mainMenu.OnItemSelect += MainMenu_OnItemSelect;

            mainMenu.OnMenuClose += MainMenu_OnMenuClose;
        }

        private void HipHop1(UIMenu mainMenu)
        {
            var HipHop1 = new UIMenuItem("HipHop", "Emisora de música hip hop");
            mainMenu.AddItem(HipHop1);
        }

        private void HipHop2(UIMenu mainMenu)
        {
            var HipHop2 = new UIMenuItem("HipHop2", "Emisora de música hip hop");
            mainMenu.AddItem(HipHop2);
        }

        private void Reggaeton(UIMenu mainMenu)
        {
            var Reggaeton = new UIMenuItem("Reggaeton", "Emisora de música reggaeton");
            mainMenu.AddItem(Reggaeton);
        }

        private void Trap(UIMenu mainMenu)
        {
            var boombox = new UIMenuItem("Trap", "Emisora de música trap");
            mainMenu.AddItem(boombox);
        }

        private void Edm1(UIMenu mainMenu)
        {
            var Edm1 = new UIMenuItem("EDM", "Emisora de música EDM");
            mainMenu.AddItem(Edm1);
        }

        private void Edm2(UIMenu mainMenu)
        {
            var Edm2 = new UIMenuItem("EDM2", "Emisora de música EDM");
            mainMenu.AddItem(Edm2);
        }

        private void Techno1(UIMenu mainMenu)
        {
            var Techno1 = new UIMenuItem("Techno", "Emisora de música techno");
            mainMenu.AddItem(Techno1);
        }

        private void Techno2(UIMenu mainMenu)
        {
            var Techno2 = new UIMenuItem("Techno2", "Emisora de música techno");
            mainMenu.AddItem(Techno2);
        }

        private void PararMusica(UIMenu mainMenu)
        {
            var PararMusica = new UIMenuItem("Parar", "Para de reproducir música");
            mainMenu.AddItem(PararMusica);
        }

        private void RecogerBoombox(UIMenu mainMenu)
        {
            var PararMusica = new UIMenuItem("Recoger", "Recoge tu boombox");
            mainMenu.AddItem(PararMusica);
        }

        private void MainMenu_OnMenuClose(UIMenu sender)
        {
            RAGE.Chat.Show(true);
        }

        private void MainMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (selectedItem.Text == "HipHop") Events.CallLocal("startBoombox", 1);
            if (selectedItem.Text == "HipHop2") Events.CallLocal("startBoombox", 2);
            if (selectedItem.Text == "Reggaeton") Events.CallLocal("startBoombox", 3);
            if (selectedItem.Text == "Trap") Events.CallLocal("startBoombox", 4);
            if (selectedItem.Text == "EDM") Events.CallLocal("startBoombox", 5);
            if (selectedItem.Text == "EDM2") Events.CallLocal("startBoombox", 6);
            if (selectedItem.Text == "Techno") Events.CallLocal("startBoombox", 7);
            if (selectedItem.Text == "Techno2") Events.CallLocal("startBoombox", 1);
            if (selectedItem.Text == "Parar") Events.CallLocal("destroyBoombox");
            if (selectedItem.Text == "Recoger") Events.CallRemote("PickUpBoombox");

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
