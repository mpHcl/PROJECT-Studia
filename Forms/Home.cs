using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_Studia {
    public partial class Home : Form {
        public FlowLayoutPanel Panel { get; set; }
        public Home() {
            InitializeComponent();
            flowLayoutPanel2.Visible = false;
            button7.Visible = false;
            this.Panel = flowLayoutPanel2;

            string path = Directory.GetCurrentDirectory() + "\\data.db";
            if (!File.Exists(path)) {
                var file = File.Create(path);
                file.Close();
                CreateTables(path);
            }
            

        }

        private void CreateTables(string path) {
            SQLiteConnection connection = new SQLiteConnection("URI=file:" + path);
            
            connection.Open();

            //ToDo
            //ID - unique number (int)
            //Name - text describing task
            //Time - date planned for finishing task
            //Done - field containig data about task status (0 - unfinished, 1 - past time, 2 - finished)  
            var createToDoTableSQL = "CREATE TABLE todo (" +
                                        "ID INT UNIQUE PRIMARY KEY," +
                                        "name TEXT," +
                                        "time time," +
                                        "done INT" +
                                        ")";
            SQLiteCommand command = new SQLiteCommand(createToDoTableSQL, connection);
            command.ExecuteNonQuery();

            //Lecturer
            //ID - unique number(int)
            //fname - First name
            //lname - Last name
            var createLecturerTable = "CREATE TABLE lecturer(" +
                                        "ID INT UNIQUE PRIMARY KEY," +
                                        "fname TEXT, " +
                                        "lname TEXT" +
                                      ")";
            command = new SQLiteCommand(createLecturerTable, connection);
            command.ExecuteNonQuery();

            //Schedule
            //ID - unique number (int)
            //title - name of class
            //start - time of start of class 
            //end - time of end of class
            //room - number (int)
            //person_id - id of lecturer
            var createScheduleTable = "CREATE TABLE schedule(" +
                                        "ID INT UNIQUE PRIMARY KEY," +
                                        "title TEXT, " +
                                        "start time," +
                                        "end time," +
                                        "room INT," +
                                        "person_id INT," +
                                        "FOREIGN KEY(person_id) REFERENCES lecturer(ID) " +
                                      ")";
            command = new SQLiteCommand(createScheduleTable, connection);
            command.ExecuteNonQuery();

            
            connection.Close();
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
            var scheduleFrom = new Schedule();
            scheduleFrom.TopLevel = false;
            scheduleFrom.Visible = true;
            flowLayoutPanel1.Visible = false;

            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel2.Controls.Add(scheduleFrom);
            flowLayoutPanel2.Visible = true;

            button7.Visible = true;

        }

        private void button2_Click(object sender, EventArgs e) {
            var noteFrom = new Notes(this);
            noteFrom.TopLevel = false;
            noteFrom.Visible = true;
            flowLayoutPanel1.Visible = false;

            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel2.Controls.Add(noteFrom);
            flowLayoutPanel2.Visible = true;

            button7.Visible = true;


        }

        private void button4_Click(object sender, EventArgs e) {
            var todoFrom = new ToDo(this);
            todoFrom.TopLevel = false;
            todoFrom.Visible = true;
            flowLayoutPanel1.Visible = false;

            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel2.Controls.Add(todoFrom);
            flowLayoutPanel2.Visible = true;

            button7.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e) {
            var settingsFrom = new Settings();
            settingsFrom.TopLevel = false;
            settingsFrom.Visible = true;
            flowLayoutPanel1.Visible = false;

            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel2.Controls.Add(settingsFrom);
            flowLayoutPanel2.Visible = true;

            button7.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e) {
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel1.Visible = true;
            button7.Visible = false;
        }

        private void button5_MouseHover(object sender, EventArgs e) {
            button5.BackColor = Color.Gray;
        }

        private void button5_MouseLeave(object sender, EventArgs e) {
            button5.BackColor = Color.Black;
        }

        private void button6_MouseHover(object sender, EventArgs e) {
            button6.BackColor = Color.Gray;
        }

        private void button6_MouseLeave(object sender, EventArgs e) {
            button6.BackColor = Color.Black;
        }
    }
}
