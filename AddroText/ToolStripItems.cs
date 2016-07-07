// Events for the tool strip


using System;
using System.Windows.Forms;

namespace AddroText
{
	partial class MainForm
	{
		void OpenToolStripMenuItemClick(object sender, EventArgs e)
		{
			OpenFile(MainBody);
		}
		
		void NewToolStripMenuItemClick(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Application.ExecutablePath);
		}
		
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (isModified)
				{
				if (wantToSave(MainBody) != 1)
					{
					Application.Exit();
					}
				}
			else
				{
				Application.Exit();
				}
			}
		
		void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
		{
			SaveFileAs(MainBody);
		}
		
		void SaveToolStripMenuItemClick(object sender, EventArgs e)
		{
			SaveFile(MainBody);
		}
		
		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			string about = "(c) Created by addroddyn, 2016. \r\n tamas.s.istvan@gmail.com";
			about += "\n Icons from www.iconfinder.com";
			MessageBox.Show(about);
		}
		
		void FontToolStripMenuItemClick(object sender, EventArgs e)
		{
			FontDialog chooseFont = new FontDialog();
			chooseFont.ShowDialog();
			
			MainBody.Font = chooseFont.Font;
		}
	}
}
