using DowntownRP.Data.Entities;
using GTANetworkAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;

namespace DowntownRP.World.Npcs
{
    public class Npcs : Script
    {
        //NPCs Functions

        [RemoteEvent("CrearNpcServer")]
        public static async void CrearNpcServer(Player player, double x, double y, double z, double heading, string nombre, string tipo)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 1)
            {
                Vector3 pos = new Vector3(x, y, z);

                Ped ped = NAPI.Ped.CreatePed((uint)NAPI.Util.PedNameToModel(tipo), pos, (float)heading, true, true, true, false);
                ped.SetSharedData("NPC_NOMBRE_SHARED", nombre);
                ped.SetSharedData("NPC_SHARED", ped);
                ped.SetData("NPC_NOMBRE", nombre);

                ColShape pedpoint = NAPI.ColShape.CreateCylinderColShape(pos, 10, 5);
                //NAPI.TextLabel.CreateTextLabel("~o~ Presiona Enter ~w~para interactuar", pos.Subtract(new Vector3(0, 0, 0.2)), 4, 2, 2, new Color(0, 0, 0));
                pedpoint.SetSharedData("NPC_NOMBRE_SHARED", nombre);
                pedpoint.SetData("NPC_NOMBRE", nombre);
                pedpoint.SetData("NPC_PED", ped);

                using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO npcs (nombre,x,y,z,heading,tipo) VALUES (@nombre, @x, @y, @z, @heading, @tipo)";
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@x", x);
                    command.Parameters.AddWithValue("@y", y);
                    command.Parameters.AddWithValue("@z", z);
                    command.Parameters.AddWithValue("@heading", heading);
                    command.Parameters.AddWithValue("@tipo", tipo);

                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            else player.SendChatMessage("~r~[ERROR]~w~ El comando no existe. (/ayuda para mas información)");

        }


        [RemoteEvent("tomarDineroServer")]
        public static async System.Threading.Tasks.Task tomarDineroServer(Player player, int precio)
        {
            if (await Game.Money.MoneyModel.AddMoney(player, precio))
            {
                Utilities.Notifications.SendNotificationINFO(player, $"Has tomado {precio} del suelo.");
            }
        }

        public static async void SpawnNPCs()
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM npcs";

                bool dados = new Random().Next(1, 11) % 80 == 0;

                DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string nombre = reader.GetString(reader.GetOrdinal("nombre"));
                        string tipo = reader.GetString(reader.GetOrdinal("tipo"));
                        double x = reader.GetDouble(reader.GetOrdinal("x"));
                        double y = reader.GetDouble(reader.GetOrdinal("y"));
                        double z = reader.GetDouble(reader.GetOrdinal("z"));
                        double heading = reader.GetDouble(reader.GetOrdinal("heading"));
                        //PedHash pedy = PedHash.Beach01AFY;
                        //uint tipo3 = Convert.ToUInt32(tipo);

                        Vector3 pos = new Vector3(x, y, z);

