using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_Studia {
    public partial class Notes : Form {
        public Notes() {
            InitializeComponent();
            if (!Directory.Exists("notes")) {
                Directory.CreateDirectory("notes");
            }
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
                MoveTimer.Enabled = true;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                MoveTimer.Enabled = false;
            }
        }

        private void MoveTimer_Tick(object sender, EventArgs e) {
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

        private void button1_Click(object sender, EventArgs e) {
            new CreateNoteMenu().Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {
            string[] path = openFileDialog1.FileName.Split('\\');
            File.Copy(openFileDialog1.FileName, 
                "notes\\" + path[path.Length - 1]);
            Process.Start(path[path.Length - 1]);
        }

        private void button2_Click(object sender, EventArgs e) {
            new BrowseNotes(Directory.GetFiles("notes\\")).ShowDialog();

        }
    }
}
