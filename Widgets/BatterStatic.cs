using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DS.Showdown.Widgets
{
	public partial class BatterStatic : Label
	{
		public BatterStatic()
		{
			InitializeComponent();
		}

		void LeftButtonDblClk(UINT nFlags, CPoint point) 
		{
			CShowBatterDlg	dlg;

			dlg.m_pBatter = m_pSlot->m_pBatter;

			dlg.DoModal();
		}

		void DisplayName()
		{
			CString	strDisplay;

			strDisplay.Format("%s: %s", theApp.PositionText(m_pSlot->m_nPosition),
				m_pSlot->m_pBatter->GetName());

			this->SetWindowText(strDisplay);
		}
	}
}