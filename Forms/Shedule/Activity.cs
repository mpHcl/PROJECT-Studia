using System;
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
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Color Color { get; set; }
        public string Day { get; set; }

        public static List<Activity> GetActivities() {
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
                        ID = reader.GetInt32(0),
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

        public static void CreateActivity(Activity activity) {
            SQLiteConnection connection = new SQLiteConnection(
                "URI = file:" + Directory.GetCurrentDirectory() + "\\data.db"
            );
            var command = $"INSERT INTO schedule(id, title, start, end, day, red, green, blue)" +
                              $"VALUES (\'{activity.ID}\', \'{activity.Title}\', \'{activity.Start}\', " +
                              $"\'{activity.End}\', \'{activity.Day}\', \'{activity.Color.R}\', \'{activity.Color.G}\', \'{activity.Color.B}\')";
            connection.Open();
            new SQLiteCommand(command, connection).ExecuteNonQuery();
            connection.Close();
        }

        public static void UpdateActivity(Activity activity) {
            SQLiteConnection connection = new SQLiteConnection(
                "URI = file:" + Directory.GetCurrentDirectory() + "\\data.db"
            );
            string command = $"UPDATE schedule " +
                $"SET title=\"{activity.Title}\", start=\"{activity.Start}\", end=\"{activity.End}\", day=\"{activity.Day}\", " +
                $"red=\"{activity.Color.R}\", blue=\"{activity.Color.B}\", green=\"{activity.Color.G}\" " +
                $"WHERE ID = {activity.ID}";
            connection.Open();
            new SQLiteCommand(command, connection).ExecuteNonQuery();
            connection.Close();
        }

        public static void DeleteActivity(Activity activity) {
            SQLiteConnection connection = new SQLiteConnection(
                "URI = file:" + Directory.GetCurrentDirectory() + "\\data.db"
            );
            string command = $"DELETE FROM schedule WHERE ID = {activity.ID}";
            connection.Open();
            new SQLiteCommand(command, connection).ExecuteNonQuery();
            connection.Close();
        }
    }
}
