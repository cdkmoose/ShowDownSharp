using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using DS.Showdown.ObjectLibrary;

namespace DS.Showdown.Widgets
{
	class PlayerLabel : Label
	{
		protected override void OnDoubleClick(EventArgs e)
		{
			if (Tag is Batter)
			{
				// show batter form
			}
			else if (Tag is Pitcher)
			{
				// show pitcher form
			}
			base.OnDoubleClick(e);
		}
	}
}
