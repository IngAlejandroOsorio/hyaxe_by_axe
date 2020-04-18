using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.company.trucker
{
    public class TruckerJob : Events.Script
    {
        private static RAGE.Elements.Blip blip = null;
        public TruckerJob()
        {
            Events.Add("TruckerStartBlip", TruckerStartBlip);
            Events.Add("TruckerDestroyBlip", TruckerDestroyBlip);
        }

        private void TruckerStartBlip(object[] args)
        {
            if (blip != null) blip.Destroy();
            Vector3 position = (Vector3)args[0];
            switch ((int)args[1])
            {
                case 1:
                    blip = new RAGE.Elements.Blip(478, position, "Almacén de carga", 1, 6, 255);
                    blip.SetRoute(true);
                    break;

                case 2:
                    blip = new RAGE.Elements.Blip(478, position, "Entrega de mercancia", 1, 6, 255);
                    blip.SetRoute(true);
                    break;

                case 3:
                    blip = new RAGE.Elements.Blip(478, position, "Entrega del camion", 1, 6, 255);
                    blip.SetRoute(true);
                    break;
            }
        }

        private void TruckerDestroyBlip(object[] args)
        {
            blip.Destroy();
        }
    }
}