                        //NAPI.LocalEvent.TriggerEvent("spawnNpcsJS", Convert.ToString(x) , Convert.ToString(z));
                        NAPI.Task.Run(() =>
                        {
                            //player.TriggerEvent("npc_server", Convert.ToString(x), Convert.ToString(y), Convert.ToString(z), Convert.ToString(heading), Convert.ToString(nombre), Convert.ToString(NAPI.Util.PedNameToModel(tipo)));
                            //Ped ped = NAPI.Ped.CreatePed(NAPI.Util.PedNameToModel(tipo), pos, (float)heading);
                            Ped ped;
                            if (dados)
                            {
                                ped = NAPI.Ped.CreatePed((uint)NAPI.Util.PedNameToModel(tipo), pos, (float)heading, true, false, false, false);
                            }
                            else
                            {
                                ped = NAPI.Ped.CreatePed((uint)NAPI.Util.PedNameToModel(tipo), pos, (float)heading, true, true, true, false);
                            }
                            ped.SetSharedData("NPC_NOMBRE_SHARED", nombre);
                            ped.SetSharedData("NPC_SHARED", ped);
                            ped.SetData("NPC_NOMBRE", nombre);

                            ColShape pedpoint = NAPI.ColShape.CreateCylinderColShape(pos, 10, 5);
                            //NAPI.TextLabel.CreateTextLabel("~o~ Presiona Enter ~w~para interactuar", pos.Subtract(new Vector3(0, 0, 0.2)), 4, 2, 2, new Color(0, 0, 0));
                            pedpoint.SetSharedData("NPC_NOMBRE_SHARED", nombre);
                            pedpoint.SetData("NPC_NOMBRE", nombre);
                            pedpoint.SetData("NPC_PED", ped);
                            pedpoint.SetData("NPC_PED_ID", id);
                            pedpoint.SetData("NPC_PED_AGRESIVO", dados);

                            //pedpoint.SetSharedData("NPC_NOMBRE_SHARED", nombre);
                            //ped.SetSharedData("NPC_NOMBRE_SHARED", nombre);
                            //                            
                        });

                    }
                }


            }
        }

        [RemoteEvent("borrarNpcServer")]
        public static async void borrarNpcServer(Player player, int id, ColShape shape, Ped ped)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 1)
            {
                shape.Delete();
                ped.Delete();

                using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM npcs WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            else player.SendChatMessage("~r~[ERROR]~w~ El comando no existe. (/ayuda para mas información)");

        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void entraEnRangodeNpc(ColShape shape, Player player)
        {
            if (!shape.HasData("NPC_PED")) return;
            Ped pedy = shape.GetData<Ped>("NPC_PED");
            String nombre = shape.GetData<String>("NPC_NOMBRE");
            int id = shape.GetData<int>("NPC_PED_ID");
            bool agresivo = shape.GetData<bool>("NPC_PED_AGRESIVO");
            int tombos = 0;
            foreach (Data.Entities.User u in Data.Lists.playersConnected)
            {
                if (u.faction == 1 & u.factionDuty == true)
                { tombos = tombos + 1; }
            }
            //Console.WriteLine($"Entro a robar a {nombre}");
            //player.SendChatMessage($"Entro a robar a {nombre}");            
            //ped.PlayAnimTask("mp_am_hold_up", "holdup_victim_20s");
            //PlayFacialAnimTask("facials@gen_male@base", "shocked_1");
            //NAPI.Ped.PlayPedAnimation(pedy, true, "mp_am_hold_up", "handsup_base");
            player.TriggerEvent("dentroNPC", pedy, nombre, id, shape, agresivo, tombos);

            //if (player.GetData<ColShape>("BOOMBOX_COL").GetData<Player>("BOOMBOX_OWNER") == player)

        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void saledeRangodeNpc(ColShape shape, Player player)
        {
            String nombre = shape.GetData<String>("NPC_NOMBRE");
            //Console.WriteLine($"Salgo de robar a {nombre}");
            //player.SendChatMessage($"{nombre}");
            player.TriggerEvent("salgoNPC", nombre);
        }

        [RemoteEvent("avisoTombos")]
        public void avisoTombos(Player player, string msg)
        {

            var entorno = new Data.Entities.Entorno(player, msg);

            entorno.pd = true;

            Data.Lists.entornos.Add(entorno);

            foreach (Data.Entities.User u in Data.Lists.playersConnected)
            {
                if (u.faction == 1 & u.factionDuty == true & entorno.pd)
                {
                    u.entity.SendChatMessage($"[ENTORNO *] ID: {entorno.userid} - {entorno.Pj} => {msg}");
                    u.entity.TriggerEvent("aviso-robo", msg, player.Position.X, player.Position.Y, player.Position.Z);
                }
                else if (u.faction == 2 & u.factionDuty == true & entorno.md)
                {
                    u.entity.SendChatMessage($"[ENTORNOS *AUTO] ID: {entorno.userid} - {entorno.Pj} => {msg}");
                    //u.entity.TriggerEvent("aviso-robo", msg, player.Position.X, player.Position.Y, player.Position.Z);
                }
            }
        }

        [RemoteEvent("avisoTombosSilen")]
        public void avisoTombosSilen(Player player, string msg)
        {

            var entorno = new Data.Entities.Entorno(player, msg);

            entorno.pd = true;

            Data.Lists.entornos.Add(entorno);

            foreach (Data.Entities.User u in Data.Lists.playersConnected)
            {
                if (u.faction == 1 & u.factionDuty == true & entorno.pd)
                {
                    u.entity.SendChatMessage($"[ENTORNO *AUTO] ID: {entorno.userid} - {entorno.Pj} => {msg}");
                }
                else if (u.faction == 2 & u.factionDuty == true & entorno.md)
                {
                    u.entity.SendChatMessage($"[ENTORNO *AUTO] ID: {entorno.userid} - {entorno.Pj} => {msg}");
                }
            }
        }


        [RemoteEvent("pagoRobo")]
        public async Task pagoRoboAsync(Player player, int precio)
        {
            if (await Game.Money.MoneyModel.AddMoney(player, precio))
            {
                Utilities.Notifications.SendNotificationINFO(player, $"Has Robado ${precio} de este negocio, deberías correr.");
            }
        }

        [RemoteEvent("fpsync.update")]
        public static void FingerPoint(Player sender, float camPitch, float camHeading)
        {
            NAPI.ClientEvent.TriggerClientEventInRange(sender.Position, 100, "fpsync.update", sender.Handle, camPitch, camHeading);
        }
    }
}
