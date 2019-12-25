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
        [RemoteEvent("UpdatePlayerParents")]
        public void UpdatePlayerParents(Client player, byte father, byte mother, float mix)
        {
            var blend = new HeadBlend()
            {
                ShapeFirst = father,
                ShapeSecond = mother,
                ShapeMix = mix,
                ShapeThird = 0
            };
            NAPI.Player.SetPlayerHeadBlend(player, blend);
        }
        // Confirm Parents
    }
}
