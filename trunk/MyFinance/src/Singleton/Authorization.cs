using System;
namespace MyFinance
{
	/// <summary>
	/// Singleton implementing Double Checked Locking
	/// 
	/// Holds:
	/// Auth Token
	/// Auth_User
	/// 
	/// 
	/// </summary>
	public sealed class Authorization
	{
		private static volatile Authorization instance;
		private static object syncRoot = new Object();
		
		private Authorization() {}
		
		public static Authorization Instance {
			
			get {
				if(instance == null)
				{
					lock (syncRoot) 
            		{
               			if (instance == null) 
                  		instance = new Authorization();
            		}

				}
				return instance;
			}
		}
		
		public string Auth_Value {get;set;}
		public string Auth_User {get;set;}
	}
}

