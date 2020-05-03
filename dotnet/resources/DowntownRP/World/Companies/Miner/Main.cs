using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DowntownRP.World.Companies.Miner
{
    public class Main : Script
    {
        public Main()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(MineroEvent);
            aTimer.Interval = 1200000;
            aTimer.Enabled = true;

           // NAPI.Blip.CreateBlip("Mina", )
        }

        private void MineroEvent(object sender, ElapsedEventArgs e)
        {
            foreach(var rock in Data.Lists.minerPoints)
            {
                if(rock.recursos != 25)
                {
                    rock.recursos++;
                    rock.label.Text = $"Pulsa ~g~K~w~ para picar piedra~n~Recursos: ~g~{rock.recursos}/25";
                }
            }
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void minero_colshape(ColShape shape, Player player)
        {
            if (shape.HasData("MINER_POINT"))
            {
                player.SetData("MINER_POINT", shape.GetData<Data.Entities.MinerPoint>("MINER_POINT"));
            }
        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void PlayerExit_Minero(ColShape shape, Player player)
        {
            if (shape.HasData("MINER_POINT"))
            {
                player.ResetData("MINER_POINT");
            }
        }

        [RemoteEvent("ActionMineroPicar")]
        public async Task ActionMineroPicar(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.isMining) return;
            if (!player.HasData("MINER_POINT")) return;
            Data.Entities.MinerPoint miner = player.GetData<Data.Entities.MinerPoint>("MINER_POINT");

            if (!user.chatStatus)
            {
                if (user.companyMember.type == 4)
                {
                    if (user.isCompanyDuty)
                    {
                        if (miner.recursos > 0)
                        {
                            Console.WriteLine($"MINERO DEBUG | {miner.recursos} - {user.entity.Name}");
                            if (await Game.Inventory.Inventory.CheckIfPlayerHasSlot(user))
                            {
                                if (miner.recursos < 0) return;
                                await FuncionMinando(user);
                                miner.recursos--;
                                miner.label.Text = $"Pulsa ~g~E~w~ para picar piedra~n~Recursos: ~g~{miner.recursos}/25";
                            }
                        }
                        else Utilities.Notifications.SendNotificationERROR(player, "Esta roca no tiene mas recursos");
                    }
                }
            }
        }

        private static async Task FuncionMinando(Data.Entities.User user)
        {
            user.isMining = true;
            user.entity.TriggerEvent("freeze_player");

            user.entity.TriggerEvent("playAnimPlayer", "amb@world_human_const_drill@male@drill@base", "base");
            NAPI.ClientEvent.TriggerClientEventForAll("Object.AttachMina", "prop_tool_jackham", user.entity.Value, 18905);
            
            await Task.Delay(20000); //10s
            NAPI.ClientEvent.TriggerClientEventForAll("Object.Delete", user.entity.Value);
            user.entity.StopAnimation();

            await GiveMineralMinero(user.entity);
            Utilities.Notifications.SendNotificationOK(user.entity, "Has obtenido un material");
            
            user.entity.TriggerEvent("freeze_player");
            user.isMining = false;
        }

        [RemoteEvent("ActionSellMinerals")]
        public void RE_ActionSellMinerals(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if(user.companyMember != null)
            {
                if (user.companyMember.type == 4)
                {
                    if (user.isInCompanyDuty) player.TriggerEvent("OpenMinerSellMenu", user.companyMember.name);
                }
            }
        }

        [RemoteEvent("SellMinerCompany")]
        public async Task RE_SellMinerCompany(Player player, string mineral)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            Data.Entities.Item itemCheck = await Game.Inventory.Inventory.CheckIfHasItem(user, mineral);
            if (itemCheck != null)
            {
                int money = 0;
                switch (mineral)
                {
                    case "Bronce":
                        money = 17;
                        break;

                    case "Plata":
                        money = 35;
                        break;

                    case "Oro":
                        money = 65;
                        break;

                    case "Diamante":
                        money = 180;
                        break;
                }

                if(itemCheck.quantity == 1)
                {
                    await Game.Inventory.DatabaseFunctions.RemoveItemDatabase(itemCheck.id);
                    itemCheck.id = 0;
                    itemCheck.name = "NO";
                    itemCheck.quantity = 0;
                    itemCheck.type = 0;
                }
                else
                {
                    itemCheck.quantity--;
                    await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(itemCheck.id, user.idpj, mineral, itemCheck.type, itemCheck.quantity, itemCheck.slot);
                }
                await Game.Money.MoneyModel.AddMoney(player, (double)money);
                Utilities.Notifications.SendNotificationOK(player, $"Has vendido 1x{mineral} por ~g~${money}");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes este mineral en el inventario");
        }

        public static async Task GiveMineralMinero(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            Random lol = new Random();
            int mineral = lol.Next(1, 20);
            string obj = "";

            switch (mineral)
            {
                case 1:
                    obj = "Bronce";
                    break;

                case 2:
                    obj = "Bronce";
                    break;

                case 3:
                    obj = "Bronce";
                    break;

                case 4:
                    obj = "Bronce";
                    break;

                case 5:
                    obj = "Bronce";
                    break;

                case 6:
                    obj = "Bronce";
                    break;

                case 7:
                    obj = "Bronce";
                    break;

                case 8:
                    obj = "Bronce";
                    break;

                case 9:
                    obj = "Bronce";
                    break;

                case 10:
                    obj = "Bronce";
                    break;

                case 11:
                    obj = "Plata";
                    break;

                case 12:
                    obj = "Plata";
                    break;

                case 13:
                    obj = "Plata";
                    break;

                case 14:
                    obj = "Plata";
                    break;

                case 15:
                    obj = "Plata";
                    break;

                case 16:
                    obj = "Plata";
                    break;

                case 17:
                    obj = "Oro";
                    break;

                case 18:
                    obj = "Oro";
                    break;

                case 19:
                    obj = "Oro";
                    break;

                case 20:
                    obj = "Diamante";
                    break;
            }

            Data.Entities.Item itemCheck = await Game.Inventory.Inventory.CheckIfHasItem(user, obj);
            if (itemCheck == null)
            {
                Data.Entities.Item itemm = new Data.Entities.Item(0, obj, 4, 1);
                await Game.Inventory.DatabaseFunctions.SetNewItemInventory(player, itemm);
            }
            else
            {
                itemCheck.quantity++;
                await Game.Inventory.DatabaseFunctions.UpdateItemQuantityDatabase(itemCheck.id, itemCheck.quantity);
            }
        }

    }
}
