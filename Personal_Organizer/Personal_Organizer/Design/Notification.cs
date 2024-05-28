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
    public partial class Notification : Form
    {
        int interval = 0;
        public Notification()
        {
            InitializeComponent();
        }

        private void PositionNotBox()
        {
            int xPos = 0; int yPos = 0;
            xPos = Screen.GetWorkingArea(this).Width;
            yPos = Screen.GetWorkingArea(this).Height;
            this.Location = new Point(xPos-this.Width, yPos-this.Height);

        }

        private void Notification_Load(object sender, EventArgs e)
        {
            PositionNotBox();
            for (int i = 0; i<673; i++)
            {
                timer1.Start();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            linPanel.Width = linPanel.Width +2;
            if(linPanel.Width == 673)
            {
               // this.Close();
            }
        }

    }
}
