using System;
using System.Xml;

namespace MyFinance
{
	/// <summary>
	/// Portfolio Entity
	/// </summary>
	public class Portfolio
	{
		
		private string ID {get;set;}
		private DateTime Updated {get;set;}
		private DateTime Edited {get;set;}
		private string Title {get;set;}
		private string FeedLink {get;set;}
		
		//TODO Add Portfolio Data
		
		public Portfolio ()
		{
		}
		
		public Portfolio(XmlNode portfolioNode){
			ParseNode(portfolioNode);	
		}
		
		private void ParseNode(XmlNode node)
		{
			this.ID = node.SelectNodes("id").Item(0).InnerText;
			this.Updated = DateTime.Parse(node.SelectNodes("updated").Item(0).InnerText);
			this.Edited  = DateTime.Parse(node.SelectNodes("edited").Item(0).InnerText);
			this.Title = node.SelectNodes("title").Item(0).InnerText;
			this.FeedLink = node.SelectNodes("feedLink").Item(0).Attributes["href"].Value;
		}
	}
}

