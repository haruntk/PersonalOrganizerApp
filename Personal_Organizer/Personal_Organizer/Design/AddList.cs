using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Personal_Organizer.Design
{
    public partial class AddList : Form
    {
        public AddList()
        {
            InitializeComponent();
            addbtn.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0) addbtn.Enabled = true;
        }


        private void addbtn_Click_1(object sender, EventArgs e)
        {
            //kaydetme islemleri
            this.Hide();
        }
    }
}
