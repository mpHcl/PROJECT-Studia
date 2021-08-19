using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace PROJECT_Studia.Forms.ToDo {
    public partial class AddTask : Form {

        SQLiteConnection connection;
        int ID;

        Home home;
        public AddTask(SQLiteConnection connection, int ID, Home home) {
            InitializeComponent();
            this.connection = connection;
            this.ID = ID;
            this.home = home;
            Console.WriteLine(ID);
        }

        private void button5_Click(object sender, EventArgs e) {
            this.Close();
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

        private void button7_Click(object sender, EventArgs e) {

            var querryText = $"INSERT INTO todo (ID, name, description, time, done) " +
                             $"VALUES ({ID}, \"{textBox1.Text}\",\"{richTextBox1.Text}\", \"{dateTimePicker1.Value}\", 0)";
            connection.Open();
            new SQLiteCommand(querryText, connection).ExecuteNonQuery();
            connection.Close();
            this.Close();
            var todo = new PROJECT_Studia.ToDo(home);
            todo.TopLevel = false;
            todo.Visible = true;
            home.Panel.Controls.Clear();
            home.Panel.Controls.Add(todo);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {

        }
    }
}
