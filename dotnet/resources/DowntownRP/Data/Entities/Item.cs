using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace DowntownRP.Data.Entities
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; } // 1 use, drop, trade | 2 use | 3 drop, trade | 4 show | 5 nothing
        public int quantity { get; set; }
        public int slot { get; set; } = 0;
        public bool isAWeapon { get; set; } = false;
        public  WeaponHash  weaponHash { get; set; }
        public int bullets { get; set; } = 0;

        public Item(int itemid, string itemname, int itemtype, int itemquantity)
        {
            id = itemid;
            name = itemname;
            type = itemtype;
            quantity = itemquantity;
        }
    }
}
