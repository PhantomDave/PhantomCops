using GTANetworkAPI;
using GTANetworkMethods;
using System;
using System.Data;

namespace PhantomCops.Accounts
{

	public class Controller
	{
		public Data Account;
		public Client Client { get; private set; }

		public Controller(Data accounts, Client player)
		{
			Account = accounts;
			Client = player;

			player.SetData("Account", this);
			player.Name = Account.username;

			UpdateAccountData(player);
		}

		public static void UpdateAccountData(Client player)
		{
			var account = GetData(player);
			if (account == null) return;
			
			player.SetData("dbID", account.Account.dbID);
			player.SetData("username", account.Account.username);
			player.SetData("lastIP", account.Account.LastIP);
			player.SetData("Admin", account.Account.Admin);
		}

		public static Controller GetData(Client Player)
		{
			var returned = Player.HasData("Account") ? Player.GetData("Account") as Controller : null;
			return returned;
		}

		public static bool LoadPlayerAccount(Client Player, DataTable tales)
		{
			var user = tales.Rows[0];

			Data accounts = new Data
			{
				username = user["username"].ToString(),
				password = user["password"].ToString(),
				RegIP = user["regip"].ToString(),
				Admin = Convert.ToInt32(user["admin"]),
			};

			new Controller(accounts, Player);

			return true;
		}

		public static bool SaveAccount(Client player)
		{
			var playerData = GetData(player);

			Database.Query("UPDATE accounts SET " +
				"lastip = '" + player.Address + "', " +
				"WHERE username " + playerData.Account.username + "'"
			);

			player.ResetData("Account");

			return true;
		}
	}
}
