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
    public partial class EditSchedule : Form {
        List<Activity> activities;
        public EditSchedule() {
            InitializeComponent();
            initializeSchedule();
            for (int i = 6; i <= 20; i += 2) {
                AddHour(i);
            }
        }

        public void initializeSchedule() {
            
            activities = Activity.GetScheduleAsList();
            foreach (var activity in activities) {
                Button activityButton = new Button();
                activityButton.FlatStyle = FlatStyle.Popup;
                activityButton.BackColor = activity.Color;
                activityButton.Text = activity.Title;
                activityButton.Width = 110;
                activityButton.Height = (activity.End.Hour - activity.Start.Hour) * 25 +
                   (int)(activity.End.Minute - activity.Start.Minute * 0.6);
                activityButton.Location = new Point(0, (int)((((int)activity.Start.TimeOfDay.TotalSeconds - (int)new TimeSpan(6, 0, 0).TotalSeconds) / 3600.0) * 25) + 50);
                activityButton.Click += openActivityButton;
                activityButton.Tag = activity;

                switch (activity.Day) {
                    case "Monday": MondayPanel.Controls.Add(activityButton); break;
                    case "Tuesday": TuesdayPanel.Controls.Add(activityButton); break;
                    case "Wednesday": WednesdayPanel.Controls.Add(activityButton); break;
                    case "Thursday": ThursdayPanel.Controls.Add(activityButton); break;
                    case "Friday": FridayPanel.Controls.Add(activityButton); break;
                    case "Saturday": SaturdayPanel.Controls.Add(activityButton); break;
                    case "Sunday": SundayPanel.Controls.Add(activityButton); break;
                }
            }
        }

        public void clearSchedule() {
            MondayPanel.Controls.Clear();
            TuesdayPanel.Controls.Clear();
            WednesdayPanel.Controls.Clear();
            ThursdayPanel.Controls.Clear();
            FridayPanel.Controls.Clear();
            SaturdayPanel.Controls.Clear();
            SundayPanel.Controls.Clear();

            MondayPanel.Controls.Add(label3);
            TuesdayPanel.Controls.Add(label2);
            WednesdayPanel.Controls.Add(label1);
            ThursdayPanel.Controls.Add(label4);
            FridayPanel.Controls.Add(label7);
            SaturdayPanel.Controls.Add(label6);
            SundayPanel.Controls.Add(label5);
        }

        private void openActivityButton(object sender, EventArgs e) {
            new ActivityDetails((Activity)((Button)sender).Tag, this).ShowDialog();
        }

        private void EditSchedule_Load(object sender, EventArgs e) {

        }

        private void button7_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
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

        private void button1_Click(object sender, EventArgs e) {
            new AddActivity(this, activities).Show();
        }
        private void AddHour(int hour) {
            Label hourLabel = new Label();
            hourLabel.ForeColor = Color.White;
            hourLabel.AutoSize = false;
            hourLabel.Width = 40;
            hourLabel.TextAlign = ContentAlignment.MiddleCenter;
            hourLabel.Text = $"{hour}:00";
            hourLabel.Location = new Point(0, (hour - 4) * 25);
            

            panel2.Controls.Add(hourLabel);
        }
    }
}
