using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using DowntownRP.Utilities;
using System.Threading.Tasks;

namespace DowntownRP.World.Factions.MD
{
    public class Incendio : Script
    {
        public bool estado; 
        public Vector3 posInicial;
        public Vector3 rotInicial;
        public List<GTANetworkAPI.Object> fueguitos;
        public Task creandoFuegos;


        [Command("fuego")]
        public void CMD_Fuego (Player player)
        {
            if(AdminLVL.PuedeUsarComando(player, 3))
            {
                if (estado)
                {
                    estado = false;
                    foreach (GTANetworkAPI.Object obj in fueguitos)
                    {
                        NAPI.Task.Run(() =>
                        {
                            obj.Delete();
                        }
                        );


                        return;

                    }
                    creandoFuegos.Dispose();
                }
                else
                {

                    estado = !estado;
                    posInicial = player.Position;
                    rotInicial = player.Rotation;

                    Crearllamas(player.Position, player.Rotation);
                    creandoFuegos = CreandoFuegos();
                    creandoFuegos.Start();

                    //  fueguitos.Add(Crearllamas(player.Position, player.Rotation));
                }

                

            }
        }

        public static GTANetworkAPI.Object Crearllamas (Vector3 pos, Vector3 rot)
        {
            GTANetworkAPI.Object obj = null;

            obj = NAPI.Object.CreateObject(509226741, pos.Subtract(new Vector3(0, 0, -1)), rot);


            obj.SetData<bool>("FUEGO_LSFD", true);
            return obj;
        }

        public async Task CreandoFuegos()
        {
            for(; ; )
            { 
                await Task.Delay(30000);
                fueguitos.Add(Crearllamas(posInicial.Around(fueguitos.Count), rotInicial));
            }

            return;
        }
    }
}
