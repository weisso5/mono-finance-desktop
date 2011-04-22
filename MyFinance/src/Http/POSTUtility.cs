using System;
using System.Text;
using System.IO;
using System.Net;

namespace MyFinance
{
	public class POSTUtility
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(POSTUtility));
		
		
		private string URL_TARGET {get;set;}
		private string HEADER_TARGET {get;set;}
		private string SERVICE_TARGET {get;set;}
		
		public POSTUtility (string targetURI, string headerTarget, string service)
		{
			this.URL_TARGET = targetURI;
			this.HEADER_TARGET = headerTarget;
			this.SERVICE_TARGET = service;
		}
		
		
		/// <summary>
		/// Authentication HTTP Post to Google for login
		/// </summary>
		/// <param name="sEmail">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="sPassword">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="sErrorMsg">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="sAuthHeader">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public bool AuthPost(string sEmail, string sPassword, out string sErrorMsg, out string sAuthHeader ){
			try{
				sErrorMsg = string.Empty;
				sAuthHeader = string.Empty;
				
				log.DebugFormat("AuthPost Attempt to => {0}",URL_TARGET);
				
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL_TARGET);
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.SendChunked = false;
				request.Headers.Add("GData-Version",Google.GDATA_VERSION);
				
				StringBuilder requestParams = new StringBuilder();
				requestParams.Append("accountType=HOSTED_OR_GOOGLE&")
				.AppendFormat("Email={0}",sEmail).Append("&")
				.AppendFormat("Passwd={0}",sPassword).Append("&")
				.AppendFormat("service={0}",SERVICE_TARGET).Append("&")
				.AppendFormat("source={0}",Google.SOURCE_HEADER);
				
				log.DebugFormat("Params => {0}",requestParams.ToString());
				
				using(Stream stream = request.GetRequestStream())
				{
					using(StreamWriter writer = new StreamWriter(stream))
					{
						writer.Write(requestParams.ToString().Normalize());
					}
				}
				
				HttpWebResponse response = (HttpWebResponse) request.GetResponse();
				if(response.StatusCode != HttpStatusCode.OK)
				{
					sAuthHeader = string.Empty;
					StreamReader reader = new StreamReader(response.GetResponseStream());
					if(reader != null){
						sErrorMsg = reader.ReadToEnd();
					}else{
						sErrorMsg = "General Error";
					}
					return false;
				} else {
					StreamReader reader = new StreamReader(response.GetResponseStream());
					if(reader != null)
					{
						string responseBody = reader.ReadToEnd();
						log.DebugFormat("Response => {0}",responseBody);
						string[] responseParams = responseBody.Split(new char[] {'\n'});
						foreach (string s in responseParams)
						{
							log.DebugFormat("Param => {0}",s);
							string[] inner = s.Split(new char[] {'='});
							if(inner[0] == Google.AUTH_HEADER_NAME)
							{
								sAuthHeader = inner[1];
							}
							else{
								continue;
							}
						}
						sErrorMsg = string.Empty;
						return true;
					}
				}
				return false;
				
			}catch(WebException wex){
				log.Error(wex.Message);
				sErrorMsg = wex.Message;
				sAuthHeader = string.Empty;
				return false;
			}
		}
		
	}
}

