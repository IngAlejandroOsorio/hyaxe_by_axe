using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class FichaPolicial
    {
        public int idCard { get; set; }
        public string Nombre { get; set; }
        public string IBAN { get; set; }
        public int phone { get; set; }

        public List<int> propiedades { get; set; } = new List<int>();
        public List<string> Vehículos { get; set; } = new List<string>();
        

        public List<int> licencias { get; set; } = new List<int>() { 0, 0, 0 };
        public List<FineLSPD> multas { get; set; }
        public int AdminIDpj { get; set; }

    }
}
