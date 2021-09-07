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

namespace PROJECT_Studia.Forms.Shedule {
    public partial class AddActivity : Form {
        EditSchedule schedule;
        List<Activity> activities;
        SQLiteConnection connection;
        public AddActivity(EditSchedule schedule, List<Activity> activities) {
            InitializeComponent();
            this.schedule = schedule;
            connection = new SQLiteConnection(
                "URI = file:" + Directory.GetCurrentDirectory() + "\\data.db"
            );
            this.activities = activities;
        }

        private void button1_Click(object sender, EventArgs e) {
            int max = 0;
            foreach (var item in activities) {
                if (item.ID > max) {
                    max = item.ID;
                }
            }
            Activity activity = new Activity() {
                ID = max + 1,
                Title = textBox1.Text,
                Start = dateTimePicker1.Value,
                End = dateTimePicker2.Value,
                Day = (string)DayBox.SelectedItem,
            };
            
            activities.Add(activity);
            activity.Save();
            var color = colorDialog1.Color;

            var commandText = $"INSERT INTO schedule(id, title, start, end, day, red, green, blue)" +
                              $"VALUES (\'{max + 1}\', \'{activity.Title}\', \'{activity.Start}\', " +
                              $"\'{activity.End}\', \'{activity.Day}\', \'{color.R}\', \'{color.G}\', \'{color.B}\')";
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(commandText, connection);
            command.ExecuteNonQuery();
            connection.Close();

            schedule.clearSchedule();
            schedule.initializeSchedule();
        }

        private void button2_Click(object sender, EventArgs e) {
            colorDialog1.ShowDialog();
        }

        
    }
}
