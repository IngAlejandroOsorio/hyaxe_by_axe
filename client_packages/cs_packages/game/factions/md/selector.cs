using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.NUI;

namespace DowntownRP_cs.game.factions.md
{
    public class Selector : Events.Script
    {
        private bool isOpen = false;
        private MenuPool mp;
        private UIMenu AyudaMenu;
        int tipo = 0;
        public Selector()
        {
            Events.Add("selectorLSFD", Taquilla);
            Events.Tick += DrawMenu;

            mp = new MenuPool();

            AyudaMenu = new UIMenu("Elección LSFD", "Selecciona tu Rama, Si has elegido en el formulario elige ESA.");
            mp.Add(AyudaMenu);
            AyudaMenu.OnItemSelect += Select;
            AyudaMenu.AddItem(new UIMenuItem("Bomberos"));
            AyudaMenu.AddItem(new UIMenuItem("Médicos"));

            AyudaMenu.AddItem(new UIMenuItem("Cerrar", "Cerrar el menú"));
            // Refrescamos los índices
            mp.RefreshIndex();


            
        }

        public void Taquilla(object[] args)
        {
            tipo = (int) args[0];
            Chat.Show(false);
            isOpen = true;

            
            AyudaMenu.Visible = true;
            AyudaMenu.OnMenuClose += (sender) => { Cerrar(); };
        }


        private void DrawMenu(List<Events.TickNametagData> nametags)
        {
            if (isOpen) mp.ProcessMenus();
        }


        public void Select(UIMenu ui, UIMenuItem item, int index)
        {
            if (index < 2)
            {
                RAGE.Events.CallRemote("recSelectorLSFD", tipo, index);
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
