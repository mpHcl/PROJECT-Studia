using PROJECT_Studia.Forms.Shedule;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PROJECT_Studia {
    public partial class Home : Form {
        public FlowLayoutPanel Panel { get; set; }
        public Home() {
            InitializeComponent();

            flowLayoutPanel2.Visible = false;
            button7.Visible = false;
            this.Panel = flowLayoutPanel2;


            //Use this without installed app
            /*
            string path = Directory.GetCurrentDirectory() + "\\data.db";
            if (!File.Exists(path)) {
                var file = File.Create(path);
                file.Close();
                CreateTables(path);
            }
            */

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
                                        "description TEXT, " +
                                        "time time," +
                                        "done INT" +
                                        ")";
            SQLiteCommand command = new SQLiteCommand(createToDoTableSQL, connection);
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
                                        "title TEXT," +
                                        "day TEXT, " +
                                        "start time," +
                                        "end time," +
                                        "room INT," +
                                        "person_id INT," +
                                        "red INT," +
                                        "green INT," +
                                        "blue INT " +
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

        private void ExitButtonClick(object sender, EventArgs e) {
            Close();
        }

        private void MinimizeButtonClick(object sender, EventArgs e) {
            WindowState = FormWindowState.Minimized;
        }

        private void ScheduleButtonClick(object sender, EventArgs e) {
            new EditSchedule().ShowDialog();

        }

        private void NotesButtonClick(object sender, EventArgs e) {
            var noteFrom = new Notes(this);
            noteFrom.TopLevel = false;
            noteFrom.Visible = true;
            flowLayoutPanel1.Visible = false;

            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel2.Controls.Add(noteFrom);
            flowLayoutPanel2.Visible = true;

            button7.Visible = true;


        }

        private void ToDoButtonClick(object sender, EventArgs e) {
            var todoFrom = new ToDo(this);
            todoFrom.TopLevel = false;
            todoFrom.Visible = true;
            flowLayoutPanel1.Visible = false;

            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel2.Controls.Add(todoFrom);
            flowLayoutPanel2.Visible = true;

            button7.Visible = true;
        }

        private void HomeButtonClick(object sender, EventArgs e) {
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel1.Visible = true;
            button7.Visible = false;
        }

        private void ExitButtonMouseHover(object sender, EventArgs e) {
            button5.BackColor = Color.Gray;
        }

        private void ExitButtonMouseLeave(object sender, EventArgs e) {
            button5.BackColor = Color.Black;
        }

        private void MinimizeButtonMouseHover(object sender, EventArgs e) {
            button6.BackColor = Color.Gray;
        }

        private void MinimizeButtonMouseLeave(object sender, EventArgs e) {
            button6.BackColor = Color.Black;
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
    }
}
