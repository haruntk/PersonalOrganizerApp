using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Organizer.Design
{
    public partial class AddList : Form
    {
        public AddList()
        {
            InitializeComponent();
        }

        private void titletextbox_TextChanged(object sender, EventArgs e)
        {
            if(titletextbox.Text.Length > 0)
            {
                addbtn.Enabled = true;  
            }
        }

        private void AddList_Load(object sender, EventArgs e)
        {
            addbtn.Enabled = false;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
