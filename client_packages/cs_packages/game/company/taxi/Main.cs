using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.company.taxi
{
    public class Main : Events.Script
    {
        private static RAGE.Elements.Blip blip = null;
        public Main()
        {
            Events.Add("CrearBlipCliente", CrearBlipCliente);
            Events.Add("DestruirBlipCliente", DestruirBlipCliente);
        }

        private void CrearBlipCliente(object[] args)
        {
            if (blip != null) blip.Destroy();
            blip = new RAGE.Elements.Blip(280, (Vector3)args[0], "CLIENTE");
            blip.SetColour(5);
        }

        private void DestruirBlipCliente(object[] args)
        {
            blip.Destroy();
            blip = null;
        }
    }
}
