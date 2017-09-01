using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace JobHunt
{
	public class FormInitCtrl : Form
	{
		// Null argument predicate for initial initialisation.
		public void RefreshControls()
		{
			RefreshControls(null, new EventArgs());
		}
				
		public virtual void RefreshControls(object sender, EventArgs e) { }

	}
}
