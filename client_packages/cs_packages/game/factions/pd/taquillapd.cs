using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.NUI;

namespace DowntownRP_cs.game.factions.pd
{
    public class taquillapd : Events.Script
    {
        private bool isOpen = false;
        private MenuPool mp;
        private UIMenu AyudaMenu;
        public taquillapd()
        {
            Events.Add("tpd", taquilla);
            Events.Tick += DrawMenu;

            mp = new MenuPool();

            AyudaMenu = new UIMenu("Taquilla Policial", "Escoje la categoria del material que busques");
            mp.Add(AyudaMenu);

            UIMenuItem un = new UIMenuItem("Uniformidades");
            un.Description = "Selección rapida";
            un.Activated += (sender, item) =>
            {
                AyudaMenu.Visible = false;
                RAGE.Ui.Cursor.Visible = false;
                RAGE.Chat.Show(true);
                Uniformidades();

            };


            AyudaMenu.AddItem(un);
        }

        public void taquilla(object[] args)
        {
            isOpen = true;
            Chat.Show(false);
            RAGE.Ui.Cursor.Visible = false;

            mp.RefreshIndex();
            AyudaMenu.Visible = true;
            AyudaMenu.OnMenuClose += (sender) => { Cerrar(); };
        }


        private void DrawMenu(List<Events.TickNametagData> nametags)
        {
            if (isOpen) mp.ProcessMenus();
        }


        public void Uniformidades()
        {
            Chat.Show(false);
            var Uniformidades = new UIMenu("Uniformidades", "Seleccion rápida");
            mp.Add(Uniformidades);
            Uniformidades.OnItemSelect += SelectUn;

            Uniformidades.AddItem(new UIMenuItem("A", "Uniformidad manga larga con corbata"));
            Uniformidades.AddItem(new UIMenuItem("B", "Uniformidad manga larga sin corbata"));
            Uniformidades.AddItem(new UIMenuItem("B-M", "Uniformidad manga larga para personal motorista"));
            Uniformidades.AddItem(new UIMenuItem("C", "Uniformidad manga corta "));
            Uniformidades.AddItem(new UIMenuItem("C-M", "Uniformidad manga corta para personal motorista"));

            Uniformidades.AddItem(new UIMenuItem("Cerrar", "Cerrar el menú"));
            // Refrescamos los índices
            mp.RefreshIndex();
            Uniformidades.MouseControlsEnabled = false;
            Uniformidades.Visible = true;
        }

        public void SelectUn(UIMenu ui, UIMenuItem item, int index)
        {
            if (index < 5)
            {
                RAGE.Events.CallRemote("RecTaquillaPD", 0, index);
            }
            ui.Visible = false;
            Cerrar();
        }

       

        public void Cerrar()
        {
            isOpen = false;
            Chat.Show(true);
            RAGE.Ui.Cursor.Visible = true;
        }
    }
}
