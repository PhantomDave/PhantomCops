using System;
using GTANetworkAPI;

namespace PhantomCops
{
	public class Main : Script
	{
		[ServerEvent(Event.ResourceStart)]
		public void OnResourceStart()
		{
			Log("===============================");
			Log($"Starting {Config.Name}");
			Log($"Version: {Config.Ver}");
			Log($"Developed by: {Config.Developer}");
			Log("===============================");
			Database.Init();
			Log("Database Initialized");
			Log("===============================");
			Log($"{Config.Name} Started!");
			Log("===============================");
				
		}
		public static void Log(string text)
		{
			NAPI.Util.ConsoleOutput("[Phantom-Logs] " + text);
		}
	}
}
