using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace PROJECT_Studia {
    public partial class Notes : Form {
        Home home;
        public Notes() {
            InitializeComponent();
            if (!Directory.Exists("notes")) {
                Directory.CreateDirectory("notes");
            }
        }

        public Notes(Home form) {
            InitializeComponent();
            if (!Directory.Exists("notes")) {
                Directory.CreateDirectory("notes");
            }
            this.home = form;
        }

        private void button5_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {
            string[] path = openFileDialog1.FileName.Split('\\');
            File.Copy(openFileDialog1.FileName, 
                "notes\\" + path[path.Length - 1]);
        }

        private void button2_Click(object sender, EventArgs e) {
            var browse = new BrowseNotes(Directory.GetFiles("notes\\"));
            browse.TopLevel = false;
            browse.Visible = true;
            home.Panel.Visible = true;
            home.Panel.Controls.Clear();
            home.Panel.Controls.Add(browse);
        }
    }
}
