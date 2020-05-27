using RAGE;
using RAGE.NUI;
using System.Collections.Generic;

namespace DowntownRP_cs.game.factions.pd
{
    public class armeria : Events.Script
    {
        private bool isOpen = false;
        private MenuPool mp;
        private UIMenu esc;
        private UIMenu cortas;
        private UIMenu misc;
        private UIMenu subf;

        public armeria()
        {
            Events.Add("armeriapd", armeriapd);
            RAGE.Events.Tick += DrawMenu;
        }

        public void armeriapd (object [] args)
        {
            isOpen = true;
            Chat.Show(false);
            RAGE.Ui.Cursor.Visible = false;

            mp = new MenuPool();

            UIMenu AyudaMenu = new UIMenu("Taquilla Policial", "Escoje la categoria del material que busques");
            mp.Add(AyudaMenu);

            UIMenuItem ar= new UIMenuItem("Armas cortas");
            ar.Description = "Pistolas, revolveres y tasers";
            ar.Activated += (sender, item) =>
            {
                AyudaMenu.Visible = false;
                RAGE.Ui.Cursor.Visible = false;
                RAGE.Chat.Show(true);
                Cortas();

            };
            UIMenuItem subfu = new UIMenuItem("Subfusiles");
            subfu.Description = "El ayuntamiento está negociando la compra de otros.";
            subfu.Activated += (sender, item) =>
            {
                AyudaMenu.Visible = false;
                RAGE.Ui.Cursor.Visible = false;
                RAGE.Chat.Show(true);
                Subfusiles();
            };
            AyudaMenu.AddItem(subfu);

            UIMenuItem esco = new UIMenuItem("Escopetas", "Armas largas para patrullaje semiordinario.");
            esco.Activated += (sender, item) =>
             {
                 AyudaMenu.Visible = false;
                 RAGE.Ui.Cursor.Visible = false;
                 RAGE.Chat.Show(true);
                 Escopetas();
             };
            AyudaMenu.AddItem(esco);
            

            UIMenuItem rif = new UIMenuItem("Rifles");
            rif.Activated += (sender, item) =>
            {
                AyudaMenu.Visible = false;
                RAGE.Ui.Cursor.Visible = false;
                RAGE.Chat.Show(true);
                Rifles();
            };
            AyudaMenu.AddItem(rif);

            UIMenuItem fus = new UIMenuItem("Fusiles");
            fus.Activated += (sender, item) =>
            {
                AyudaMenu.Visible = false;
                RAGE.Ui.Cursor.Visible = false;
                RAGE.Chat.Show(true);
                Fusiles();
            };
            AyudaMenu.AddItem(fus);

            UIMenuItem expl = new UIMenuItem("Explosivos");
            expl.Activated += (sender, item) =>
            {
                AyudaMenu.Visible = false;
                RAGE.Ui.Cursor.Visible = false;
                RAGE.Chat.Show(true);
                Expl();
            };
            AyudaMenu.AddItem(expl);

            UIMenuItem otr = new UIMenuItem("Otros Objectos");
            otr.Description = "Defensas, linternas ...";
            otr.Activated += (sender, item) =>
            {
                AyudaMenu.Visible = false;
                RAGE.Ui.Cursor.Visible = false;
                RAGE.Chat.Show(true);
                Otr();
            };
           

            AyudaMenu.AddItem(ar);
            AyudaMenu.AddItem(otr);

            mp.RefreshIndex();
            AyudaMenu.Visible = true;
            AyudaMenu.OnMenuClose += (sender) => { Cerrar(); };
        }


        private void DrawMenu(List<Events.TickNametagData> nametags)
        {
            if (isOpen) mp.ProcessMenus();
        }


