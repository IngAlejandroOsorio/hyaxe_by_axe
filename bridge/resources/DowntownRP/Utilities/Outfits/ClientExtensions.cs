using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Utilities.Outfits
{
    public static class ClientExtensions
    {
        public static void SetOutfit(this Client player, int ID, bool save = false)
        {
            switch ((PedHash)player.Model)
            {
                case PedHash.FreemodeMale01:
                    if (ID < 0 || ID >= Main.MaleOutfits.Count) return;

                    for (int i = 0; i < Main.MaxComponent; i++)
                    {
                        if (i == 2) continue;
                        player.SetClothes(i, Main.MaleOutfits[ID].Components[i].Item1, Main.MaleOutfits[ID].Components[i].Item2);

                        /*if (save)
                        {
                            var clothes = Data.Character[player].Clothes.FirstOrDefault(c => c.IsProp == false && c.Slot == i);
                            if (clothes == null)
                            {
                                clothes = new CharacterClothes { Character = Data.Character[player], Slot = i, Drawable = Main.MaleOutfits[ID].Components[i].Item1, Texture = Main.MaleOutfits[ID].Components[i].Item2, IsProp = false };
                                Data.Character[player].Clothes.Add(clothes);
                            }
                            else
                            {
                                clothes.Drawable = Main.MaleOutfits[ID].Components[i].Item1;
                                clothes.Texture = Main.MaleOutfits[ID].Components[i].Item2;
                            }
                        }*/
                    }

                    for (int i = 0; i < Main.MaxProp; i++)
                    {
                        player.ClearAccessory(i);
                        player.SetAccessories(i, Main.MaleOutfits[ID].Props[i].Item1, Main.MaleOutfits[ID].Props[i].Item2);

                        /*if (save)
                        {
                            var clothes = Data.Character[player].Clothes.FirstOrDefault(c => c.IsProp == true && c.Slot == i);
                            if (clothes == null)
                            {
                                clothes = new CharacterClothes { Character = Data.Character[player], Slot = i, Drawable = Main.MaleOutfits[ID].Props[i].Item1, Texture = Main.MaleOutfits[ID].Props[i].Item2, IsProp = true };
                                Data.Character[player].Clothes.Add(clothes);
                            }
                            else
                            {
                                clothes.Drawable = Main.MaleOutfits[ID].Props[i].Item1;
                                clothes.Texture = Main.MaleOutfits[ID].Props[i].Item2;
                            }
                        }*/
                    }

                    break;

                case PedHash.FreemodeFemale01:
                    if (ID < 0 || ID >= Main.FemaleOutfits.Count) return;

                    for (int i = 0; i < Main.MaxComponent; i++)
                    {
                        if (i == 2) continue;
                        player.SetClothes(i, Main.FemaleOutfits[ID].Components[i].Item1, Main.FemaleOutfits[ID].Components[i].Item2);

                        /*if (save)
                        {
                            var clothes = Data.Character[player].Clothes.FirstOrDefault(c => !c.IsProp && c.Slot == i);
                            if (clothes == null)
                            {
                                clothes = new CharacterClothes { Character = Data.Character[player], Slot = i, Drawable = Main.FemaleOutfits[ID].Components[i].Item1, Texture = Main.FemaleOutfits[ID].Components[i].Item2, IsProp = false };
                                Data.Character[player].Clothes.Add(clothes);
                            }
                            else
                            {
                                clothes.Drawable = Main.FemaleOutfits[ID].Components[i].Item1;
                                clothes.Texture = Main.FemaleOutfits[ID].Components[i].Item2;
                            }
                        }*/
                    }

                    for (int i = 0; i < Main.MaxProp; i++)
                    {
                        player.ClearAccessory(i);
                        player.SetAccessories(i, Main.FemaleOutfits[ID].Props[i].Item1, Main.FemaleOutfits[ID].Props[i].Item2);

                        /*if (save)
                        {
                            var clothes = Data.Character[player].Clothes.FirstOrDefault(c => c.IsProp && c.Slot == i);
                            if (clothes == null)
                            {
                                clothes = new CharacterClothes { Character = Data.Character[player], Slot = i, Drawable = Main.FemaleOutfits[ID].Props[i].Item1, Texture = Main.FemaleOutfits[ID].Props[i].Item2, IsProp = true };
                                Data.Character[player].Clothes.Add(clothes);
                            }
                            else
                            {
                                clothes.Drawable = Main.FemaleOutfits[ID].Props[i].Item1;
                                clothes.Texture = Main.FemaleOutfits[ID].Props[i].Item2;
                            }
                        }*/
                    }

                    break;
            }
        }


    }
}
