using RAGE;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.factions
{
    public class HQMenu : Events.Script
    {
        private bool isOpen = false;
        private MenuPool _menuPool;
        private static RAGE.Ui.HtmlWindow browser;

        public HQMenu()
        {
            Events.Add("OpenFactionMenu", OpenFactionMenu);
            RAGE.Events.Tick += DrawMenu;

            Events.Add("OpenSafeBoxFaction", OpenSafeBoxFaction);
            Events.Add("CloseSafeBoxFaction", CloseSafeBoxFaction);
            Events.Add("CloseSafeBoxFactionBtn", CloseSafeBoxFactionBtn);
            Events.Add("DepositarSafeBoxFaction", DepositarSafeBoxFaction);
            Events.Add("RetirarSafeBoxFaction", RetirarSafeBoxFaction);
        }

        private void OpenSafeBoxFaction(object[] args)
        {
            browser = new RAGE.Ui.HtmlWindow("package://statics/faction/cajafuerte.html");
            browser.ExecuteJs($"setMoney({(int)args[0]})");

            RAGE.Ui.Cursor.Visible = true;
        }

        private void CloseSafeBoxFaction(object[] args)
        {
            browser.Destroy();
            RAGE.Ui.Cursor.Visible = false;
        }

        private void CloseSafeBoxFactionBtn(object[] args)
        {
            Events.CallLocal("CloseSafeBoxFaction");
        }

        private void DepositarSafeBoxFaction(object[] args)
        {
            Events.CallRemote("SafeBoxFactionDepositar", args[0].ToString());
        }

        private void RetirarSafeBoxFaction(object[] args)
        {
            Events.CallRemote("SafeBoxFactionRetirar", args[0].ToString());
        }

        private void DrawMenu(List<Events.TickNametagData> nametags)
        {
            if (isOpen) _menuPool.ProcessMenus();
        }

        private void OpenFactionMenu(object[] args)
        {
            isOpen = true;
            RAGE.Chat.Show(false);

            _menuPool = new MenuPool();
            var mainMenu = new UIMenu("HQ", "~b~Selecciona una opción");
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
            else Events.CallRemote("OpenSafeboxFaction");

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
