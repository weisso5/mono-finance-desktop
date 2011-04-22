using System;
using Gtk;

namespace MyFinance
{
	class MainClass
	{
		private static void ConfigureLogging()
		{
			string logfile = "./log4net.config";
			if(System.IO.File.Exists(logfile))
			{
				log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(logfile));
			}
		}
		
		public static void Main (string[] args)
		{
			Application.Init ();
			ConfigureLogging();
			MainWindow win = new MainWindow ();
			win.SetPosition(WindowPosition.CenterAlways);
			win.Show ();
			win.GrabFocus();
			Application.Run ();
		}
	}
}

