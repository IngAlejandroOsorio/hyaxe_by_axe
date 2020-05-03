using RAGE;
using RAGE.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs
{
    public class Vehicle : Events.Script
    {
        public Vehicle()
        {
            Events.Add("open_vehicle_trunk", open_vehicle_trunk);
            Events.Add("close_vehicle_trunk", close_vehicle_trunk);
            Events.Add("open_vehicle_hood", open_vehicle_hood);
            Events.Add("close_vehicle_hood", close_vehicle_hood);
        }

        private void open_vehicle_hood(object[] args)
        {
            RAGE.Elements.Vehicle vehicle = (RAGE.Elements.Vehicle)args[0];
            vehicle.SetDoorOpen(4, false, true);
        }

        private void close_vehicle_hood(object[] args)
        {
            RAGE.Elements.Vehicle vehicle = (RAGE.Elements.Vehicle)args[0];
            vehicle.SetDoorShut(4,true);
        }

        private void open_vehicle_trunk(object[] args)
        {
            RAGE.Elements.Vehicle vehicle = (RAGE.Elements.Vehicle)args[0];
            vehicle.SetDoorOpen(5, false, true);
        }

        private void close_vehicle_trunk(object[] args)
        {
            RAGE.Elements.Vehicle vehicle = (RAGE.Elements.Vehicle)args[0];
            vehicle.SetDoorShut(5, true);
        }
    }
}
