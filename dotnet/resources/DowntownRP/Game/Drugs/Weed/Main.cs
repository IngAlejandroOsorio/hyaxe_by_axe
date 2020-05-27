using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Game.Drugs.Weed
{
    public class Main : Script
    {
        [ServerEvent(Event.PlayerEnterColshape)]
        public void Weed_EnterColShape(ColShape shape, Player player)
        {
            if (shape.HasData("WEED_CLASS"))
            {
                player.SetData<Data.Entities.Weed>("WEED_CLASS", shape.GetData<Data.Entities.Weed>("WEED_CLASS"));
            }
        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void Weed_ExitColShape(ColShape shape, Player player)
        {
            if (shape.HasData("WEED_CLASS"))
            {
                player.ResetData("WEED_CLASS");
            }
        }

        public async static Task RegarPlanta(Player player, Data.Entities.Weed weed)
        {
            if (weed.status == 2 || weed.status == 4 || weed.status == 6 || weed.status == 8 || weed.status == 9)
            {
                Utilities.Notifications.SendNotificationERROR(player, "No puedes regar la planta");
                return;
            }

            NAPI.Task.Run( () =>
            {
                switch (weed.status)
                {
                    case 1:
                        weed.prop.Delete();
                        weed.prop = NAPI.Object.CreateObject(1595624552, weed.position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                        weed.label.Text = $"~g~{weed.type}~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~g~sana";
                        weed.status++;
                        break;

                    case 3:
                        weed.prop.Delete();
                        weed.prop = NAPI.Object.CreateObject(1595624552, weed.position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                        weed.label.Text = $"~g~{weed.type}~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~g~sana";
                        weed.status++;
                        break;

                    case 5:
                        weed.prop.Delete();
                        weed.prop = NAPI.Object.CreateObject(3989082015, weed.position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                        weed.label.Text = $"~g~{weed.type}~n~~w~Fase: ~b~floración~n~~w~Estado: ~g~sana";
                        weed.status++;
                        break;

                    case 7:
                        weed.prop.Delete();
                        weed.prop = NAPI.Object.CreateObject(3989082015, weed.position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                        weed.label.Text = $"~g~{weed.type}~n~~w~Fase: ~b~floración~n~~w~Estado: ~g~sana";
                        weed.status++;
                        break;
                }
            });
        }

        public async static Task PaydayMarihuana()
        {
            NAPI.Task.Run(() =>
            {
                foreach(var weed in Data.Lists.weedPlants)
                {
                    switch (weed.status)
                    {
                        case 2:
                            weed.label.Text = $"~g~{weed.type}~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~r~necesita riego";
                            weed.status++;
                            break;

                        case 4:
                            weed.prop.Delete();
                            weed.prop = NAPI.Object.CreateObject(3989082015, weed.position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                            weed.label.Text = $"~g~{weed.type}~n~~w~Fase: ~b~floración~n~~w~Estado: ~r~necesita riego";
                            weed.status++;
                            break;

                        case 6:
                            weed.label.Text = $"~g~{weed.type}~n~~w~Fase: ~b~floración~n~~w~Estado: ~r~necesita riego";
                            weed.status++;
                            break;

                        case 8:
                            weed.prop.Delete();
                            weed.prop = NAPI.Object.CreateObject(452618762, weed.position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                            weed.label.Text = $"~g~{weed.type}~n~~w~Fase: ~b~lista para cortar~n~~w~Pulsa ~b~K ~w~para cortar";
                            weed.status++;
                            break;
                    }
                }
            });
        }
    }
}
