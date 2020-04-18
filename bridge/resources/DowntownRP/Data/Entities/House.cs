using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class House
    {
        public int id { get; set; }
        public int owner { get; set; }
        public Client entityOwner { get; set; }
        public ColShape shape { get; set; }
        public TextLabel label { get; set; }
        public Marker marker { get; set; }
        public Blip blip { get; set; }
        public int type { get; set; }
        public int price { get; set; }
        public Vector3 position { get; set; }
        public string area { get; set; }
        public int number { get; set; }
        public int safeBox { get; set; }
        public Inventory inventory { get; set; }
        public bool isOpen { get; set; } = false;
        public List<Data.Entities.User> usersOnInterior = new List<Data.Entities.User>();
    }
}
