using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Expando;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

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
                shopPnl.BringToFront(); // Bring the red pop-up panel to the front
            }
            else
            {
                expandBtn.Text = "+";
                timer.Start();
            }
        }

        // Event handler for add button click
        private void AddButton_Click(object sender, EventArgs e)
        {
            Button addButton = (Button)sender;
            string itemDetails = addButton.Tag.ToString(); // Get item details from Tag property
            AddItemToBasket(itemDetails);
        }

        // Event handler for remove button click
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            Button removeButton = (Button)sender;
            string itemDetails = removeButton.Tag.ToString(); // Get item details from Tag property
            RemoveItemFromBasket(itemDetails);
        }

        // Method to add item to basket
        // Method to add item to basket
        // Method to add item to basket
        private void AddItemToBasket(string itemDetails)
        {
            // Define the path for the Basket.csv file
            string basketFilePath = @"C:\Users\bossu\OneDrive\Desktop\Uni\sem2\New folder\AI_Delivery\Basket.csv";

            try
            {
                // Split the item details
                string[] components = itemDetails.Split(',');

                // Extract the item name, price, quantity, and weight
                string itemName = components[0];
                string itemPrice = components[1];
                string itemQuantity = components[2];
                string itemWeight = components[3];
                int itemCount = 1; // Default count for a new item

                // Check if the item already exists in the basket
                bool itemExists = false;

                // Read all lines from the Basket.csv file
                List<string> lines = File.ReadAllLines(basketFilePath).ToList();

                for (int i = 0; i < lines.Count; i++)
                {
                    string line = lines[i];
                    string[] lineComponents = line.Split(',');

                    if (lineComponents[0] == itemName)
                    {
                        // Update the itemCount
                        int currentItemCount = int.Parse(lineComponents[4]);
                        currentItemCount++;
                        itemCount = currentItemCount; // Update itemCount with the new count

                        // Reconstruct the line with updated item count
                        lines[i] = $"{itemName},{itemPrice},{itemQuantity},{itemWeight},{itemCount}";
                        itemExists = true;
                        break;
                    }
                }

                if (!itemExists)
                {
                    // Add the item to the basket with quantity 1
                    string newItem = $"{itemName},{itemPrice},{itemQuantity},{itemWeight},{itemCount}";
                    lines.Add(newItem);
                }

                // Write the updated list back to the Basket.csv file
                File.WriteAllLines(basketFilePath, lines);

                // Update the listText label with basket items
                UpdateListText(lines);

                MessageBox.Show($"Selected item '{itemName}' added to basket.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred while adding item to basket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        Label totalPriceLabel = new Label();
        Label itemsLabel = new Label();

        // Method to update the listText label with basket items and total price
        private void UpdateListText(List<string> basketItems)
        {
            string headingText = "Basket Items:\n";
            string itemsText = "";

            // Calculate total price
            decimal totalPrice = 0;

            foreach (string item in basketItems)
            {
                string[] components = item.Split(',');
                string itemName = components[0];
                int quantity = int.Parse(components[4]);
                decimal itemPrice = decimal.Parse(components[1]);
                decimal subtotal = itemPrice * quantity;

                totalPrice += subtotal;

                itemsText += $"{itemName} - Quantity: {quantity}\n";
            }

            // Set the font size and style for the heading
            listText.Font = new Font("Arial", 16, FontStyle.Bold);

            // Set the text for the heading
            listText.Text = headingText;

            // Create a separate label for the basket items
            
            itemsLabel.Text = itemsText;

            // Set the font size and style for the basket items
            itemsLabel.Font = new Font("Arial", 10, FontStyle.Regular);
            itemsLabel.ForeColor = Color.Blue;

            // Add the items label below the heading
            itemsLabel.Location = new Point(listText.Left, listText.Bottom);
            itemsLabel.AutoSize = true;

            // Add the items label to the form's controls
            Controls.Add(itemsLabel);

            // Create a label for the total price
            totalPriceLabel.Text = $"Total Price: £{totalPrice:F2}"; // Format the total price with two decimal places
            totalPriceLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            totalPriceLabel.ForeColor = Color.Red;

            // Set the location of the total price label below the basket items label
            totalPriceLabel.Location = new Point(itemsLabel.Left, itemsLabel.Bottom + 10);
            totalPriceLabel.AutoSize = true;

            // Add the total price label to the form's controls
            Controls.Add(totalPriceLabel);

            // Bring both labels to the front
            listText.BringToFront();
            itemsLabel.BringToFront();
            totalPriceLabel.BringToFront();
            shopPnl.BringToFront();
        }



        // Method to remove item from basket
        private void RemoveItemFromBasket(string itemDetails)
        {
            // Define the path for the Basket.csv file
            string basketFilePath = @"C:\Users\bossu\OneDrive\Desktop\Uni\sem2\New folder\AI_Delivery\Basket.csv";

            try
            {
                // Split the item details
                string[] components = itemDetails.Split(',');

                // Extract the item name
                string itemName = components[0];

                // Read all lines from the Basket.csv file
                List<string> lines = File.ReadAllLines(basketFilePath).ToList();

                // Flag to check if the item was found in the basket
                bool itemFound = false;

                // Find and remove the selected item from the list
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] lineComponents = lines[i].Split(',');

                    if (lineComponents[0] == itemName)
                    {
                        // Update the item count
                        int currentCount = int.Parse(lineComponents[4]);
                        currentCount--;

                        if (currentCount <= 0)
                        {
                            // Remove the item if count is 0 or less
                            lines.RemoveAt(i);
                        }
                        else
                        {
                            // Update the count in the line
                            lineComponents[4] = currentCount.ToString();
                            lines[i] = string.Join(",", lineComponents);
                        }

                        itemFound = true;
                        break;
                    }
                }

                if (!itemFound)
                {
                    MessageBox.Show($"There is no '{itemName}' in the basket to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Write the updated list back to the Basket.csv file
                File.WriteAllLines(basketFilePath, lines);
                // Update the listText label with basket items
                UpdateListText(lines);

                MessageBox.Show($"Selected item '{itemName}' removed from basket.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred while removing item from basket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string searchTerm = searchShop.Text.Trim().ToLower(); // Trim whitespace and convert to lowercase
            string csvFilePath = @"C:\Users\bossu\OneDrive\Desktop\Uni\sem2\New folder\AI_Delivery\ItemList.csv"; // Path to your CSV file

            if (!File.Exists(csvFilePath))
            {
                MessageBox.Show("CSV file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(csvFilePath);

            // Clear the existing items from the shop panel
            shopPnl.Controls.Clear();

            // Create a list to hold selected items
            List<string> selectedItems = new List<string>();

            // Calculate the starting Y-coordinate for displaying items
            int startY = searchShop.Bottom + 30;

            // Display the matching items
            foreach (string line in lines)
            {
                string[] components = line.Split(','); // Split the line into components

                // Ensure there are at least four components (name, price, quantity, weight)
                if (components.Length >= 4 && components[0].ToLower().Contains(searchTerm))
                {
                    // Format the item for display
                    string formattedItem = $"{components[0]}, £{components[1]}, {components[2]}, {components[3]} kg";

                    // Create label for displaying the item
                    Label itemLabel = new Label
                    {
                        Text = formattedItem,
                        ForeColor = Color.White,
                        BackColor = Color.DodgerBlue,
                        Font = new Font("Arial", 12, FontStyle.Regular),
                        BorderStyle = BorderStyle.FixedSingle,
                        AutoSize = true,
                        Location = new Point(20, startY) // Set Y-coordinate relative to the search bar
                    };
                    shopPnl.Controls.Add(itemLabel);

                    // Create add button for adding item to basket
                    Button addButton = new Button
                    {
                        Text = "+",
                        ForeColor = Color.Black,
                        BackColor = Color.ForestGreen,
                        FlatStyle = FlatStyle.Popup,
                        Font = new Font("Arial", 10, FontStyle.Regular),
                        Size = new Size(30, 20),
                        Location = new Point(itemLabel.Right + 10, itemLabel.Top), // Align with right side of label
                        Tag = line // Store item details in Tag property
                    };
                    addButton.Click += AddButton_Click; // Attach click event handler
                    shopPnl.Controls.Add(addButton);

                    // Create remove button for removing item from basket
                    Button removeButton = new Button
                    {
                        Text = "-",
                        ForeColor = Color.Black,
                        BackColor = Color.Crimson,
                        FlatStyle = FlatStyle.Popup,
                        Font = new Font("Arial", 10, FontStyle.Regular),
                        Size = new Size(30, 20),
                        Location = new Point(addButton.Right + 10, itemLabel.Top), // Align with right side of add button
                        Tag = line // Store item details in Tag property
                    };
                    removeButton.Click += RemoveButton_Click; // Attach click event handler
                    shopPnl.Controls.Add(removeButton);

                    // Add a separator
                    Panel separator = new Panel
                    {
                        BackColor = Color.Gray,
                        Size = new Size(shopPnl.Width - 40, 1),
                        Location = new Point(20, itemLabel.Bottom + 5) // Adjust Y position below item label
                    };
                    shopPnl.Controls.Add(separator);

                    // Add formatted item to selected items list
                    selectedItems.Add(formattedItem);

                    // Increment startY for the next item
                    startY = separator.Bottom + 5;
                }
            }
        }

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
