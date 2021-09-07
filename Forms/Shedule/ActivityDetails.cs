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
            label1.Text = activity.ID.ToString();
            label2.Text = activity.Day;
            label3.Text = activity.Start.TimeOfDay.ToString();
            label4.Text = activity.End.TimeOfDay.ToString();
            schedule = editSchedule;
            this.activity = activity;
        }

        private void button1_Click(object sender, EventArgs e) {
            Activity.DeleteActivity(activity);
            schedule.clearSchedule();
            schedule.initializeSchedule();
            this.Close();
        }
    }
}
