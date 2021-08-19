using System;

namespace PROJECT_Studia {
    public class ToDoTask {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public int Done { get; set; }
        public ToDoTask(int id, string title, string description, DateTime time, int done) {
            ID = id;
            this.Title = title;
            this.Description = description;
            this.Time = time;
            this.Done = done;
        }

    }
}