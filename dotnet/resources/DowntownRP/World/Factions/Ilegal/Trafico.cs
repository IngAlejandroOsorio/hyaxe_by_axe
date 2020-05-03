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
        public Trafico()
        {
            cargarPuntosTrafico();
        }
        public async Task cargarPuntosTrafico ()
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

                            NAPI.Task.Run(async () =>
                            {
                               // Data.Lists.puntosTraficoIl.Add(new Data.Entities.puntoTraficoIl(x, y, z, heading, false));
                            });
                        }
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

                    //Data.Lists.puntosTraficoIl.Add(new Data.Entities.puntoTraficoIl(x, y, z, heading, true));
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

        /*[Command ("traficararmas")]
        public async Task CMD_traficarArmas (Player player)
        {
            Data.Entities.User usr = player.GetData<Data.Entities.User>("USER_CLASS");
            if (usr.rank >= 5)
            {
                /if (usr.ilegalFaction.armasCortas | usr.ilegalFaction.armasLargas) 
                {
                    if(usr.ilegalFaction.traficob == null)
                    {
                        Data.Entities.Faction f = usr.ilegalFaction;
                        Random random = new Random();
                        int item = random.Next(Data.Lists.puntosTraficoIl.Count);
                        Data.Entities.puntoTraficoIl punto;

                        for (; ; )
                        {
                            punto = Data.Lists.puntosTraficoIl[item];
                            if (punto.activo)
                            {
                                break;
                            }
                            else
                            {
                                item++;
                            }

                            f.traficov = new Data.Entities.VehicleFaction();
                            f.traficov.faction = f.id;
                            f.traficov.model = Data.Lists.vehsTrafico[random.Next(Data.Lists.vehsTrafico.Count)];
                            f.traficov.entity = NAPI.Vehicle.CreateVehicle(NAPI.Util.VehicleNameToModel(f.traficov.model), punto.posiciones, punto.heading, random.Next(26), random.Next(26));
                            f.traficov.entity.EnginePowerMultiplier = 0;

                            f.traficob = NAPI.Blip.CreateBlip(1, punto.posiciones, 25, 36, "Alijo");

                            f.traficov.trucker = new Data.Entities.Inventory();

                            var t = f.traficov.trucker;
                            //Falta meter armas en el maletero

                            int milisecs = Convert.ToInt32(TimeSpan.FromHours(1).TotalMilliseconds); //Cooldown hasta desaparición veh
                            await Task.Delay(milisecs);

                            f.traficov.entity.Delete();
                            f.traficob = null;

                            milisecs = Convert.ToInt32(TimeSpan.FromHours(11).TotalMilliseconds); // Cooldown hasta poder hacer otro.
                            await Task.Delay(milisecs);
                            f.traficov = null;


                        }
                    }
                    else
                    {
                        Utilities.Notifications.SendNotificationERROR(player, "Aún tienes un tráfico pendiente o un cooldown");
                    }
                }
                else
                {
                    Utilities.Notifications.SendNotificationERROR(player, "Tu facción no está habilitada para traficar con armas");
                }

            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "Tienes que estar en una facció ilegal con rango 5 o superior");
            }
        }*/

    }
}
