using PROJECT_Studia.Forms.ToDo;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PROJECT_Studia {
    public partial class ToDo : Form {
        List<ToDoTask> tasks;
        int maxID;
        SQLiteConnection connection;
        Home home;

        public ToDo(Home home) {
            InitializeComponent();
            tasks = GetTasks();
            flowLayoutPanel1.AutoScroll = true;
            DrawButtons();
            this.home = home;
        }

        private List<ToDoTask> GetTasks() {
            List<ToDoTask> result = new List<ToDoTask>();

            connection = new SQLiteConnection(
                "URI = file:" + Directory.GetCurrentDirectory() + "\\data.db"
            );
            string commandText = "SELECT * FROM todo";
            
            connection.Open();
            var queue = new SQLiteCommand(commandText, connection).ExecuteReader();
           

            maxID = 0;
            while (queue.Read()) {
                if (queue.GetInt32(3) != 2) {
                    result.Add(
                        new ToDoTask(
                            queue.GetInt32(0), queue.GetString(1), DateTime.Parse(queue.GetString(2)), queue.GetInt32(3)
                       )
                    );

                }
                if (queue.GetInt32(0) > maxID) maxID = queue.GetInt32(0);
            }
            connection.Close();
            
            result.Sort((x, y) => { return x.time.CompareTo(y.time); });
            return result;
        }

        private void DrawButtons() {
            foreach (var task in tasks) {
                //Task visuals
                var button = new Button();
                button.Text = task.name;
                button.BackColor = task.done == 0 ? Color.Black : Color.Orange;
                button.FlatStyle = FlatStyle.Flat;
                button.Font = new Font(
                    "Century Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0
                );
                button.ForeColor = Color.White;
                button.Size = new Size(195, 50);
                button.UseVisualStyleBackColor = false;
                button.Tag = task;
                button.Click += Button_Click;
                flowLayoutPanel1.Controls.Add(button);
            }
        }

        private void Button_Click(object sender, EventArgs e) {
            ToDoTask task = (ToDoTask)((Button)sender).Tag;
            Console.WriteLine(task.ID);
            Console.WriteLine(task.time);
            Console.WriteLine(task.name);
        }

        private void button1_Click(object sender, EventArgs e) {
            new AddTask(connection, maxID + 1, home).ShowDialog();
        }
    }
}
