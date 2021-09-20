using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace PROJECT_Studia.Forms.ToDo {
    public partial class TaskForm : Form {
        ToDoTask task;
        SQLiteConnection connection;
        PROJECT_Studia.Home home;
        public TaskForm(ToDoTask task, SQLiteConnection connection, PROJECT_Studia.Home home) {
            InitializeComponent();
            this.task = task;
            this.connection = connection;
            this.home = home;
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void TaskForm_Load(object sender, EventArgs e) {
            textBox1.Text = task.Title;
            label2.Text = task.Time.ToString();
            richTextBox1.Text = task.Description;
            
        }

        private void button5_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e) {
            connection.Open();
            var commandText = $"UPDATE todo SET done = 2 WHERE id = {task.ID};";
            new SQLiteCommand(commandText, connection).ExecuteNonQuery();
            connection.Close();
            this.Close();
            var todo = new PROJECT_Studia.ToDo(home);
            todo.TopLevel = false;
            todo.Visible = true;
            home.Panel.Controls.Clear();
            home.Panel.Controls.Add(todo);
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

        private void label2_Click(object sender, EventArgs e) {

        }
    }
}
