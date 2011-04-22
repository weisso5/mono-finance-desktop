
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.Fixed fixed1;

	private global::Gtk.Image imgLogo;

	private global::Gtk.Label lblIntroText;

	private global::Gtk.Image imgLogo2;

	private global::Gtk.Label lblEmail;

	private global::Gtk.Entry eEmail;

	private global::Gtk.Label lblPassword;

	private global::Gtk.Entry ePassword;

	private global::Gtk.Button btnSubmit;

	private global::Gtk.Label lblIntroText2;

	private global::Gtk.ProgressBar pbLoading;

	private global::Gtk.Image imgEmailValid;

	private global::Gtk.Image imgPasswordValid;

	private global::Gtk.ScrolledWindow tvScrolllWindow;

	private global::Gtk.TextView tvIntroText;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.fixed1 = new global::Gtk.Fixed ();
		this.fixed1.TooltipMarkup = "Logo";
		this.fixed1.Name = "fixed1";
		this.fixed1.HasWindow = false;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.imgLogo = new global::Gtk.Image ();
		this.imgLogo.TooltipMarkup = "Logo";
		this.imgLogo.Name = "imgLogo";
		this.imgLogo.Xalign = 0f;
		this.fixed1.Add (this.imgLogo);
		global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.imgLogo]));
		w1.X = 6;
		w1.Y = 7;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.lblIntroText = new global::Gtk.Label ();
		this.lblIntroText.Name = "lblIntroText";
		this.lblIntroText.LabelProp = global::Mono.Unix.Catalog.GetString ("lblIntroText");
		this.fixed1.Add (this.lblIntroText);
		global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.lblIntroText]));
		w2.X = 67;
		w2.Y = 94;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.imgLogo2 = new global::Gtk.Image ();
		this.imgLogo2.Name = "imgLogo2";
		this.fixed1.Add (this.imgLogo2);
		global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.imgLogo2]));
		w3.X = 70;
		w3.Y = 117;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.lblEmail = new global::Gtk.Label ();
		this.lblEmail.Name = "lblEmail";
		this.lblEmail.LabelProp = global::Mono.Unix.Catalog.GetString ("lblEmail");
		this.fixed1.Add (this.lblEmail);
		global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.lblEmail]));
		w4.X = 17;
		w4.Y = 192;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.eEmail = new global::Gtk.Entry ();
		this.eEmail.CanFocus = true;
		this.eEmail.Name = "eEmail";
		this.eEmail.IsEditable = true;
		this.eEmail.InvisibleChar = '●';
		this.fixed1.Add (this.eEmail);
		global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.eEmail]));
		w5.X = 75;
		w5.Y = 190;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.lblPassword = new global::Gtk.Label ();
		this.lblPassword.Name = "lblPassword";
		this.lblPassword.LabelProp = global::Mono.Unix.Catalog.GetString ("lblPassword");
		this.fixed1.Add (this.lblPassword);
		global::Gtk.Fixed.FixedChild w6 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.lblPassword]));
		w6.X = 11;
		w6.Y = 233;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.ePassword = new global::Gtk.Entry ();
		this.ePassword.CanFocus = true;
		this.ePassword.Name = "ePassword";
		this.ePassword.IsEditable = true;
		this.ePassword.InvisibleChar = '●';
		this.fixed1.Add (this.ePassword);
		global::Gtk.Fixed.FixedChild w7 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.ePassword]));
		w7.X = 86;
		w7.Y = 229;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.btnSubmit = new global::Gtk.Button ();
		this.btnSubmit.CanFocus = true;
		this.btnSubmit.Name = "btnSubmit";
		this.btnSubmit.UseUnderline = true;
		this.btnSubmit.Label = global::Mono.Unix.Catalog.GetString ("btnSubmit");
		this.fixed1.Add (this.btnSubmit);
		global::Gtk.Fixed.FixedChild w8 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.btnSubmit]));
		w8.X = 90;
		w8.Y = 260;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.lblIntroText2 = new global::Gtk.Label ();
		this.lblIntroText2.Name = "lblIntroText2";
		this.lblIntroText2.LabelProp = global::Mono.Unix.Catalog.GetString ("lblIntroText2");
		this.fixed1.Add (this.lblIntroText2);
		global::Gtk.Fixed.FixedChild w9 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.lblIntroText2]));
		w9.X = 131;
		w9.Y = 121;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.pbLoading = new global::Gtk.ProgressBar ();
		this.pbLoading.Name = "pbLoading";
		this.fixed1.Add (this.pbLoading);
		global::Gtk.Fixed.FixedChild w10 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.pbLoading]));
		w10.X = 344;
		w10.Y = 209;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.imgEmailValid = new global::Gtk.Image ();
		this.imgEmailValid.Name = "imgEmailValid";
		this.fixed1.Add (this.imgEmailValid);
		global::Gtk.Fixed.FixedChild w11 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.imgEmailValid]));
		w11.X = 237;
		w11.Y = 193;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.imgPasswordValid = new global::Gtk.Image ();
		this.imgPasswordValid.Name = "imgPasswordValid";
		this.fixed1.Add (this.imgPasswordValid);
		global::Gtk.Fixed.FixedChild w12 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.imgPasswordValid]));
		w12.X = 246;
		w12.Y = 234;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.tvScrolllWindow = new global::Gtk.ScrolledWindow ();
		this.tvScrolllWindow.Name = "tvScrolllWindow";
		this.tvScrolllWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child tvScrolllWindow.Gtk.Container+ContainerChild
		this.tvIntroText = new global::Gtk.TextView ();
		this.tvIntroText.Buffer.Text = "Welcome to MyFinance!\n\nSome Text Goes Here!!";
		this.tvIntroText.CanFocus = true;
		this.tvIntroText.Name = "tvIntroText";
		this.tvScrolllWindow.Add (this.tvIntroText);
		this.fixed1.Add (this.tvScrolllWindow);
		global::Gtk.Fixed.FixedChild w14 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.tvScrolllWindow]));
		w14.X = 524;
		w14.Y = 108;
		this.Add (this.fixed1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 779;
		this.DefaultHeight = 457;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.btnSubmit.Clicked += new global::System.EventHandler (this.BtnSubmit_Click);
	}
}
