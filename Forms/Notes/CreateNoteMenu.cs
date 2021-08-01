using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_Studia {
    public partial class CreateNoteMenu : Form {

        private enum Office { Microsoft_Office, Libre_Office }
        public CreateNoteMenu() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
  
        }

        private void CreatePresentation(Office office) {
            File.Create("test.xls");
        }
    }
}
