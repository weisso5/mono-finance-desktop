using System;
using System.IO;
using System.Net;
using System.Xml;
using MyFinance;


namespace MyFinance
{
	public class GETUtility
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(GETUtility));
		
		private string TARGET_URI {get;set;}
		private string TARGET_SERVICE {get;set;}
		
		public GETUtility (string sTargetURI, string sTargetService)
		{
			this.TARGET_URI = sTargetURI;
			this.TARGET_SERVICE = sTargetService;
		}
		
		public XmlDocument DataGet(string sPath, string sQuery)
		{
			XmlDocument xdoc = new XmlDocument();
			try {
				log.DebugFormat("Attempting GET on {0} with {0}",sPath,sQuery);
				
				string requestURI = TARGET_URI + sPath;
				if(!String.IsNullOrEmpty(sQuery))
				{
					requestURI += "?" + sQuery;
				}
				
				HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestURI);
				
				request.Method = "GET";
				request.ContentType = "text/xml";
				request.SendChunked = false;
				request.Headers.Add("GData-Version",Google.GDATA_VERSION);
				request.Headers.Add("Authorization","GoogleLogin auth=" + Authorization.Instance.Auth_Value);
				
				HttpWebResponse response = (HttpWebResponse) request.GetResponse();
				if(response.StatusCode == HttpStatusCode.OK){
					StreamReader reader = new StreamReader(response.GetResponseStream());
					if (reader != null)
					{
						string responseString = reader.ReadToEnd();
						log.DebugFormat("Response String {0}",responseString);
						xdoc.LoadXml(responseString);
						reader.Dispose();
					}
				}
				
			} catch (Exception ex) {
				log.Error(ex.Message);
			}
			
			return xdoc;
		}
		
		
	}
}

