using System;

namespace PROJECT_Studia {
    internal class ToDoTask {

        public int ID { get; set; }
        public string name { get; set; }
        public DateTime time { get; set; }
        public int done { get; set; }
        public ToDoTask(int id, string name, DateTime time, int done) {
            ID = id;
            this.name = name;
            this.time = time;
            this.done = done;
        }

    }
}