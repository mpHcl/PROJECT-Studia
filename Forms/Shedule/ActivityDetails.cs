using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_Studia.Forms.Shedule {
    public partial class ActivityDetails : Form {
        EditSchedule schedule;
        Activity activity;
        public ActivityDetails(Activity activity, EditSchedule editSchedule) {
            InitializeComponent();
            textBox1.Text = activity.Title;
            DayBox.Text = activity.Day;
            dateTimePicker1.Value = activity.Start;
            dateTimePicker2.Value = activity.End;
            schedule = editSchedule;
            this.activity = activity;
        }

        private void button1_Click(object sender, EventArgs e) {
            Activity.DeleteActivity(activity);
            schedule.clearSchedule();
            schedule.initializeSchedule();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            activity.Title = textBox1.Text;
            activity.Start = dateTimePicker1.Value;
            activity.End = dateTimePicker2.Value;
            activity.Day = (string)DayBox.SelectedItem;
            activity.Color = colorDialog1.Color;
            Activity.UpdateActivity(activity);
            schedule.clearSchedule();
            schedule.initializeSchedule();
        }

        private void DayBox_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void button3_Click(object sender, EventArgs e) {
            colorDialog1.ShowDialog();
        }
    }
}
