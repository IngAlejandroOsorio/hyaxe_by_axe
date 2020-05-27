using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;

namespace DowntownRP.Game.Payday
{
    public class Payday : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public async Task PaydayFunction()
        {
            await Task.Delay(1000);

            while (true)
            {
                await Task.Delay(3600000);

                // Payday player
                foreach (var player in NAPI.Pools.GetAllPlayers())
                {
                    //var info = player.GetExternalData<Data.Entities.User>(0);
                    if (!player.HasData("USER_CLASS")) return;
                    Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
                    if (user != null)
                    {
                        await PaydayPlayerFunction(player, user);
                    }
                }

                // Payday weed
                await Drugs.Weed.Main.PaydayMarihuana();

            }
        }

        public async Task PaydayPlayerFunction(Player player, Data.Entities.User user)
        {
            int level = user.level;
            int exp = user.exp;

            user.exp = exp + 1;

            if (user.exp*4 == user.level)
            {
                user.level = level + 1;
                //player.TriggerEvent("testPaydayLevel", "¡Has subido de nivel!");
                await DatabaseFunctions.UpdateUserXp(user.idpj, user.exp);
                await DatabaseFunctions.UpdateUserLevel(user.idpj, user.level);
            }
            else
            {
               // player.TriggerEvent("testPaydayLevel", "");
                await DatabaseFunctions.UpdateUserXp(user.idpj, user.exp);
            }

            player.SendChatMessage("--------- PAGOS E IMPUESTOS ---------");
            player.SendChatMessage($"Nivel {user.level} | Experiencia {user.exp}");

            if (user.bankAccount != 0)
            {
                await World.Banks.MoneyFunctions.RemoveMoneyBank(player, 10);
                player.SendChatMessage($"Impuestos por cuenta bancaria: $10");
                await World.Banks.MoneyFunctions.AddMoneyBank(player, 50); // Ganancia gubernamental
            }
            else await Money.MoneyModel.AddMoney(player, 50); // Ganancia gubernamental

            /*player.SendChatMessage($"<font color='green'><i class='fas fa-money-check'></i></font> Impuestos por vehículos: <font color='green'>$10</font>");
            player.SendChatMessage($"<font color='green'><i class='fas fa-money-check'></i></font> Impuestos por propiedades: <font color='green'>$10</font>");
            player.SendChatMessage($"<font color='green'><i class='fas fa-money-bill-alt'></i></font> Ganancias por empleo: <font color='green'>$10</font>");*/

            
            player.SendChatMessage($"Ayuda gubernamental: $50");
            player.SendChatMessage("--------- PAGOS E IMPUESTOS ---------");

            Data.Entities.FineLSPD multa = Data.Lists.finesPD.Find(x => x.userid == user.idpj);
            if(multa != null)
            {
                if (!multa.isPaid)
                {
                    player.SendChatMessage($"Tienes multas sin pagar. Se te descontarán $50 cada payday hasta que las pagues.");
                    await Money.MoneyModel.SubMoney(player, 50);
                }
            }
        }
    }
}
