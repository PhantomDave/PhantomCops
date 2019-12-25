using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhantomCops
{
    public class Events : Script
    {
        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Client player)
        {
            player.SendChatMessage($"Welcome to {Config.Name}!");
            player.TriggerEvent("reloadCEF", "package://html/Login/index.html");
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Client player)
        {
            Accounts.Controller.SaveAccount(player);
        }
    }
}
