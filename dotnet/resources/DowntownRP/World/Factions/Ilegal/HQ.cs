using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Factions.Ilegal
{
    public class HQ : Script
    {
        public static Vector3 positionHq = new Vector3(266.1425, -1006.98, -100.8834);

        [RemoteEvent("ActionEnterFactionHq")]
        public async Task RE_ActionEnterFactionHq(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (player.HasData("SALIDA_FACTION"))
            {
                player.Position = player.GetData<Vector3>("SALIDA_FACTION");
                player.Dimension = 0;
                user.isInFactionInterior = false;
                user.ilegalFactionInterior = null;
            }

            if (user.ilegalFactionShape != null)
            {
                if (user.faction == user.ilegalFactionShape.id)
                {
                    user.isInFactionInterior = true;
                    user.ilegalFactionInterior = user.ilegalFactionShape;

                    await Task.Delay(500);

                    user.entity.Position = positionHq;
                    user.entity.Dimension = (uint)user.faction;
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No eres miembro de esta facción");
            }
        }
    }
}
