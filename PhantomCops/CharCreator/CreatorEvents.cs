using GTANetworkAPI;
using GTANetworkMethods;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhantomCops.CharCreator
{
    public class CreatorEvents : Script
    {
        [RemoteEvent("ChangePlayerGender")]
        public void ChangeGender(Client player, int gender)
        {
            switch (gender)
            {
                case 0:
                {
                    NAPI.Player.SetPlayerSkin(player, PedHash.FreemodeMale01);
                    break;
                }
                case 1:
                { 
                    NAPI.Player.SetPlayerSkin(player, PedHash.FreemodeFemale01);
                    break;
                }
            }
        }
        [RemoteEvent("ChangePlayerParents")]
        public void ChangeParents(Client player, int father, int mother)
        {
           // NAPI.Player.SetPlayerHeadBlend
        }
    }
}
