using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace PROJECT_Studia {
    public partial class BrowseNotes : Form {
        public BrowseNotes(string[] notesPaths) {
            InitializeComponent();

            foreach (var notePath in notesPaths) {
                CreateNoteButton(notePath); 
            }
            flowLayoutPanel1.AutoScroll = true;
        }

        private void CreateNoteButton(string note) {
            Button button = new Button();
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = Color.Black;
            button.Font = new Font("Century Gothic", 15F, FontStyle.Regular, GraphicsUnit.Point);
            button.ForeColor = Color.White;
            button.Text = note.Split('\\')[1];
            button.Height = 50;
            button.Width = 220;
            button.Click += Button_Click;
            flowLayoutPanel1.Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e) {
            Process.Start($"notes\\{((Button)sender).Text}");
        }
    }
}
