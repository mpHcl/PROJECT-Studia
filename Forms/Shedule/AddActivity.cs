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
        public AddActivity(EditSchedule schedule, List<Activity> activities) {
            InitializeComponent();
            this.schedule = schedule;
            this.activities = activities;
        }

        private void AddButtonClick(object sender, EventArgs e) {
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
            
            activity.ID = max + 1;
            activity.Color = colorDialog1.Color;

            Activity.CreateActivity(activity);

            schedule.clearSchedule();
            schedule.initializeSchedule();
        }

        private void ColorPickerButtonClick(object sender, EventArgs e) {
            colorDialog1.ShowDialog();
        }

        
    }
}
