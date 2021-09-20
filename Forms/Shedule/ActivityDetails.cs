using System;
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
            button4.BackColor = activity.Color;
        }

        private void DeleteButtonClick(object sender, EventArgs e) {
            Activity.DeleteActivity(activity);
            schedule.clearSchedule();
            schedule.initializeSchedule();
            this.Close();
        }

        private void SaveButtonClick(object sender, EventArgs e) {
            activity.Title = textBox1.Text;
            activity.Start = dateTimePicker1.Value;
            activity.End = dateTimePicker2.Value;
            activity.Day = (string)DayBox.SelectedItem;
            activity.Color = colorDialog1.Color;
            Activity.UpdateActivity(activity);
            schedule.clearSchedule();
            schedule.initializeSchedule();
        }

        private void ColorPickClick(object sender, EventArgs e) {
            colorDialog1.ShowDialog();
        }
    }
}
