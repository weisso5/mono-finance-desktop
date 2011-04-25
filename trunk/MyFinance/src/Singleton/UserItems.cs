using System;
using System.Collections.Generic;

namespace MyFinance
{
	/// <summary>
	/// User items.
	/// This class contains Objects persisted across threads for the Session
	/// </summary>
	public sealed class UserItems
	{
		private static volatile UserItems instance;
		private static object syncRoot = new Object();
		
		private UserItems()
		{
			this.Portfolios = new List<Portfolio>();
		}
		
		public static UserItems Instance {
			
			get {
				if(instance == null)
				{
					lock (syncRoot) 
            		{
               			if (instance == null) 
                  		instance = new UserItems();
            		}

				}
				return instance;
			}
		}
		
		public List<Portfolio> Portfolios {get;set;}
		
		
	}
}

