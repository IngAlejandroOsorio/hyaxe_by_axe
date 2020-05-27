using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace DowntownRP.World.Factions.Ilegal
{
    public class Trafico : Script
    {

        public static async Task cargarPuntosTrafico ()
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM puntostrafico";

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            float x = reader.GetFloat(reader.GetOrdinal("x"));
                            float y = reader.GetFloat(reader.GetOrdinal("y"));
                            float z = reader.GetFloat(reader.GetOrdinal("z"));
                            float heading = reader.GetFloat(reader.GetOrdinal("heading"));


                            Data.Lists.puntosTraficoIl.Add(new Data.Entities.puntoTraficoIl(x, y, z, heading, false));                        }
                    }
                }
            }
        }

        [Command ("crearpuntotrafico")]
        public async Task CMD_crearpuntotrafico(Player player)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 4))
            {
                if (player.IsInVehicle)
                {
                    float x = player.Vehicle.Position.X;
                    float y = player.Vehicle.Position.Y;
                    float z = player.Vehicle.Position.Z;

                    float heading = player.Vehicle.Heading;

                    Data.Lists.puntosTraficoIl.Add(new Data.Entities.puntoTraficoIl(x, y, z, heading, true));
                    Utilities.Notifications.SendNotificationOK(player, "Has creado un punto de tráficos correctamente");
                }
                else
                {
                    Utilities.Notifications.SendNotificationERROR(player, "Tienes que estar en un vehículo para ejecutar este comando");
                }
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permiso para usar este comando");
            }
        }

        [Command("traficararmas")]
        public async Task CMD_traficarArmas (Player player)
        { 
            Data.Entities.User usr = player.GetData<Data.Entities.User>("USER_CLASS");
            Data.Entities.Faction f = Data.Lists.factions.Find(x => x.id == usr.faction);
            if (f.armasCortas|f.armasLargas) {
                if (usr.rank >= 5) {
                    if (0<=DateTime.Compare(f.coolDown, DateTime.UtcNow))
                    {
                        Utilities.Notifications.SendNotificationINFO(player, "Preparando paquete");
                        await Task.Delay(1);
                        await Trabajar(player);
                    }
                    else
                    {
                        Utilities.Notifications.SendNotificationERROR(player, "Tienes que esperar para poder hacer un trafico ilegal");
                    }
                    Utilities.Notifications.SendNotificationINFO(player, "Preparando paquete");
                    await Trabajar(player);
                } else {
                    Utilities.Notifications.SendNotificationERROR(player, "Tienes que tener rango 5 o superior en tu facción para poder inciar un tráfico");
                }
            } else
            {
                Utilities.Notifications.SendNotificationERROR(player, "Tu facción no está habilitada para traficar con armas");
            }

            

        }


        public async Task<Vehicle> CrearVeh(Data.Entities.puntoTraficoIl punto, VehicleHash model)
        {
            Random random = new Random();
            
            return NAPI.Vehicle.CreateVehicle(model, punto.posiciones, punto.heading, random.Next(26), random.Next(26));        
        }

        public async Task Trabajar(Player player)
        {
            int Precio = 0;
            Data.Entities.User usr = player.GetData<Data.Entities.User>("USER_CLASS");

            Random random = new Random();
            Data.Entities.puntoTraficoIl punto;
            Random r = new Random();

            
            for (; ; )
            {
                punto = Data.Lists.puntosTraficoIl[random.Next(Data.Lists.puntosTraficoIl.Count)];
                if (!punto.activo)
                {
                    break;
                }
            }
            punto.activo = true;
                var f = Data.Lists.factions.Find(x => x.id == usr.faction);
                
                
                

                f.traficov.trucker = new Data.Entities.Inventory();

                var t = f.traficov.trucker;
                int i = 0;
                List<WeaponHash> whL = new List<WeaponHash>(){
                                    WeaponHash.Machinepistol, WeaponHash.Microsmg, WeaponHash.Minismg, WeaponHash.Assaultrifle, WeaponHash.Advancedrifle, WeaponHash.Bullpuprifle, WeaponHash.Compactrifle};
                List<WeaponHash> whC = new List<WeaponHash>(){
                                    WeaponHash.Vintagepistol, WeaponHash.Pistol, WeaponHash.Pistol50};
                if (f.armasCortas)
                {
                    for (; ; )
                    {
                        Precio = Precio + 300;

                        if (i == 7)
                        {
                            break;
                        }
                        if(Precio > f.safeBox)
                        {
                            Precio = Precio - 300;
                            break;
                        }

                        WeaponHash v = whC[random.Next(whC.Count)];

                        var it = new Data.Entities.Item(0, Utilities.Weapon.GetWeaponNameByHash(v), 1, 1);
                        it.isAWeapon = true;
                        it.bullets = 36;
                        it.weaponHash = v;
                        Game.Vehicles.Truck.SetNewItemVehicleInventory(t, it);

                        i++;

                    }
                }

                int a = 0;
                if (f.armasLargas) 
                {
                    
                    for (; ; )
                    {
                        Precio = Precio + 1000;
                        if (a == 3)
                        {
                            break;
                        }

                        if (Precio > f.safeBox)
                        {
                            Precio = Precio - 1000 ;
                            break;
                        }

                        WeaponHash v = whL[random.Next(whL.Count)];

                        var it = new Data.Entities.Item(0, Utilities.Weapon.GetWeaponNameByHash(v), 1, 1);
                        it.isAWeapon = true;
                        it.bullets = 36;
                        it.weaponHash = v;
                        Game.Vehicles.Truck.SetNewItemVehicleInventory(t, it);

                        a++;
                    }
                }
            if (a == 0 & i == 0) return;

            f.traficov.entity.Locked = true;

            f.traficov = new Data.Entities.VehicleFaction();

            f.traficov.faction = f.id;
            f.traficov.model = Data.Lists.vehsTrafico[random.Next(Data.Lists.vehsTrafico.Count)];
            //f.traficov.entity.EnginePowerMultiplier = 0;

            NAPI.Task.Run(() =>
            {
                f.traficov.entity = NAPI.Vehicle.CreateVehicle(NAPI.Util.VehicleNameToModel(f.traficov.model), punto.posiciones, punto.heading, random.Next(26), random.Next(26));
            });

            NAPI.Task.Run(() => { f.traficov.entity.SetData("VEHICLE_FACTION_DATA", f.traficov); });
            foreach (Data.Entities.User user in Data.Lists.playersConnected)
            {
                if (usr.faction == user.faction)
                {
                    player.SendChatMessage("Creado tráfico, vé y recoge el paquete, el vehículo tiene un localizador cian");
                    usr.entity.TriggerEvent("LocalizarTrafBlip", punto.posiciones, $"Armas ({i}C/{a}L)");
                }
            }

            new Utilities.Log($"La facción {f.name} ({f.id}) inicia trafico de Armas ({a} Largas y {i} cortas) - {Factions.Main.GetFactionRankName(usr.faction, usr.rank)}{player.Name}", 1);
            f.traficov = new Data.Entities.VehicleFaction();

                f.traficov.faction = f.id;
                f.traficov.model = Data.Lists.vehsTrafico[random.Next(Data.Lists.vehsTrafico.Count)];
                //f.traficov.entity.EnginePowerMultiplier = 0;

                NAPI.Task.Run(() =>
                {
                    f.traficov.entity = NAPI.Vehicle.CreateVehicle(NAPI.Util.VehicleNameToModel(f.traficov.model), punto.posiciones, punto.heading, random.Next(26), random.Next(26));
                });
            Utilities.Notifications.SendNotificationOK(player, "Creado tráfico, vé y recoge el paquete");
                f.safeBox = f.safeBox - Precio;
                World.Factions.DbFunctions.UpdateFactionSafebox(f.id, f.safeBox);

                f.coolDown = DateTime.UtcNow.AddDays(1); //CONFIGURAR TIEMPO (DE MOMENTO 1 DIA)
                World.Factions.DbFunctions.UpdateFactionCooldown(f.id, f.coolDown);

                



                int milisecs = Convert.ToInt32(TimeSpan.FromHours(1).TotalMilliseconds); //Cooldown hasta desaparición veh
                await Task.Delay(milisecs);

                NAPI.Task.Run(() =>
                {
                    f.traficov.entity.Delete();
                    punto.activo = false;
                });

                f.traficov = null;
                
            
        }
    }
}
