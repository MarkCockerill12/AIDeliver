using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Expando;
using System.Windows.Forms;
using System.IO;
using System.Linq;

//sources
//read from file- https://www.c-sharpcorner.com/UploadFile/mahesh/how-to-read-a-text-file-in-C-Sharp/#:~:text=The%20File%20class%20provides%20two,text%20file%20into%20a%20string.
//search through file https://stackoverflow.com/questions/30078432/read-a-text-file-and-search-for-string-in-memory-efficient-way-and-abort-when-f


namespace Route_Finder
{

    public partial class Form1 : Form
    {
        private Panel listPnl;
        private Panel infoPnl;
        private Panel shopPnl;
        private Label mapText;
        private Label listText;
        private Label infoText;
        private TextBox searchShop;
        private Button expandBtn;
        private Timer timer;
        private bool isExpanding = false;
        private int expandStep = 10;
        private PointsCreation p = new PointsCreation();
        private Button searchBtn;
        private Label shopText;

        public Form1()
        {
            InitializeComponent();
            setupDisplay();
            initialiseShop();

            this.BackColor = Color.DarkGray;
            this.Size = new Size(900, 600);
            timer = new Timer();
            timer.Interval = 1;
            timer.Tick += Timer_Tick;

            drawMap();
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
            mapText = new Label { Text = "", ForeColor = Color.White, BackColor = Color.DodgerBlue, Font = new Font("Arial", 20, FontStyle.Regular), BorderStyle = BorderStyle.None, AutoSize = false };
            mapText.Size = new Size(this.Width - listPnl.Width, this.Height - infoPnl.Height); // Set size to cover the available area
            mapText.Location = new Point((this.Width - mapText.Width) / 2, (this.Height - mapText.Height) / 2); // Center the map panel
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
            expandBtn = new Button { Text = "+", ForeColor = Color.Black, FlatStyle = FlatStyle.Popup, Font = new Font("Arial", 15, FontStyle.Regular), Location = new Point(840, 10), Size = new Size(25, 25), BackColor = Color.Red };
            expandBtn.Click += new EventHandler(this.expandBtn_Click);
            Controls.Add(this.expandBtn);
            expandBtn.BringToFront();

            //pop up panel shopping
            shopPnl = new Panel { BackColor = Color.Red, Location = new Point(40, 40), Height = (this.Height - (this.Height / 20)), Width = (this.Width - (this.Width / 20)), Visible = false, };
            Controls.Add(shopPnl);
            shopPnl.BringToFront();

        }

        private void initialiseShop()
        {
            //create search bar for shopping
            searchShop = new TextBox {Text= "", Location = new Point(this.Width / 4, 60), Size = new Size(350, 40), Font = new Font("Arial", 26, FontStyle.Regular), BorderStyle = BorderStyle.None, BackColor = Color.Silver, ForeColor = Color.Black, Visible = false, };
            Controls.Add(searchShop);

            // seearch button 
            searchBtn = new Button { Text = "🔎", ForeColor = Color.Black, FlatStyle = FlatStyle.Popup, Font = new Font("Arial", 15, FontStyle.Regular), Location = new Point(((this.Width / 4)+ 350), 60), Size = new Size(40, 40), BackColor = Color.White, Visible = false };
            searchBtn.Click += new EventHandler(this.searchBtn_Click);
            Controls.Add(this.searchBtn);
           

            //add text
            shopText = new Label { Text = "Shop results", ForeColor = Color.White, BackColor = Color.DodgerBlue, Font = new Font("Arial", 20, FontStyle.Regular), BorderStyle = BorderStyle.None, AutoSize = true, AutoEllipsis = false, MaximumSize = new Size(400, 900), Location = new Point(this.Width / 4, 110), Visible = false };
            Controls.Add(shopText);
        }

