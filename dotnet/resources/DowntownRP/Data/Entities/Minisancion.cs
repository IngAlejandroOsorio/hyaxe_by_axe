using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class Minisancion
    {
        public int paraElBalance { get;  }              //-1 punto rol negativo
                                                        // 0 Warn
                                                        // 1 punto rol positivo
                                                        //-2 Kick
        public string razon { get;  }
        public DateTime FechaHora { get; }
        public string Staff { get; }

        public Minisancion(DateTime fechaHora, string staff, string Razon, int tipo)
        {
            paraElBalance = tipo;
            razon = Razon;
            FechaHora = fechaHora;
            Staff = staff;
        }
    }
}
