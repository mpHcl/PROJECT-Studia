using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            label1.Text = task.Title;
            label2.Text = task.Time.ToString();
            label3.Text = task.Description;
            
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
    }
}
