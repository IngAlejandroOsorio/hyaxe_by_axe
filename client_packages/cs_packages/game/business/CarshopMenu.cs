using RAGE;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP_cs.game.business
{
    public class CarshopMenu : Events.Script
    {
        private bool isOpen = false;
        private bool isRent = false;
        private bool isRenting = false;
        private MenuPool _menuPool;
        private int price = 0;
        private string color = "Negro";

        public CarshopMenu()
        {
            Events.Add("OpenBuyCarMenu", OpenBuyCarMenu);
            Events.Add("OpenRentCarMenu", OpenRentCarMenu);
            Events.Add("IniciaContadorRentaCar", IniciaContadorRentaCar); 
            Events.Add("devolverRentado", TerminarContadorRenta);
            RAGE.Events.Tick += DrawMenu;
        }

        private void DrawMenu(List<Events.TickNametagData> nametags)
        {
            if (isOpen) _menuPool.ProcessMenus();
        }

        private void OpenBuyCarMenu(object[] args)
        {
            isOpen = true;
            RAGE.Chat.Show(false);
            price = (int)args[2];

            _menuPool = new MenuPool();
            var mainMenu = new UIMenu(args[0].ToString(), "~b~Venta de vehículo | " + args[1].ToString());
            mainMenu.FreezeAllInput = true;

            _menuPool.Add(mainMenu);
            SeleccionarColor(mainMenu);
            Comprar(mainMenu);
            _menuPool.RefreshIndex();
            mainMenu.Visible = true;
            mainMenu.OnItemSelect += MainMenu_OnItemSelect;

            mainMenu.OnMenuClose += MainMenu_OnMenuClose;
        }
        
        private async void IniciaContadorRentaCar(object[] args)
        {
            isRenting = true;
            do
            {
                await Task.Delay(60000);
                Events.CallRemote("RentVehiclePin");
            }
            while (isRenting); 
        }
        
        private void TerminarContadorRenta(object[] args)
        {
            isRenting = false;
            Events.CallRemote("RentVehicleFinish");
        }

        private void OpenRentCarMenu(object[] args)
        {
            if (isRenting)
            {
                Events.CallRemote("SendNotificationUser", "~g~ERROR: ~w~ Ya tienes un coche alquilado solo puedes usar uno al tiempo");
            }
            else
            {
                isRent = true;
                isOpen = true;
                RAGE.Chat.Show(false);
                price = (int)args[2];

                _menuPool = new MenuPool();
                var mainMenu = new UIMenu(args[0].ToString(), "~b~Renta de vehículo | " + args[1].ToString());
                mainMenu.FreezeAllInput = true;

                _menuPool.Add(mainMenu);
                SeleccionarColor(mainMenu);
                Rentar(mainMenu);
                _menuPool.RefreshIndex();
                mainMenu.Visible = true;
                mainMenu.OnItemSelect += MainMenu_OnItemSelect;

                mainMenu.OnMenuClose += MainMenu_OnMenuClose;
            }
        }

        private void SeleccionarColor(UIMenu mainMenu)
        {
            var foods = new List<dynamic>
        {
            "Negro",
            "Blanco",
            "Amarillo",
            "Rojo",
            "Azul",
            "Verde"
        };
            var newitem = new UIMenuListItem("Color del vehículo", foods, 0);
            mainMenu.AddItem(newitem);
            mainMenu.OnListChange += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    color = item.IndexToItem(index).ToString();
                }

            };
        }

        private void Comprar(UIMenu mainMenu)
        {
            var comprar = new UIMenuItem("Comprar", $"El precio del vehículo es de ~g~${price}");
            mainMenu.AddItem(comprar);
        }

        private void Rentar(UIMenu mainMenu)
        {

            var rentar = new UIMenuItem("Rentar", $"El precio de renta del vehículo es de ~g~${price / 1000} xMinuto");
            isRent = false;
            mainMenu.AddItem(rentar);
        }

        private void MainMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (selectedItem.Text == "Comprar") Events.CallRemote("BuyVehicleCarshop", color);
            if (selectedItem.Text == "Rentar") Events.CallRemote("RentVehicleCarshop", color);

            sender.Visible = false;
            isOpen = false;
            RAGE.Chat.Show(true);
        }

        private void MainMenu_OnMenuClose(UIMenu sender)
        {
            RAGE.Chat.Show(true);
        }
    }
}
