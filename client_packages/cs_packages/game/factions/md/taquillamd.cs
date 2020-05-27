using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.NUI;

namespace DowntownRP_cs.game.factions.pd
{
    public class taquillamd : Events.Script
    {
        private bool isOpen = false;
        private MenuPool mp;
        private UIMenu AyudaMenu;
        public taquillamd()
        {
            Events.Add("tfd", taquilla);
            Events.Tick += DrawMenu;

            mp = new MenuPool();

            AyudaMenu = new UIMenu("Taquilla LSFD", "Escoje la categoria del material que busques");
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

            UIMenuItem ar = new UIMenuItem("Herramientas");
            ar.Activated += (sender, item) =>
            {
                AyudaMenu.Visible = false;
                RAGE.Ui.Cursor.Visible = false;
                RAGE.Chat.Show(true);
                Armas();

            };

            
            AyudaMenu.AddItem(un);
            AyudaMenu.AddItem(ar);
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

            Uniformidades.AddItem(new UIMenuItem("Azul Bombero"));
            Uniformidades.AddItem(new UIMenuItem("Contraincendios"));
            Uniformidades.AddItem(new UIMenuItem("Azul Paramédicos"));
            Uniformidades.AddItem(new UIMenuItem("Blanca"));

            Uniformidades.AddItem(new UIMenuItem("Cerrar", "Cerrar el menú"));
            // Refrescamos los índices
            mp.RefreshIndex();
            Uniformidades.MouseControlsEnabled = false;
            Uniformidades.Visible = true;
        }

        public void SelectUn(UIMenu ui, UIMenuItem item, int index)
        {
            if (index < 4)
            {
                RAGE.Events.CallRemote("RecTaquillaMD", 0, index);
            }
            ui.Visible = false;
            Cerrar();
        }

        public void Armas()
        {
            Chat.Show(false);
            var armas = new UIMenu("Armas", "Armamento básico");
            mp.Add(armas);
            armas.OnItemSelect += SelectAr;

            armas.AddItem(new UIMenuItem("Hacha"));
            armas.AddItem(new UIMenuItem("Hacha de Combate"));
            armas.AddItem(new UIMenuItem("Llave Inglesa"));
            armas.AddItem(new UIMenuItem("Palanca"));
            armas.AddItem(new UIMenuItem("Martillo"));
            armas.AddItem(new UIMenuItem("Linterna", "¿Es de noche?"));
            armas.AddItem(new UIMenuItem("Extintor", "¡FUEGO!"));

            armas.AddItem(new UIMenuItem("Cerrar", "Cerrar el menú"));
            // Refrescamos los índices
            mp.RefreshIndex();
            armas.MouseControlsEnabled = false;
            armas.Visible = true;
        }

        public void SelectAr(UIMenu ui, UIMenuItem item, int index)
        {
            if (index < 7)
            {
                RAGE.Events.CallRemote("RecTaquillaMD", 1, index);
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
