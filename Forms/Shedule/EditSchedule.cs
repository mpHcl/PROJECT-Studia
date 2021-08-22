﻿using System;
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
            for (int i = 6; i <= 20; i += 2) {
                AddHour(i);
                Console.WriteLine("-");
            }
            activities = Activity.GetScheduleAsList();
            foreach (var activity in activities) {
                Button activityButton = new Button();
                activityButton.FlatStyle = FlatStyle.Popup;
                activityButton.BackColor = activity.Color;
                activityButton.Text = activity.Title;
                activityButton.Width = 190;
                activityButton.Height = (activity.End.Hour - activity.Start.Hour) * 50 +
                   (int)(activity.End.Minute - activity.Start.Minute * 0.6);
                activityButton.Location = new Point(0, (int)((((int)activity.Start.TimeOfDay.TotalSeconds - (int)new TimeSpan(6, 0, 0).TotalSeconds) / 3600.0) * 50) + 50);

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

        private void label8_Click(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            new temp(this, activities).Show();
        }
        private void AddHour(int hour) {
            Label hourLabel = new Label();
            hourLabel.ForeColor = Color.White;
            hourLabel.AutoSize = false;
            hourLabel.Width = 40;
            hourLabel.TextAlign = ContentAlignment.MiddleCenter;
            hourLabel.Text = $"{hour}:00";
            hourLabel.Location = new Point(0, (hour - 5) * 50);
            

            panel2.Controls.Add(hourLabel);
        }
    }
}