        public void Cortas()
        {
            Chat.Show(false);
            cortas = new UIMenu("Armas cortas", "Pistolas, revolveres y tasers");
            mp.Add(cortas);
            cortas.OnItemSelect += SelectCortas;

            cortas.AddItem(new UIMenuItem("Pistola", "Pistola normal con 36 balas (3 cargadores)"));
            cortas.AddItem(new UIMenuItem("Pistola de Combate", "Pistola de Combate con 36 balas (3 cargadores)"));
            cortas.AddItem(new UIMenuItem("Pistola compacta", "Arma pequeña para Detectives o personal de paísano 18 balas (3 cargadores)"));
            cortas.AddItem(new UIMenuItem("Taser", "Arma electrica de baja letalidad y muy dolorosa"));
            cortas.AddItem(new UIMenuItem("Pistola AP", "WIP"));
            cortas.AddItem(new UIMenuItem("Pistola 50", "WIP"));
            cortas.AddItem(new UIMenuItem("Pistola Pesada", "WIP"));
            cortas.AddItem(new UIMenuItem("Pistola Vintage", "WIP"));
            cortas.AddItem(new UIMenuItem("Pistola de vengalas", "WIP"));
            cortas.AddItem(new UIMenuItem("Pistola de tirador", "WIP"));
            cortas.AddItem(new UIMenuItem("Revolver", "WIP"));


            cortas.AddItem(new UIMenuItem("Cerrar", "Cerrar el menú"));
            // Refrescamos los índices
            mp.RefreshIndex();
            cortas.MouseControlsEnabled = false;
            cortas.Visible = true;
        }

        public void SelectCortas(UIMenu ui, UIMenuItem item, int index)
        {
            if (index < 11)
            {
                RAGE.Events.CallRemote("RecArmeriaPD", 0, index);
            }
            ui.Visible = false;
            Cerrar();
        }

        public void Otr()
        {
            Chat.Show(false);
            misc = new UIMenu("Otros objectos", "Defensas, linternas ...");
            mp.Add(misc);
            misc.OnItemSelect += SelectOtr;

            misc.AddItem(new UIMenuItem("Defensa", "Herramienta perfecta para dispersar y marcar manifestantes."));
            misc.AddItem(new UIMenuItem("Linterna", "La luz de la noche policial"));
            misc.AddItem(new UIMenuItem("Extintor", "En caso de incendio ..."));
            misc.AddItem(new UIMenuItem("Palanca", "Para abrir vehículos en caso de emergencia"));
            misc.AddItem(new UIMenuItem("Paracaidas", "I believe I can fly"));

            misc.AddItem(new UIMenuItem("Cerrar", "Cerrar el menú"));
            // Refrescamos los índices
            mp.RefreshIndex();
            misc.MouseControlsEnabled = false;
            misc.Visible = true;

        }

        public void SelectOtr(UIMenu ui, UIMenuItem item, int index)
        {
            if (index < 5)
            {
                RAGE.Events.CallRemote("RecArmeriaPD", 1, index);
            }
            ui.Visible = false;
            Cerrar();
        }

        public void Subfusiles()
        {
            Chat.Show(false);
            subf = new UIMenu("Subfusiles", "El ayuntamiento está negociando la compra de otros.");
            mp.Add(subf);
            subf.OnItemSelect += SelectSubf;

            subf.AddItem(new UIMenuItem("MP5", "Un subfusil con 120 balas (4 cargadores)"));
            subf.AddItem(new UIMenuItem("Subfusil de Asalto", "Un subfusil con 120 balas (4 cargadores)"));
            subf.AddItem(new UIMenuItem("ADP de Combate", "Un subfusil con 12 balas (4 cargadores)"));
          
            subf.AddItem(new UIMenuItem("Cerrar", "Cerrar el menú"));
            // Refrescamos los índices
            mp.RefreshIndex();
            subf.MouseControlsEnabled = false;
            subf.Visible = true;

        }

        public void SelectSubf(UIMenu ui, UIMenuItem item, int index)
        {
            if (index < 3)
            {
                RAGE.Events.CallRemote("RecArmeriaPD", 2, index);
            }
            ui.Visible = false;
            Cerrar();
        }

