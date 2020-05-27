using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace DowntownRP.World.Factions
{
    public class PuntoFaccion
    {
        public ColShape shape { get; set; }
        public Marker marker { get; set; }
        public TextLabel tlabel { get; set; }

        public PuntoFaccion(string code, Vector3 poss, string TextLabel)
        {
            shape = NAPI.ColShape.CreateCylinderColShape(poss, 3, 3);
            shape.SetData(code, true);
            tlabel = NAPI.TextLabel.CreateTextLabel(TextLabel, poss, 4, 2, 0, new Color(255, 255, 255));
            marker = NAPI.Marker.CreateMarker(1, poss.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(202, 24, 24));
        }

    }
}
