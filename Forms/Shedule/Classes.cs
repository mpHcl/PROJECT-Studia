using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_Studia.Forms.Shedule {
    class Classes {       
        public int ID { get; set; }
        public string Title { get; set; }
        public Person Lecturer { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public class Person {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

        }

        public List<Classes> GetScheduleAsList() {
            

            return null;
        }
    }
}
