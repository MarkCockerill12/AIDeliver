using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Expando;
using System.Windows.Forms;
using System.IO;

namespace Route_Finder
{
    public partial class Form1 : Form
    {
        private Panel listPnl;
        private Panel infoPnl;
        private Label mapText;
        private Label listText;
        private Label infoText;
        private System.Windows.Forms.Button expandBtn;
        private PointsCreation p = new PointsCreation();

        public Form1()
        {
            InitializeComponent();
            setupDisplay();
            
            this.BackColor = Color.DarkGray;
            this.Size = new Size(900, 600);
        }

        private void setupDisplay()
        {
            // create a colored block for the info 
            infoPnl = new Panel { BackColor = Color.DarkBlue, Dock = DockStyle.Bottom, Height = this.Height / 3, Width = this.Width };
            Controls.Add(infoPnl);
            infoPnl.SendToBack();

            // create a colored block for the List    
            listPnl = new Panel { BackColor = Color.Black, Dock = DockStyle.Right, Height = this.Height, Width = this.Width / 3 };
            Controls.Add(listPnl);
            listPnl.SendToBack();

            //add text
            mapText = new Label { Text = "map goes here", ForeColor = Color.White, BackColor = Color.DodgerBlue, Font = new Font("Arial", 20, FontStyle.Regular), BorderStyle = BorderStyle.None, AutoSize = true, AutoEllipsis = false, MaximumSize = new Size(400, 900), Location = new Point(10, 10) };
            Controls.Add(mapText);
            mapText.BringToFront();

            //add text
            listText = new Label { Text = "list goes here", ForeColor = Color.White, BackColor = Color.DodgerBlue, Font = new Font("Arial", 20, FontStyle.Regular), BorderStyle = BorderStyle.None, AutoSize = true, AutoEllipsis = false, MaximumSize = new Size(400, 900), Location = new Point(625, 10) };
            Controls.Add(listText);
            listText.BringToFront();

            //add text
            infoText = new Label { Text = "info goes here", ForeColor = Color.White, BackColor = Color.DodgerBlue, Font = new Font("Arial", 20, FontStyle.Regular), BorderStyle = BorderStyle.None, AutoSize = true, AutoEllipsis = false, MaximumSize = new Size(400, 900), Location = new Point(10, 450) };
            Controls.Add(infoText);
            infoText.BringToFront();

            // button 
            expandBtn = new System.Windows.Forms.Button { Text = "+", ForeColor = Color.Black, FlatStyle = FlatStyle.Popup, Font = new Font("Arial", 15, FontStyle.Regular), Location = new Point(840, 10), Size = new Size(25, 25), BackColor = Color.Red };
            expandBtn.Click += new EventHandler(this.expandBtn_Click);
            Controls.Add(this.expandBtn);
            expandBtn.BringToFront();
        }

        private void expandBtn_Click(object sender, EventArgs e) // expand the panel, THIS IS NOT FINISHED, IT SHOULD BE A POP UP FOR SHOPPING
        {
            // expand the panel
            if (listPnl.Width == this.Width / 3)
            {
                listPnl.Width = this.Width;
                listText.Width = 900;
                expandBtn.Text = "-";
            }
            else
            {
                listPnl.Width = this.Width / 3;
                listText.Width = 300;
                expandBtn.Text = "+";
            }
        }

    }
}