        private void expandBtn_Click(object sender, EventArgs e)
        {
            isExpanding = !isExpanding; // Toggle the state
            if (isExpanding)
            {
                expandBtn.Text = "-";
                shopPnl.Height = 0; // Set initial height to 0 before expanding
                shopPnl.Location = new Point((this.Width - shopPnl.Width) / 2, (this.Height - shopPnl.Height) / 2); // Set initial position below the form
                timer.Start();
                shopPnl.Visible = true;
            }
            else
            {
                expandBtn.Text = "+";
                timer.Start();
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine(searchShop.Text);

            string textFile = @"C:\Temp\Data\Authors.txt";

            shopText.Text = searchShop.Text;

            //search for shop
            Console.WriteLine("searching for shop");
            if (File.Exists(textFile))
            {
                // Read a text file line by line.
                string[] lines = File.ReadAllLines(textFile);
                foreach (string line in lines)
                    Console.WriteLine(line);
            }

            //COULD ALSO DO 
            //FileContainsString(textFile, searchShop.Text, true);
        }

       /* public static bool FileContainsString(string path, string str, bool caseSensitive = true)
        {
            if (String.IsNullOrEmpty(str))
                return false;

            using (var stream = new StreamReader(path))
                while (!stream.EndOfStream)
                {
                    bool stringFound = true;
                    for (int i = 0; i < str.Length; i++)
                    {
                        char strChar = caseSensitive ? str[i] : Char.ToUpperInvariant(str[i]);
                        char fileChar = caseSensitive ? (char)stream.Read() : Char.ToUpperInvariant((char)stream.Read());
                        if (strChar != fileChar)
                        {
                            stringFound = false;
                            break; // break for-loop, start again with first character at next position
                        }
                    }
                    if (stringFound)
                        return true;
                }
            return false;
        }*/



        private void Timer_Tick(object sender, EventArgs e)
        {
            int expandIncrement = expandStep;
            int collapseIncrement = expandStep;

            if (isExpanding)
            {
                if (shopPnl.Height < this.Height - (this.Height / 20))
                {
                    if (shopPnl.Top > (this.Height - shopPnl.Height) / 2)
                    {
                        shopPnl.Top -= expandIncrement / 2; // Move the panel up to maintain the middle position

                        shopPnl.Height += expandIncrement; // Increase the height gradually
                    }
                    else
                    {
                        shopPnl.Height += expandIncrement; // Increase the height gradually
                    }
                }
                else
                {
                    timer.Stop();
                    
                    searchShop.Visible = true;
                    searchBtn.Visible = true;
                    shopText.Visible = true;
                    searchShop.BringToFront();
                    searchBtn.BringToFront();
                    shopText.BringToFront();
                }
            }
            else
            {
                if (shopPnl.Height > 0)
                {
                    
                    searchShop.Visible = false;
                    searchBtn.Visible = false;
                    shopText.Visible = false;

                    if (shopPnl.Top < (this.Height - shopPnl.Height) / 2)
                    {
                        shopPnl.Top += collapseIncrement / 2; // Move the panel down to maintain the middle position
                        shopPnl.Height -= collapseIncrement; // Decrease the height gradually
                    }
                    else
                    {
                        shopPnl.Height -= collapseIncrement; // Decrease the height gradually
                    }

                    if (shopPnl.Height <= 0)
                    {
                        shopPnl.Visible = false;
                        timer.Stop();
                    }
                }
            }
        }
        private void drawMap()
        {
            // Clear the existing drawing on the map panel
            mapText.Refresh();

            // Get the Graphics object to draw on the map panel
            Graphics g = mapText.CreateGraphics();

            // Calculate the cell size based on the number of nodes and the size of the map panel
            int cellSize = Math.Min(mapText.Width / p.getNumberOfNodes(), mapText.Height / p.getNumberOfNodes());

            // Loop through all nodes and plot them on the map
            foreach (Node node in p.getAllNodes())
            {
                int x = (node.X * cellSize) + cellSize / 2; // Calculate the x-coordinate with an offset to center the nodes
                int y = (node.Y * cellSize) + cellSize / 2; // Calculate the y-coordinate with an offset to center the nodes

                // Draw a rectangle at the calculated position to represent the node
                g.FillRectangle(Brushes.Black, x, y, cellSize, cellSize);
            }

            // Dispose of the Graphics object to free resources
            g.Dispose();
        }






    }

    public class Node
    {
        private int height = 1;
        private Dictionary<Node, int> connections = new Dictionary<Node, int>();
        public int X { get; private set; } // X coordinate property
        public int Y { get; private set; } // Y coordinate property

        public Node(int x, int y, int height)
        {
            this.X = x;
            this.Y = y;
            this.height = height;
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
            return X + ":" + Y;
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

            for (int i = 0; i < size; i++)
            {
                for (int ii = 0; ii < size; ii++)
                {
                    points.Add(new Node(i, ii, 1));
                }
            }

            var rnd = new Random();
            int rnd1;

            foreach (Node p in points)
            {
                try
                {
                    for (int i = 0; i < size; i++)
                    {
                        rnd1 = rnd.Next(size * size);
                        p.setAllConnections(points[rnd1], 5); //All connections are directed, ie one way. It will be a pain to make it omnidirectional. 
                    }
                }
                catch (Exception e) //For when connection has already been made.
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine(p.getStringCoOrd());
                Console.WriteLine(p.getStringConnectinos());
                Console.WriteLine("");
            }
        }

        public Node getNode(int index)
        {
            return this.points[index];
        }

        public List<Node> getAllNodes() { return this.points; }
        public int getNumberOfNodes() { return this.points.Count; }
    }
}