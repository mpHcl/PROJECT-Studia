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
            this.home = home;
            tasks = GetTasks();
            flowLayoutPanel1.AutoScroll = true;
            DrawButtons();
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
                if (queue.GetInt32(4) != 2) {
                    int done;
                    DateTime time = DateTime.Parse(queue.GetString(3));
                    if (time < DateTime.Now) 
                        done = 1;
                    else
                        done = 0;

                    result.Add(
                        new ToDoTask(
                            queue.GetInt32(0), queue.GetString(1), queue.GetString(2), time, done
                       )
                    );

                }
                if (queue.GetInt32(0) > maxID) maxID = queue.GetInt32(0);
            }
            connection.Close();
            
            result.Sort((x, y) => { return x.Time.CompareTo(y.Time); });
            return result;
        }

        private void DrawButtons() {
            foreach (var task in tasks) {
                //Task visuals
                var button = new Button();
                button.Text = task.Title;
                button.BackColor = task.Done == 0 ? Color.Black : Color.Orange;
                button.FlatStyle = FlatStyle.Flat;
                button.Font = new Font(
                    "Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0
                );
                button.ForeColor = Color.White;
                button.Size = new Size(200, 33);
                button.UseVisualStyleBackColor = false;
                button.Tag = task;
                button.Click += Button_Click;
                flowLayoutPanel1.Controls.Add(button);
            }
        }

        private void Button_Click(object sender, EventArgs e) {
            ToDoTask task = (ToDoTask)((Button)sender).Tag;
            new TaskForm(task, connection, home).ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) {
            new AddTask(connection, maxID + 1, home).ShowDialog();
        }
    }
}
