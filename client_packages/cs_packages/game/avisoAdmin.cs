using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.NUI;

namespace DowntownRP_cs.game
{
    public class avisoAdmin : Events.Script
    {
        private bool isOpen = false;
        private MenuPool mp;
        public avisoAdmin()
        {
            Events.Add("TiposAyudaStaff", TiposAyudaStaff);
            RAGE.Events.Tick += DrawMenu;
        }

        public void TiposAyudaStaff( object [] args)
        {
            isOpen = true;
            Chat.Show(false);
            RAGE.Ui.Cursor.Visible = false;

            mp = new MenuPool();

            UIMenu AyudaMenu = new UIMenu("Ayuda staf", "Indica que tipo de miembro del staff necesitas.");

            mp.Add(AyudaMenu);

            UIMenuItem mod = new UIMenuItem("Moderador");
                mod.Description = "Para situaciones en las que un rol no sea acorde a la normativa";
                mod.Activated += (sender, item) =>
                {
                    AyudaMenu.Visible = false;
                    RAGE.Ui.Cursor.Visible = false;
                    RAGE.Chat.Show(true);
                };
            UIMenuItem dev = new UIMenuItem("Desarrollador");
                dev.Description = "Para reportar bugs o glitches que NO IMPLIQUEN devolución de item";
                dev.Activated += (sender, item) =>
                {
                    AyudaMenu.Visible = false;
                    RAGE.Ui.Cursor.Visible = false;
                    RAGE.Chat.Show(true);
                };
            UIMenuItem manager = new UIMenuItem("Manager");
                manager.Activated += (sender, item) =>
                {
                    AyudaMenu.Visible = false;
                    RAGE.Ui.Cursor.Visible = false;
                    RAGE.Chat.Show(true);
                };
                manager.Description = "Para solucionar problemas relativos a negocios, propiedades ...";

            AyudaMenu.AddItem(mod);
            AyudaMenu.AddItem(dev);
            AyudaMenu.AddItem(manager);

            mp.RefreshIndex();
            AyudaMenu.Visible = true;
            AyudaMenu.OnMenuClose += (sender) => { RAGE.Chat.Show(true); }
                ;
        }

        private void DrawMenu(List<Events.TickNametagData> nametags)
        {
            if (isOpen) mp.ProcessMenus();
        }

    }
}
