// The under-the-hood methods.
// Save, Open, wantToSave, SaveLocation

using System;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace AddroText
{
	partial class MainForm
	{
		static void OpenFile(TextBox textBody)
		{	
			// Filter the file dialog for .txt files and disable multiselect
			OpenFileDialog openNew = new OpenFileDialog();
			openNew.Filter = "Text files (*.txt)|*.txt" ;
		    openNew.FilterIndex = 1;
		    openNew.RestoreDirectory = true ;
		    openNew.Multiselect = false;
		    
		    if(openNew.ShowDialog() == DialogResult.OK)
		    {
		    	// Open a file. If there was text in the textbox, clear it.
			    StreamReader reader = new StreamReader(openNew.FileName);
			    openedFileName = openNew.FileName;
			    string fileName = Path.GetFileNameWithoutExtension(openNew.FileName);
				ClearScreen(textBody);
				
				ActiveForm.Text = "AddroText - " + fileName;
				
				// Read all lines and display it line by line.
				// Encoding is necessary so app can handle ANSI.
				string[] lines = File.ReadAllLines(openedFileName, Encoding.Default);
			    for(int i = 0; i < lines.Length; ++i)
			    {
			    	textBody.Text += lines[i];
			    	if (i != lines.Length - 1)
			    	{
						// Add a newline in a TextBox after each line in file
						// Except last line.
			    	    textBody.Text += "\r\n";
			    	}
			    }
			    
		    }
		}
		
		private static void ClearScreen(TextBox textBody)
			{
			// Clear screen, and if text is not saved, ask user to save it.
			if ((textBody.Text != null) && (isModified == true))
				{
				wantToSave(textBody);
				}
			textBody.Clear();
			}
			
		static void SaveFile(TextBox textBody)
		{
			// Check to see if there is a file opened. if not, go to save as.
			if (openedFileName != "")
			{
				StreamWriter writer = new StreamWriter(openedFileName);
				writer.WriteLine(textBody.Text);
				writer.Flush();
				writer.Close();
			}
			else 
			{
				SaveFileAs(textBody);
			}
		}
		
		static void SaveFileAs(TextBox textBody)
		{
			// Filter save dialog for .txt files.
			SaveFileDialog saveOther = new SaveFileDialog();
			saveOther.Filter = "Text Files (*.txt)|*.txt";
			saveOther.DefaultExt = "txt";
			saveOther.AddExtension = true;
			
			if(saveOther.ShowDialog() == DialogResult.OK)
		    {
				StreamWriter writer = new StreamWriter(saveOther.OpenFile(), Encoding.Unicode);
				openedFileName = saveOther.FileName;
				writer.WriteLine(textBody.Text);
				writer.Flush();
				writer.Close();
				string fileName = Path.GetFileNameWithoutExtension(saveOther.FileName);
				ActiveForm.Text = "AddroText - " + fileName;
			}

			isModified = false;
			}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (isModified)
			{
				if(wantToSave(MainBody) == 1)
					{
					e.Cancel = true;
					}
			}

			SaveLocation(MainBody);
		}

		static void SaveLocation(TextBox textBody)
			{
			// Save location, size and font, unless window was minimized when closed.
			// If it WAS minimized, start default:
			// Size: 400x600; Location: 150, 150.
			if (ActiveForm.WindowState == FormWindowState.Minimized)
				{
				Settings1.Default.Location = new System.Drawing.Point(150, 150);
				Settings1.Default.Size = new System.Drawing.Size(400, 600);
				}
			else
				{
				Settings1.Default.Location = ActiveForm.Location;
				Settings1.Default.Size = ActiveForm.Size;
				}
			Settings1.Default.Font = textBody.Font;
			Settings1.Default.Save();
			}

		static void changeModified()
			{
			isModified = true;
			}

		static int wantToSave(TextBox textBody)
			{

			// Prompt to save when quitting or opening with existing text.
			string message = "Changes have been made, do you want to save?";
			string caption = "Are you sure?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
			DialogResult result;

			result = MessageBox.Show(message, caption, buttons);
			if (result == System.Windows.Forms.DialogResult.Yes)
				{
				// isModified is false because it was saved.
				SaveFileAs(textBody);
				isModified = false;
				return 0;
				}
			else if (result == System.Windows.Forms.DialogResult.Cancel)
				{
				return 1;
				}
			else
				{
				// isModified is false because user doesn't want to save.
				isModified = false;
				return 2;
				}
			}
	}
}
