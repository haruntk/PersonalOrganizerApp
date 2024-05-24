using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Organizer
{
    public partial class Kayıt : Form
    {
        public Kayıt()
        {
            InitializeComponent();
            

        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Close();
        }
    }
}
