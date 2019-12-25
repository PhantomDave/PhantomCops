using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhantomCops.Accounts
{
    public class Events : Script
    {
        [RemoteEvent("RegisterPlayer")]
        public void RegisterPlayer(Client player, string username, string password, string email)
        {
            if (Database.IsRegistered(email))
            {
                player.TriggerEvent("showError", "Questa email è già registrata");
            }
            else
            {
                Database.RegisterPlayer(player, username, password, email);
                Main.Log($"Registrato il player {username} con la seguente E-MAIL {email}");
                player.TriggerEvent("reloadCEF", "package://html/Login/index.html");
                player.TriggerEvent("showSuccess", "Ora esegui il login");
            }
        }

        [RemoteEvent("LoginPlayer")]
        public void LoginPlayer(Client player, string email, string password)
        {
            Main.Log("LoginPlayer");

            if (Database.CheckLogin(player, email, password))
            {
                player.TriggerEvent("GetReadyToPlay");
                SetupPlayerForCharacterCreation(player);
                player.TriggerEvent("ShowCharacterCreator");
            }
            else
            {
                player.TriggerEvent("showError", "Credenziali errate");
            }

        }

        uint creatorDim = 0;
        public void SetupPlayerForCharacterCreation(Client player)
        {
            NAPI.Player.SpawnPlayer(player, new Vector3(402.8664, -996.4108, -99.00027), -185);
            player.Dimension = creatorDim;
            ++creatorDim;
           
        }
    }
}
