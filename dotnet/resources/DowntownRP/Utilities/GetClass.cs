using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace DowntownRP.Utilities
{
    public static class GetClass
    {
        public static Data.Entities.User getClass(this Player pl)
        {
            if (pl.HasData("USER_CLASS"))
            {
                return pl.GetData<Data.Entities.User>("USER_CLASS");
            }
            else
            {
                return null;
            }
        }
    }
}
