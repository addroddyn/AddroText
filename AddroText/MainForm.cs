// This is Main Form and methods that set it up / directly interact with it.

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Media;


namespace AddroText
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		// Changes if any changes are made to the MainBody textbox.
		static bool isModified = false;
		
		// Needed for save and open, mainly.
		static string openedFileName = "";
		
		public MainForm()
		{
			InitializeComponent();
			
			// Some basic characteristics and an event handler for changes.
			MainBody.Font = new Font("Times New Roman", 12);
			MainBody.BackColor = Color.White;
			MainBody.ModifiedChanged += Changed;
		}
		
		void Changed(object sender, EventArgs e)
		{
			// If MainBody is changed
			changeModified();
		}

		private void MainForm_Load(object sender, EventArgs e)
			{
			// Unless this is the first time the user starts the program, look up
			// the setting for size, location and font.
			if (Settings1.Default.Font != null)
				{
				this.Size = Settings1.Default.Size;
				this.Location = Settings1.Default.Location;
				MainBody.Font = Settings1.Default.Font;
				}
			}

		private void barkToolStripMenuItem_Click(object sender, EventArgs e)
			{
			SoundPlayer bark = new SoundPlayer(Resource1.puppy_bark);
			bark.Play();
			}
		}
}
