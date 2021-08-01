using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_Studia {
    public partial class BrowseNotes : Form {
        public BrowseNotes(string[] notesPaths) {
            InitializeComponent();

            foreach (var notePath in notesPaths) {
                CreateNoteButton(notePath); 
            }
        }

        private void CreateNoteButton(string note) {
            Button button = new Button();
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.BackColor = System.Drawing.Color.Black;
            button.Font = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button.ForeColor = System.Drawing.Color.White;
            button.Text = note.Split('\\')[1];
            button.Height = 50;
            button.Width = 250;
            button.Click += Button_Click;
            flowLayoutPanel1.Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e) {
            Process.Start($"notes\\{((Button)sender).Text}");
        }

        private void button5_Click(object sender, EventArgs e) {
            this.Close();
        }

        int x;
        int y;
        bool firstClick = false;

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                firstClick = true;
                moveTimer.Enabled = true;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                moveTimer.Enabled = false;
            }
        }

        private void moveTimer_Tick(object sender, EventArgs e) {
            if (firstClick) {
                firstClick = false;
                x = Cursor.Position.X;
                y = Cursor.Position.Y;
            }
            int deltax = x - Cursor.Position.X;
            int deltay = y - Cursor.Position.Y;

            this.Location = new Point(this.Location.X - deltax, this.Location.Y - deltay);

            x = Cursor.Position.X;
            y = Cursor.Position.Y;
        }
    }
}
