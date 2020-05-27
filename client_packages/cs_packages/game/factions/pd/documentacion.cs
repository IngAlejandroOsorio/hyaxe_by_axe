using RAGE;
using RAGE.NUI;
using System.Collections.Generic;
    

namespace DowntownRP_cs.game.factions.pd
{
    public class documentacion : Events.Script
    {
        private bool isOpen = false;
        private MenuPool mp;
        private UIMenu AyudaMenu;
        private UIMenuItem close;
        public documentacion()
        {
            Events.Add("documentacion", mdoc);
            Events.Tick += DrawMenu;

           

            
            
        }

        public void mdoc(object [] args)
        {


            mp = new MenuPool();

            AyudaMenu = new UIMenu("Documentación", "");
            AyudaMenu.OnMenuClose += (sender) => { Cerrar(); };
            mp.Add(AyudaMenu);

            string nombre = (string) args[0];
            string dni = (string) args[1];
            string edad = (string) args[2];



            RAGE.Events.CallRemote("debugTest", "Cargados datos");

            AyudaMenu.AddItem(new UIMenuItem("Nombre:", nombre));
            AyudaMenu.AddItem(new UIMenuItem("Num ID:", dni));
            //AyudaMenu.AddItem(new UIMenuItem("Edad:", edad));
            close = new UIMenuItem("Cerrar", "Cierra y libera el slot para que otro pueda darte tu documentación");
            AyudaMenu.AddItem(close);

            RAGE.Events.CallRemote("debugTest", "Creado menú");


            isOpen = true;
            Chat.Show(false);
            RAGE.Ui.Cursor.Visible = false;


            AyudaMenu.OnMenuClose += (sender) => { Cerrar(); };
            AyudaMenu.OnItemSelect += (sender, item, index) => { if(item == close) Cerrar(); };

            mp.RefreshIndex();
            AyudaMenu.Visible = true;
           
        }


        private void DrawMenu(List<Events.TickNametagData> nametags)
        {
            if (isOpen) mp.ProcessMenus();
        }


        public void Cerrar()
        {
            isOpen = false;
            Chat.Show(true);
            RAGE.Ui.Cursor.Visible = true;
            Events.CallRemote("cerrarDoc");
        }
    }
}
