using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NinjaSoftwareLtd.ErrorLogging;

namespace JobHunt
{
	static class Program
	{
        private static NLogErrorLogger errorLogger = new NLogErrorLogger();
        
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            try
            {
                var kernel = new StandardKernel(new DependencyInjectionModule());
                var form = kernel.Get<JobHunt>();

                Application.Run(form);
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex.Message, ex);
                MessageBox.Show("An error has occurred in this application.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
            errorLogger.LogError(e.Exception.Message, e.Exception);
			MessageBox.Show("An error has occurred in this application.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}
}
