using RAGE;
using RAGE.NUI;
using System.Collections.Generic;
    

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

            UIMenuItem ar = new UIMenuItem("Armamento");
            ar.Description = "Armas de Uso diario";
            ar.Activated += (sender, item) =>
            {
                AyudaMenu.Visible = false;
                RAGE.Ui.Cursor.Visible = false;
                RAGE.Chat.Show(true);
                Armas();

            };


            AyudaMenu.AddItem(un);
            AyudaMenu.AddItem(ar);
            AyudaMenu.OnMenuClose += (sender) => { Cerrar(); };
        }

        public void taquilla(object[] args)
        {
            isOpen = true;
            Chat.Show(false);
            RAGE.Ui.Cursor.Visible = false;

            mp.RefreshIndex();
            AyudaMenu.Visible = true;
           
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
            Uniformidades.AddItem(new UIMenuItem("Paisano", "Para detectives, principalmente, puedes ponerte la placa con /placa"));
            Uniformidades.AddItem(new UIMenuItem("Metropolitana SWAT", "Uniformidad táctica (de momento con el chaleco, pero es temporal)"));
            Uniformidades.AddItem(new UIMenuItem("Metropolitana TEDAX", "Uniformidad para Tecnicos en Desactivación de artefactos explosivos"));

            Uniformidades.AddItem(new UIMenuItem("Cerrar", "Cerrar el menú"));
            // Refrescamos los índices
            mp.RefreshIndex();
            Uniformidades.MouseControlsEnabled = false;
            Uniformidades.Visible = true;
            Uniformidades.OnMenuClose += (sender) => { Cerrar(); };
        }

        public void SelectUn(UIMenu ui, UIMenuItem item, int index)
        {
            if (index < 8)
            {
                RAGE.Events.CallRemote("RecTaquillaPD", 0, index);
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

            armas.AddItem(new UIMenuItem("Pistola", "Pistola con 3 cargadores (36 balas)"));
            armas.AddItem(new UIMenuItem("Pistola de Combate", "Pistola con 3 cargadores (36 balas) Para oficial III en adelante"));
            armas.AddItem(new UIMenuItem("Pistola Compacta", "Pistola con 3 cargadores (16 balas) Para Detectives en adelante"));
            armas.AddItem(new UIMenuItem("Taser", "Arma electrica"));
            armas.AddItem(new UIMenuItem("Porra", "Para pulir manifestantes"));
            armas.AddItem(new UIMenuItem("Linterna", "¿Es de noche?"));

            armas.AddItem(new UIMenuItem("Cerrar", "Cerrar el menú"));

            armas.OnMenuClose += (sender) => { Cerrar(); };
            // Refrescamos los índices
            mp.RefreshIndex();
            armas.MouseControlsEnabled = false;
            armas.Visible = true;
        }

        public void SelectAr(UIMenu ui, UIMenuItem item, int index)
        {
            if (index < 6)
            {
                RAGE.Events.CallRemote("RecTaquillaPD", 1, index);
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