        public void Escopetas()
        {
            Chat.Show(false);
            esc = new UIMenu("Escopetas", "Armas largas para patrullaje semiordinario.");
            mp.Add(esc);
            esc.OnItemSelect += SelectEsc;

            esc.AddItem(new UIMenuItem("UTAS UTS-15", "Una escopeta de asalto con muncion para 16 disparos"));
            esc.AddItem(new UIMenuItem("Mossberg 500 recortada", "Una escopeta recortada con muncion para 16 disparos"));


            esc.AddItem(new UIMenuItem("Cerrar", "Cerrar el menú"));
            // Refrescamos los índices
            mp.RefreshIndex();
            esc.MouseControlsEnabled = false;
            esc.Visible = true;

        }

        public void SelectEsc(UIMenu ui, UIMenuItem item, int index)
        {
            if (index < 2)
            {
                RAGE.Events.CallRemote("RecArmeriaPD", 3, index);
            }
            ui.Visible = false;
            Cerrar();
        }

        public void Cerrar()
        {
            isOpen = false;
            Chat.Show(true);
            misc.Visible = false;
            cortas.Visible = false;
            esc.Visible = false;
            subf.Visible = false;
        }

        public void Backspace(object [] args)
        {
            Cerrar();
        }

        public void Fusiles()
        {
            Chat.Show(false);
            subf = null;
            subf = new UIMenu("Fusiles", "Armas automaticas");
            mp.Add(subf);
            subf.OnItemSelect += SelectFus;

            subf.AddItem(new UIMenuItem("Carbine", "Un fusil con 120 balas (4 cargadores)"));
            subf.AddItem(new UIMenuItem("G36C", "Un fusil con 120 balas (4 cargadores)"));


            subf.AddItem(new UIMenuItem("Cerrar", "Cerrar el menú"));
            // Refrescamos los índices
            mp.RefreshIndex();
            subf.MouseControlsEnabled = false;
            subf.Visible = true;

        }

        public void SelectFus(UIMenu ui, UIMenuItem item, int index)
        {
            if (index < 2)
            {
                RAGE.Events.CallRemote("RecArmeriaPD", 4, index);
            }
            ui.Visible = false;
            Cerrar();
        }

        public void Rifles()
        {
            Chat.Show(false);
            subf = null;
            subf = new UIMenu("Rifles", "Armas automaticas");
            mp.Add(subf);
            subf.OnItemSelect += SelectRif;

            subf.AddItem(new UIMenuItem("Rifle de tirador", "Un rile con 24 balas (3 cargadores)"));
           


            subf.AddItem(new UIMenuItem("Cerrar", "Cerrar el menú"));
            // Refrescamos los índices
            mp.RefreshIndex();
            subf.MouseControlsEnabled = false;
            subf.Visible = true;

        }

        public void SelectRif(UIMenu ui, UIMenuItem item, int index)
        {
            if (index < 1)
            {
                RAGE.Events.CallRemote("RecArmeriaPD", 5, index);
            }
            ui.Visible = false;
            Cerrar();
        }

        public void Expl()
        {
            Chat.Show(false);
            subf = null;
            subf = new UIMenu("Explosivos", "¡Boom!");
            mp.Add(subf);
            subf.OnItemSelect += SelectExpl;

            subf.AddItem(new UIMenuItem("Granada humo", "5 unidades"));
            subf.AddItem(new UIMenuItem("Lanza Granadas", "10 Proyectiles"));
            subf.AddItem(new UIMenuItem("Granadas", "5 unidades"));


            subf.AddItem(new UIMenuItem("Cerrar", "Cerrar el menú"));
            // Refrescamos los índices
            mp.RefreshIndex();
            subf.MouseControlsEnabled = false;
            subf.Visible = true;

        }

        public void SelectExpl(UIMenu ui, UIMenuItem item, int index)
        {
            if (index < 3)
            {
                RAGE.Events.CallRemote("RecArmeriaPD", 6, index);
            }
            ui.Visible = false;
            Cerrar();
        }
    }
}
