using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using System.Threading.Tasks;
using DowntownRP.Utilities;

namespace DowntownRP.World.Factions.MD
{
    public class Main : Script
    {
        [RemoteEvent ("recSelectorLSFD")]
        public async Task EV_recSelectorLSFD (Player player, int tipo, int el)
        {
            Notifications.SendNotificationINFO(player, "Elección hecha");
            Data.Entities.User t = player.getClass();
            if(tipo == 0)
            {
                int factionrank = 2;

                if(el == 0)
                {
                    factionrank = 1;
                }
                await Game.CharacterSelector.CharacterSelector.UpdateUserFaction(t.idpj, 2);
                await Game.CharacterSelector.CharacterSelector.UpdateUserFactionRank(t.idpj, factionrank);

                t.faction = 2;
                t.rank = factionrank;

                player.ResetData("FACTION_PETICION");

                Utilities.Notifications.SendNotificationOK(player, "Has ingresado en la LSFD con rango " + World.Factions.Main.GetFactionRankName(2, t.rank));
            }
        }

    }
}
