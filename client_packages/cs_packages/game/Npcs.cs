using Newtonsoft.Json;
using RAGE;
using RAGE.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game
{
    public class Npcs : Events.Script
    {
        private Ped npcActual;
        private String npcActualNombre;

        public Npcs()
        {
            Events.Add("dentroNPC", dentroNPC);
            Events.Add("salgoNPC", salgoNPC);
            Events.Add("cagadaRoboNpc", cagadaRoboNpc);
            Events.Add("roboNpcListo", roboNpcListo);


        }

        private void dentroNPC(object[] args)
        {
            RAGE.Elements.Ped pedy = (RAGE.Elements.Ped)args[0];
            int id = (int)args[2];
            RAGE.Elements.Colshape shape = (RAGE.Elements.Colshape)args[3];
            npcActual = pedy;
            string nombre = (String)args[1];
            npcActualNombre = nombre;
            bool agresivo = (bool)args[4];
            if (agresivo) { 
                RAGE.Game.Weapon.GiveWeaponToPed(npcActual.Handle, 453432689, 500, false, true);
            }
            int tombos = (int)args[5];
            RAGE.Events.CallLocal("entroNPC", pedy, id, shape, tombos, nombre);

            /*RAGE.Game.Streaming.RequestAnimDict("amb@code_humand_wander_clipboard@male@idle_a");
            pedy.TaskPlayAnim("mp_am_hold_up", "handsup_base", 1f, 1, 4000, 0, 1, true, true, true);
            pedy.PlayAnim("mp_am_hold_up", "handsup_base", 1f,true,true,true,1f,1);            */
            //RAGE.Chat.Output($"el Mico es {(String)args[1]} en {pedy.Position.X}");
        }

        private void salgoNPC(object[] args)
        {
            //npcActual.ClearTasksImmediately();
        }

        private void cagadaRoboNpc(object[] args)
        {
            //RAGE.Chat.Output($"el Mico en {npcActual.Position.X}");
            //npcActual.ClearTasksImmediately();
        }

        private void roboNpcListo(object[] args)
        {
            RAGE.Chat.Output($"{npcActualNombre} ha accedido a darte todo su dinero incluyendo el oculto.");
            Random r = new Random();
            int rInt = r.Next(500, 1000);
            RAGE.Game.Streaming.RequestAnimDict("random@shop_robbery");//preload the animation
            npcActual.TaskPlayAnim("random@shop_robbery", "robbery_action_f", 1f, 1f, -1, 1, 1f, false, false, false);
            RAGE.Events.CallRemote("pagoRobo", rInt);
            npcActual.ClearTasks();
            RAGE.Events.CallLocal("chat_goal", "¡Felicidades!", "Realizado el robo con éxito, destruye las cajas coje el dinero y corre!");
            RAGE.Events.CallLocal("finalizoRoboJs", npcActual); 
        }


    }
}
