﻿using System;
using System.Collections.Generic;
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

    public class Node
    {
        private int height = 1;
        private Dictionary<Node, int> connections = new Dictionary<Node, int>();
        private int x, y;

        public Node(int x, int y, int height)
        {
            this.x = x; this.y = y; this.height = height;
        }

        public int getHeight()
        {
            return height;
        }

        public void setHeight(int height)
        {
            this.height = height;
        }

        public Dictionary<Node, int> getConnections()
        {
            return connections;
        }

        public void setAllConnections(Node p, int dist)
        {
            this.connections.Add(p, dist);
        }

        public int getSpecificPoint(Node p)
        {
            return (int)connections[p];
        }

        public List<Node> getConnection()
        {
            List<Node> pointConnections = new List<Node>();
            foreach (Node p in connections.Keys) { pointConnections.Add(p); }
            return pointConnections;
        }

        public string getStringCoOrd()
        {
            return x + ":" + y;
        }

        public string getStringConnectinos()
        {
            String s = "";
            foreach (Node p in getConnection())
            {
                s += p.getStringCoOrd() + ", ";
            }
            return s;
        }
    }

    public class PointsCreation
    {
        private const int size = 10;
        private List<Node> points = new List<Node>();
        

        public PointsCreation()
        {

            for (int j = 0; j < size; j++)
            {
                for (int ii = 0; ii < size; ii++)
                {
                    points.Add(new Node(j, ii, 1));
                }
            }

            string f = System.IO.File.ReadAllText("data.csv");
            f = f.Replace('\n', '\r');
            string[] lines = f.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            int r = lines.Length;
            int c = lines[0].Split(',').Length;

            int[,] val = new int[r, c];

            for (int j = 0; j < r; j++)
            {
                string[] line_i = lines[j].Split(',');
                for (int jj = 0;jj< c; jj++)
                {
                    Int32.TryParse(line_i[jj], out val[j,jj]);
                    Console.WriteLine(val[j,jj]);
                }


            }

            int index = 0;
            int i = 0;
            foreach (Node p in points)
            {
                while (index < c)
                {
                try
                    {
                    Console.WriteLine(val[i, index]);
                    p.setAllConnections(points[val[i, index]], 5);
                    }
                    catch (Exception e) {}

                    index++;
                }
                i++;
                index = 0;
                if(i > r)
                {
                    break;
                }
            }
        }

        public Node getNode(int index)
        {
            return this.points[index];
        }

        public List<Node> getAllNodes() { return this.points; }
        public int getNumberOfNodes() {  return this.points.Count; }
    }
}
