using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_Studia {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            label3.Text = $"{DateTime.Now.DayOfWeek}";
            label2.Text = $"{DateTime.Now.Date}".Split(' ')[0];
            TimeTimer.Enabled = true;

        }

        private void TimeTimer_Tick(object sender, EventArgs e) {
            string hour = DateTime.Now.Hour > 9 ? $"{ DateTime.Now.Hour }" : $"0{ DateTime.Now.Hour }";
            string minute = DateTime.Now.Minute > 9 ? $"{ DateTime.Now.Minute }" : $"0{ DateTime.Now.Minute }";
            string second = DateTime.Now.Second > 9 ? $"{ DateTime.Now.Second }" : $"0{ DateTime.Now.Second }";
            label1.Text = $"{hour}:{minute}:{second}";
        }

        private void button5_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        int x;
        int y;
        private bool firstClick = false;
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
            new Schedule().Show();
        }

        private void button2_Click(object sender, EventArgs e) {
            new Notes().Show();
        }

        private void button4_Click(object sender, EventArgs e) {
            new ToDo().Show();
        }

        private void button3_Click(object sender, EventArgs e) {
            new Settings().Show();
        }
    }
}
