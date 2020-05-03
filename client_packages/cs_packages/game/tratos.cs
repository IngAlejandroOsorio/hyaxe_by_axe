using RAGE;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game
{
    public class tratos : Events.Script
    {
        private RAGE.Ui.HtmlWindow windowd;
        private MenuPool mpool = new MenuPool();
        private UIMenu uiM = new UIMenu("Tratos", "Menú con todos los tratos por aceptar - Al cerrar el menú se cancelan");


        public tratos()
        {
            Events.Add("CrearTrato", crearTrato);
            Events.Add("CerrarDocumentacion", cerrarDocumentacion);
        }

        private void crearTrato(object[] args)
        {
            int tipo = int.Parse(args[0].ToString());
            string nombre = args[1].ToString();


            UIMenuItem trato = new UIMenuItem(nombre);
            uiM.AddItem(trato);

            if (tipo == 1)  //DOCUMENTACIÓN
            {
                windowd = new RAGE.Ui.HtmlWindow("package://statics/documentos/Pasaporte.html");
                windowd.ExecuteJs($"instanciar({args[2]});");
            }

            mpool.RefreshIndex();
            uiM.Visible = true;
        }

        private void mostrarDocumentacion(string JSONCODE)
        {
            windowd = new RAGE.Ui.HtmlWindow("package://statics/documentos/Pasaporte.html");
            windowd.ExecuteJs("instanciar('"+JSONCODE+"');");
            
        }

        private void cerrarDocumentacion(object[] args)
        {
            windowd.Destroy();
        }
    }


 }

