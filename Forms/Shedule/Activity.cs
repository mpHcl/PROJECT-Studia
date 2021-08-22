﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_Studia.Forms.Shedule {
    public class Activity {       
        public int ID { get; set; }
        public string Title { get; set; }
        //public Person Lecturer { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Color Color { get; set; }
        public string Day { get; set; }

        /*
        public class Person {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

        }
        */
        public static List<Activity> GetScheduleAsList() {
            List<Activity> result = new List<Activity>();

            SQLiteConnection connection = new SQLiteConnection(
                "URI = file:" + Directory.GetCurrentDirectory() + "\\data.db"
            );
            string command = "SELECT * FROM schedule;";
            connection.Open();
            SQLiteCommand query = new SQLiteCommand(command, connection);
            var reader = query.ExecuteReader();

            while (reader.Read()) {
                result.Add(
                    new Activity() {
                        ID = 0,
                        Color = Color.FromArgb(100, reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9)),
                        Title = reader.GetString(1),
                        Start = DateTime.Parse(reader.GetString(2)),
                        End = DateTime.Parse(reader.GetString(3)),
                        Day = reader.GetString(6)
                    }
                );
            }
            connection.Close();
            return result;
        }

        public void Save() {

        }
    }
}
