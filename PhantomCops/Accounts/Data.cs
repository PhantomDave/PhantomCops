namespace PhantomCops.Accounts
{
	public class Data
	{
		public Data() { }

		public int dbID { get; set; }
		public string username { get; set; }
		public string email { get; set; }
		public string password { get; set; }
		public string RegIP { get; set; }
		public string LastIP { get; set; }
		public int Admin { get; set; }
		public int Team { get; set; }
	}
}
