using Ninject;
using System;
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

            var kernel = new StandardKernel(new DependencyInjectionModule());
            var form = kernel.Get<JobHunt>();
            
            Application.Run(form);
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			MessageBox.Show("An error has occurred in this application.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

	}
}
