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
    public partial class temp : Form {
        EditSchedule schedule;
        List<Activity> activities;
        SQLiteConnection connection;
        public temp(EditSchedule schedule, List<Activity> activities) {
            InitializeComponent();
            this.schedule = schedule;
            connection = new SQLiteConnection(
                "URI = file:" + Directory.GetCurrentDirectory() + "\\data.db"
            );
            this.activities = activities;
        }

        private void SundayPanel_Paint(object sender, PaintEventArgs e) {
            
        }

        private void button1_Click(object sender, EventArgs e) {
            Activity activity = new Activity() {
                ID = new Random().Next(),
                Title = textBox1.Text,
                Start = dateTimePicker1.Value,
                End = dateTimePicker2.Value,
                //Lecturer = null,
                Day = (string)DayBox.SelectedItem,
            };
            
            activities.Add(activity);
            activity.Save();
            var color = colorDialog1.Color;
            var commandText = $"INSERT INTO schedule(id, title, start, end, day, red, green, blue)" +
                              $"VALUES (\'{activities.Count()}\', \'{activity.Title}\', \'{activity.Start}\', " +
                              $"\'{activity.End}\', \'{activity.Day}\', \'{color.R}\', \'{color.G}\', \'{color.B}\')";
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(commandText, connection);
            command.ExecuteNonQuery();
            connection.Close();

            Button activityButton = new Button();
            activityButton.FlatStyle = FlatStyle.Popup;
            activityButton.BackColor = colorDialog1.Color;
            activityButton.Text = textBox1.Text;
            activityButton.Width = 190;
            activityButton.Height = (dateTimePicker2.Value.Hour - dateTimePicker1.Value.Hour) * 50 + 
               (int)(dateTimePicker2.Value.Minute - dateTimePicker1.Value.Minute * 0.6);
            activityButton.Location = new Point(0, (int)((((int)dateTimePicker1.Value.TimeOfDay.TotalSeconds - (int)new TimeSpan(6, 0, 0).TotalSeconds) / 3600.0) * 50) + 50);

            Console.WriteLine(DayBox.SelectedItem);
            switch (DayBox.SelectedItem) {
                case "Monday": schedule.MondayPanel.Controls.Add(activityButton); break;
                case "Tuesday": schedule.TuesdayPanel.Controls.Add(activityButton); break;
                case "Wednesday": schedule.WednesdayPanel.Controls.Add(activityButton); break;
                case "Thursday": schedule.ThursdayPanel.Controls.Add(activityButton); break;
                case "Friday": schedule.FridayPanel.Controls.Add(activityButton); break;
                case "Saturday": schedule.SaturdayPanel.Controls.Add(activityButton); break;
                case "Sunday": schedule.SundayPanel.Controls.Add(activityButton); break;
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            colorDialog1.ShowDialog();
        }

        
    }
}
