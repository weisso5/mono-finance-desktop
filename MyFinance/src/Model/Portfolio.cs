using System;
using System.Xml;
using System.Xml.XPath;

namespace MyFinance
{
	/// <summary>
	/// Portfolio Entity
	/// </summary>
	public  sealed class Portfolio
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(MainWindow));
		
		public string ID {get;set;}
		public string eTag {get;set;}
		public DateTime Updated {get;set;}
		public DateTime Edited {get;set;}
		public string Title {get;set;}
		public string FeedLink {get;set;}
		
		//TODO Add Portfolio Data
		
		public Portfolio ()
		{
		}
		
		public Portfolio(string portfolioNodeOuterXml){
			ParseNode(portfolioNodeOuterXml);	
		}
		
		private void ParseNode(string outerXML)
		{
			XmlDocument node = new XmlDocument();
			node.LoadXml(outerXML);
			
			if(!node.HasChildNodes)
			{
				return; //skip childless
			}
			
			try {				
				this.ID = node.GetElementsByTagName("id").Item(0).InnerText.Replace("http://","").Split('/')[5];
				this.eTag = node.GetElementsByTagName("entry").Item(0).Attributes.GetNamedItem("etag").Value; // save this for If-none-Match
				this.Updated = DateTime.Parse(node.GetElementsByTagName("updated").Item(0).InnerText);
				this.Edited  = DateTime.Parse(node.GetElementsByTagName("edited").Item(0).InnerText);
				this.Title = node.GetElementsByTagName("title").Item(0).InnerText;
				this.FeedLink = node.GetElementsByTagName("feedlink").Item(0).Attributes["href"].Value;
			} catch (XmlException ex) {
				log.Warn(ex.Message);
			} catch (XPathException ex1) {
				log.Warn(ex1.Message);
			} catch (NullReferenceException ex2)
			{
				log.Warn(ex2.Message);
			}
		}
	}
}

