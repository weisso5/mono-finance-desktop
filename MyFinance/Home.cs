using System;
namespace MyFinance
{
	public partial class Home : Gtk.Window
	{
		public Home () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

