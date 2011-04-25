using System;
using System.Collections.Generic;
using System.Xml;
using Gdk;
using Gtk;
using MyFinance;


public partial class MainWindow : Gtk.Window
{
	private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(MainWindow));
	
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		log.Debug("Build Complete");
		
		//setup images
		Pixbuf pic = new Pixbuf("img/logo_us.png");
		imgLogo.Pixbuf = pic;
		
		Pixbuf pic2 = new Pixbuf("img/google_transparent.gif");
		imgLogo2.Pixbuf = pic2;
		
		//setup Labels
		lblIntroText.Text = "Sign in with your";
		
		lblIntroText2.UseMarkup = true;
		lblIntroText2.Markup = "<b>Account</b>";
		
		lblEmail.Text = "Email:";
		lblPassword.Text = "Password:";
		
		//set text views
		tvScrolllWindow.Visible = true;
		tvIntroText.Editable = false;
		tvIntroText.SetSizeRequest(200,300);
		
		
		//setup Entry Boxes
		
		eEmail.SetSizeRequest(200,30);
		eEmail.MaxLength = 200;
		eEmail.GrabFocus();
		
		ePassword.SetSizeRequest(200,30);
		ePassword.MaxLength = 200;
		ePassword.Visibility = false; 
		
		//buttons
		btnSubmit.Label = "Log in";
		
		//Progress bar
		pbLoading.Visible = false;
		pbLoading.Orientation = ProgressBarOrientation.RightToLeft;
		pbLoading.Text = "Logging In...";
		pbLoading.PulseStep = 10;
		
		//valid Images
		imgEmailValid.SetFromIconName("gtk-dialog-error",IconSize.SmallToolbar);
		imgEmailValid.TooltipText = string.Empty;
		//imgEmailValid.Visible = false;
		
		imgPasswordValid.SetFromIconName("gtk-dialog-error",IconSize.SmallToolbar);
		imgPasswordValid.TooltipText = string.Empty;
		//imgPasswordValid.Visible = false;
		
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	protected void BtnSubmit_Click(object sender, EventArgs e) 
	{
		if(String.IsNullOrEmpty(eEmail.Text))
		{
			this.Focus = eEmail;
			imgEmailValid.Visible = true;
			return;
		}
		
		if(String.IsNullOrEmpty(ePassword.Text))
		{
			imgPasswordValid.Visible = true;
			return;
		}
		
		imgEmailValid.SetFromIconName("gtk-apply",IconSize.SmallToolbar);
		
		pbLoading.Visible = true;
		pbLoading.Pulse();
		
		Color greyColor;
		Color.Parse("grey", ref greyColor);
		
		eEmail.IsEditable = false;
		eEmail.ModifyFg(StateType.Active,greyColor);
		ePassword.IsEditable = false;
		ePassword.ModifyFg(StateType.Active,greyColor);
		
		POSTUtility postUtility = new POSTUtility(Google.AUTH_URL,Google.AUTH_HEADER_NAME,Google.SERVICE);
		string sErrorMsg = string.Empty;
		string sAuthHeader = string.Empty;
		if(postUtility.AuthPost(eEmail.Text.Trim(),ePassword.Text.Trim(),out sErrorMsg, out sAuthHeader)){
			//success
			//Add to Singleton
			Authorization.Instance.Auth_Value = sAuthHeader;
			pbLoading.Pulse();
			pbLoading.Text = "Loading Portfolios";
			GETUtility getUtility = new GETUtility(Google.FINANCE_BASE_URL,Google.SERVICE);
			pbLoading.Pulse();
			try{
				
				XmlDocument xdoc = getUtility.DataGet("portfolios",string.Empty);
				XmlNamespaceManager namespaces = new XmlNamespaceManager(xdoc.NameTable);
				namespaces.AddNamespace("openSearch","http://a9.com/-/spec/opensearch/1.1/");
				namespaces.AddNamespace("gf","http://schemas.google.com/finance/2007");
				namespaces.AddNamespace("gd","http://schemas.google.com/g/2005");
				
				string[] id = xdoc.DocumentElement.GetElementsByTagName("id").Item(0).InnerText.Replace("http://","").Split('/');
				Authorization.Instance.Auth_User = id[3]; //HACK
				
				pbLoading.Pulse();
				if(xdoc != null && xdoc.HasChildNodes)
				{
					log.DebugFormat("Portfolios Get returned {0} nodes",xdoc.ChildNodes.Count);
					XmlNodeList entries = xdoc.GetElementsByTagName("entry");
					if(entries.Count > 0)
					{
						for (int i = 0; i < entries.Count;i++)
						{
							Portfolio aPortfolio = new Portfolio(entries.Item(i).OuterXml);
							UserItems.Instance.Portfolios.Add(aPortfolio);
							
						}
						log.DebugFormat("Found {0} Portfolios",UserItems.Instance.Portfolios.Count);
					}
					else
					{
						//QUESTION Create Portfolio?
					}
					
					//TODO XXX
					//1. Get all Data in Portfolios
					//2. Organize Data
					//3. Cache Data
					//4. Load Portfolio window
					
					//Load Positions for each Portfolio
					List<XmlDocument> feedDataXMLs = new List<XmlDocument>();
					for(int i = 0;i< UserItems.Instance.Portfolios.Count; i++)
					{
						Portfolio p = UserItems.Instance.Portfolios[i];
						log.DebugFormat("DataGet for Positions on Portfolio {0}", p.ID);
						feedDataXMLs.Add(getUtility.DataGet(Google.FINANCE_BASE_URL + "/portfolios/" + p.ID + "/positions","returns=true&transactions=true" ));
					}
					log.DebugFormat("Got {0} Positions",feedDataXMLs.Count);
					
					
				}
				else
				{
					DisplayError("No Data Returned!");
					return;
				}
			}catch(Exception ex){
				log.Error(ex.Message);
				DisplayError(ex.Message);
				pbLoading.Visible = false;
				pbLoading.Fraction = 0;
				eEmail.IsEditable = true;
				ePassword.IsEditable = true;
			}
		}
		else{
			//fail
			MessageDialog md = new MessageDialog(this,DialogFlags.Modal,MessageType.Error,ButtonsType.Close, "Login Error, {0}",sErrorMsg);
			ResponseType retval = (ResponseType) md.Run();
			
			if(retval == ResponseType.Close)
			{
				md.Destroy();
			}
			eEmail.GrabFocus();
			pbLoading.Visible = false;
			pbLoading.Fraction = 0;
			ePassword.Text = string.Empty;
			ePassword.IsEditable = true;
			eEmail.IsEditable = true;
		}
		
	}
	
	[GLib.ConnectBefore]
	protected void ePassword_KeyPress(object sender, KeyPressEventArgs e)
	{
		if(e.Event.Key == Gdk.Key.Return )
		{
			BtnSubmit_Click(sender,new EventArgs());
		}
	}
	
	
	private void DisplayError(string sMsg)
	{
			log.Error("Display Error => " + sMsg);
			MessageDialog md = new MessageDialog(this,DialogFlags.Modal,MessageType.Error,ButtonsType.Close, "{0}",sMsg);
			ResponseType retval = (ResponseType) md.Run();
			
			if(retval == ResponseType.Close)
			{
				md.Destroy();
			}
	}
}

