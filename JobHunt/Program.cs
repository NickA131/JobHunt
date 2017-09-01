﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JobHunt
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ThreadException +=new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			Application.Run(new JobHunt());
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			MessageBox.Show("An error has occured in this application.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

	}
}