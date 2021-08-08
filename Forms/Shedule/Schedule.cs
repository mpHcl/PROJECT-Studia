using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_Studia {
    public partial class Schedule : Form {

        SQLiteConnection connection;
        
        public Schedule() {
            InitializeComponent();
            string path = Directory.GetCurrentDirectory() + "\\data.db";
            if (!File.Exists(path)) {
                File.Create(path);
            }


            connection = new SQLiteConnection("URI=file:" + path);
        }

        private void button3_Click(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {

        }
    }
}
