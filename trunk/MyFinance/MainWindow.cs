using System;
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
//				id = id.Substring(id.IndexOf("feed/"),id.IndexOf("/portfolio"));
				//XXXX HERE
				
				pbLoading.Pulse();
				if(xdoc != null && xdoc.HasChildNodes)
				{
					log.DebugFormat("Portfolios Get returned {0} nodes",xdoc.ChildNodes.Count);
					
					//TODO XXX
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

