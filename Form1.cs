using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Expando;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Text;

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
        private Button searchBtn;
        private Label shopText;
        private int currentState = 1;
        private Button stateBtn;
        private Button routeBtn;
        private Traversal traversal = new Traversal();




        Label totalPriceLabel = new Label();
        Label itemsLabel = new Label();

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

            displayAddress();
        }

        private void setupDisplay()
        {
            // create a colored block for the info 
            infoPnl = new Panel { BackColor = Color.DarkBlue, Dock = DockStyle.Bottom, Height = 195, Width = 900 };
            Controls.Add(infoPnl);
            infoPnl.SendToBack();

            // create a colored block for the List    
            listPnl = new Panel { BackColor = Color.Black, Dock = DockStyle.Right, Height = 600, Width = 300 };
            Controls.Add(listPnl);
            listPnl.SendToBack();

            //add text
            listText = new Label { Text = "list goes here", ForeColor = Color.White, BackColor = Color.DodgerBlue, Font = new Font("Arial", 20, FontStyle.Regular), BorderStyle = BorderStyle.None, AutoSize = true, AutoEllipsis = false, MaximumSize = new Size(400, 900), Location = new Point(625, 10) };
            Controls.Add(listText);
            listText.BringToFront();

            //add text
            infoText = new Label { Text = "info goes here", ForeColor = Color.White, BackColor = Color.DodgerBlue, Font = new Font("Arial", 20, FontStyle.Regular), BorderStyle = BorderStyle.None, AutoSize = true, AutoEllipsis = false, Location = new Point(10, 380) };
            Controls.Add(infoText);
            infoText.BringToFront();

            // button 
            expandBtn = new Button { Text = "+", ForeColor = Color.Black, FlatStyle = FlatStyle.Popup, Font = new Font("Arial", 15, FontStyle.Regular), Location = new Point(840, 10), Size = new Size(25, 25), BackColor = Color.Red };
            expandBtn.Click += new EventHandler(this.expandBtn_Click);
            Controls.Add(this.expandBtn);
            expandBtn.BringToFront();

            //pop up panel shopping
            shopPnl = new Panel { BackColor = Color.Red, Location = new Point(40, 40), Height = (600 - (600 / 23)), Width = (900 - (900 / 20)), Visible = false, };
            Controls.Add(shopPnl);
            shopPnl.BringToFront();

            // Algorithm state button
            stateBtn = new Button { Location = new Point((900 - 900 / 3) - 120, 520), Font = new Font("Arial", 15, FontStyle.Regular), AutoSize = true, };
            stateBtn.Text = "A*";
            stateBtn.Click += StateButton_Click; // Attach click event handler
            Controls.Add(stateBtn); // Add button to form
            stateBtn.BringToFront();

            // Algorithm state button
            routeBtn = new Button { Location = new Point((900 - 900 / 3) - 220, 320), Font = new Font("Arial", 15, FontStyle.Regular), AutoSize = true, };
            routeBtn.Text = "Find Shortest Route";
            routeBtn.Click += RouteButton_Click; // Attach click event handler
            Controls.Add(routeBtn); // Add button to form
            routeBtn.BringToFront();
        }

        private void RouteButton_Click(object sender, EventArgs e)
        {
            // Display the shortest route based on the current state
            switch (currentState)
            {
                case 1:

                    MessageBox.Show("This will be the A*");
                    // Send the two nodes to the GBF algorithm
                    if (listBox.SelectedItems.Count == 2)
                    {
                        // Get the indices of the selected nodes
                        int index1 = listBox.SelectedIndices[0];
                        int index2 = listBox.SelectedIndices[1];

                        // Get the selected nodes
                        Node node1 = nodes[index1];
                        Node node2 = nodes[index2];

                        // Call the BFS algorithm with the selected nodes
                        /* bool routeSuccess = Astar_search(node1, node2); travers.Astar(whatever bits);

                         // Display the result
                         if (routeSuccess)
                             Console.WriteLine("Route successful");
                         else
                             Console.WriteLine("Route failed");*/
                    }
                    else
                    {
                        MessageBox.Show("Please select exactly two nodes for A* route finding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;
                case 2:

                    // Send the two nodes to the BFS algorithm
                    if (listBox.SelectedItems.Count == 2)
                    {
                        // Get the indices of the selected nodes
                        int index1 = listBox.SelectedIndices[0];
                        int index2 = listBox.SelectedIndices[1];

                        // Get the selected nodes
                        Node node1 = nodes[index1];
                        Node node2 = nodes[index2];

                        // Call the BFS algorithm with the selected nodes
                       /* bool routeSuccess = BFS_search(node1, node2); travers.BFS(whatever bits);

                        // Display the result
                        if (routeSuccess)
                            Console.WriteLine("Route successful");
                        else
                            Console.WriteLine("Route failed");*/
                    }
                    else
                    {
                        MessageBox.Show("Please select exactly two nodes for BFS route finding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 3:

                    // Send the two nodes to the GBF algorithm
                    if (listBox.SelectedItems.Count == 2)
                    {
                        // Get the indices of the selected nodes
                        int index1 = listBox.SelectedIndices[0];
                        int index2 = listBox.SelectedIndices[1];

                        // Get the selected nodes
                        Node node1 = nodes[index1];
                        Node node2 = nodes[index2];

                        // Call the BFS algorithm with the selected nodes
                        /* bool routeSuccess = GBF_search(node1, node2); travers.GBF(whatever bits);

                         // Display the result
                         if (routeSuccess)
                             Console.WriteLine("Route successful");
                         else
                             Console.WriteLine("Route failed");*/
                    }
                    else
                    {
                        MessageBox.Show("Please select exactly two nodes for GBF route finding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    break;
                default:
                    break;
            }
        }

        private void StateButton_Click(object sender, EventArgs e)
        {
            // Increment the state or loop back to 1 if it reaches 3
            currentState = currentState % 3 + 1;

            // Update button text based on the current state
            Button clickedButton = (Button)sender;
            switch (currentState)
            {
                case 1:
                    clickedButton.Text = "A* ";
                    break;
                case 2:
                    clickedButton.Text = "BFS";
                    break;
                case 3:
                    clickedButton.Text = "GBF";
                    break;
                default:
                    break;
            }
        }

        private void initialiseShop()
        {
            //create search bar for shopping
            searchShop = new TextBox { Text = "", Location = new Point(900 / 4, 60), Size = new Size(350, 40), Font = new Font("Arial", 26, FontStyle.Regular), BorderStyle = BorderStyle.None, BackColor = Color.Silver, ForeColor = Color.Black, Visible = false, };
            Controls.Add(searchShop);

            // seearch button 
            searchBtn = new Button { Text = "🔎", ForeColor = Color.Black, FlatStyle = FlatStyle.Popup, Font = new Font("Arial", 15, FontStyle.Regular), Location = new Point(((900 / 4) + 350), 60), Size = new Size(40, 40), BackColor = Color.White, Visible = false };
            searchBtn.Click += new EventHandler(this.searchBtn_Click);
            Controls.Add(this.searchBtn);

            // Panel to contain shop items with scrolling
            Panel shopItemsPanel = new Panel();
            shopItemsPanel.Location = new Point(900 / 4, 160); // Adjust location based on your layout
            shopItemsPanel.Size = new Size(400, 400); // Adjust size based on your layout
            shopItemsPanel.AutoScroll = true; // Enable vertical scrolling
            Controls.Add(shopItemsPanel);

            // Assign shopItemsPanel as the parent for displaying shop items
            shopPnl = new Panel { BackColor = Color.Red, Location = new Point(40, 40), Height = (600 - (600 / 10)), Width = (900 - (900 / 10)), Visible = false, };
            shopPnl.Controls.Add(shopItemsPanel); // Add shopItemsPanel as a child control of shopPnl
            Controls.Add(shopPnl);
            shopPnl.BringToFront();
        }

        private void expandBtn_Click(object sender, EventArgs e)
        {

            isExpanding = !isExpanding; // Toggle the state
            if (isExpanding)
            {
                expandBtn.Text = "-";
                shopPnl.Height = 0; // Set initial height to 0 before expanding
                shopPnl.Location = new Point((900 - shopPnl.Width) / 2, (600 - shopPnl.Height) / 2); // Set initial position below the form
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
            string itemDetails = removeButton.Tag.ToString();
            RemoveItemFromBasket(itemDetails);
        }

        // Method to add item to basket
        private void AddItemToBasket(string itemDetails)
        {
            // Define the path for the Basket.csv file
            string basketFilePath = @"Basket.csv";

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

        private void commitToBasket()
        {
            string[] lines = System.IO.File.ReadAllLines("Basket.csv");
            List<string> basketItems = new List<string>();
            foreach (string line in lines)
            {
                basketItems.Add(line);
            }
        }





        // Method to update the listText label with basket items and total price
        private void UpdateListText(List<string> basketItems)
        {
            string headingText = "Basket Items:\n";
            string itemsText = "";

            // Calculate total price and total weight
            decimal totalPrice = 0;
            decimal totalWeight = 0;

            foreach (string item in basketItems)
            {
                string[] components = item.Split(',');
                string itemName = components[0];
                int quantity = int.Parse(components[4]);
                decimal itemPrice = decimal.Parse(components[1]);
                decimal itemWeight = decimal.Parse(components[3]);

                totalWeight += itemWeight * quantity; // Calculate total weight
                decimal subtotal = itemPrice * quantity;
                totalPrice += subtotal;

                itemsText += $"{itemName} - Quantity: {quantity}\n";
            }

            // Apply additional fees for exceeding weight limit
            decimal additionalFee = 0;
            decimal weightLimit = 10; // 10 kg weight limit
            if (totalWeight > weightLimit)
            {
                decimal exceededWeight = totalWeight - weightLimit;
                // Calculate additional fee for every 10kg exceeded
                additionalFee = Math.Ceiling(exceededWeight / 10) * 5;
                totalPrice += additionalFee; // Add additional fee to total price
            }

            // Set the font size and style for the heading
            listText.Font = new Font("Arial", 16, FontStyle.Bold);

            // Set the text for the heading
            listText.Text = headingText;

            // Create a separate label for the basket items

            itemsLabel.Text = itemsText;

            // Set the font size and style for the basket items
            itemsLabel.Font = new Font("Arial", 10, FontStyle.Regular);
            itemsLabel.Location = new Point(listText.Left, listText.Bottom);
            itemsLabel.ForeColor = Color.Blue;

            // Add the items label below the heading
            itemsLabel.Location = new Point(listText.Left, listText.Bottom + 10);
            itemsLabel.AutoSize = true;

            // Add the items label to the form's controls
            Controls.Add(itemsLabel);

            // Create a label for the total price
            totalPriceLabel.Text = $"Total Price: £{totalPrice:F2}"; // Format the total price with two decimal places
            totalPriceLabel.Font = new Font("Arial", 12, FontStyle.Bold);

            totalPriceLabel.ForeColor = Color.Red;

            // Set the location of the total price label below the basket items label
            totalPriceLabel.Location = new Point(listText.Left, 520);
            totalPriceLabel.AutoSize = true;

            // Add the total price label to the form's controls
            Controls.Add(totalPriceLabel);

            // Display the total weight in the info text
            infoText.Text = $"Total Weight: {totalWeight} kg";

            // Check if additional fee is applied and display it
            if (additionalFee > 0)
            {
                infoText.Text += $"\nAdditional Fee: £{additionalFee:F2}";
            }

            // Bring all labels to the front
            listText.BringToFront();
            itemsLabel.BringToFront();
            totalPriceLabel.BringToFront();
            shopPnl.BringToFront();
            searchShop.BringToFront();
            searchBtn.BringToFront();

        }




        // Method to remove item from basket
        private void RemoveItemFromBasket(string itemDetails)
        {
            // Define the path for the Basket.csv file
            string basketFilePath = @"Basket.csv";

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
            string csvFilePath = @"ItemList.csv"; // Path to your CSV file

            if (!File.Exists(csvFilePath))
            {
                MessageBox.Show("CSV file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(csvFilePath);

            // Clear the existing items from the shop panel
            shopPnl.Controls.Clear();

            // Panel to contain shop items with scrolling
            Panel shopItemsPanel = new Panel();
            shopItemsPanel.Location = new Point(0, searchShop.Bottom + 10);
            shopItemsPanel.Size = new Size(shopPnl.Width, shopPnl.Height - searchShop.Bottom - 20);
            shopItemsPanel.AutoScroll = true;

            // Add shopItemsPanel to shopPnl
            shopPnl.Controls.Add(shopItemsPanel);

            // Calculate the starting Y-coordinate for displaying items
            int startY = 0;

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
                    shopItemsPanel.Controls.Add(itemLabel);

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
                    shopItemsPanel.Controls.Add(addButton);

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
                    shopItemsPanel.Controls.Add(removeButton);

                    // Add a separator
                    Panel separator = new Panel
                    {
                        BackColor = Color.Gray,
                        Size = new Size(shopItemsPanel.Width - 40, 1),
                        Location = new Point(20, itemLabel.Bottom + 5) // Adjust Y position below item label
                    };
                    shopItemsPanel.Controls.Add(separator);

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
                if (shopPnl.Height < 600 - (600 / 20))
                {
                    if (shopPnl.Top > (600 - shopPnl.Height) / 2)
                    {
                        shopPnl.Top -= expandIncrement / 2; // Move the panel up to maintain the middle position
                    }
                    shopPnl.Height += expandIncrement; // Increase the height gradually
                }
                else
                {
                    timer.Stop();

                    searchShop.Visible = true;
                    searchBtn.Visible = true;
                    //shopText.Visible = true;
                    searchShop.BringToFront();
                    searchBtn.BringToFront();
                    //shopText.BringToFront();
                }
            }
            else
            {
                if (shopPnl.Height > 0)
                {
                    searchShop.Visible = false;
                    searchBtn.Visible = false;
                

                    if (shopPnl.Top < (600 - shopPnl.Height) / 2)
                    {
                        shopPnl.Top += collapseIncrement / 2; // Move the panel down to maintain the middle position
                    }
                    shopPnl.Height -= collapseIncrement; // Decrease the height gradually

                    if (shopPnl.Height <= 0)
                    {
                        shopPnl.Visible = false;
                        timer.Stop();
                    }
                }
            }
        }



        private ListBox listBox;
        private List<Node> nodes; // Assuming this is accessible in the class

        private void displayAddress()
        {
            // Assuming t is an instance of Traversal class
            nodes = traversal.getNodes(); // Modify this method call based on your implementation

            // Create a ListBox to display the addresses
            listBox = new ListBox
            {
                Font = new Font("Arial", 10, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(585, 370), // Adjust the size as needed
                Location = new Point(0, 0), // Adjust the location as needed
                SelectionMode = SelectionMode.MultiExtended // Allow multiple selections
            };

            // Add each coordinate to the ListBox
            foreach (Node node in nodes)
            {
                listBox.Items.Add($"  Address: {node.getX()}, {node.getY()}");
            }

            // Allow the ListBox to automatically scroll
            listBox.ScrollAlwaysVisible = true;

            // Add double-click event handler for selecting addresses
            listBox.DoubleClick += ListBox_DoubleClick;

            // Add the ListBox to the form's controls
            Controls.Add(listBox);
        }

        // Event handler for double-clicking on addresses
        private void ListBox_DoubleClick(object sender, EventArgs e)
        {
            // Get the selected address
            string selectedAddress = listBox.SelectedItem.ToString();

            // Extract the coordinates from the selected address
            string[] parts = selectedAddress.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                string coordinates = parts[1];
                // Add the selected address to the route
                UpdateInfo(new List<string> { coordinates });
            }
        }




        // Method to update the info section with selected addresses
        private void UpdateInfo(List<string> selectedAddresses = null)
        {
            // If selectedAddresses is null, initialize it as an empty list
            if (selectedAddresses == null)
            {
                selectedAddresses = new List<string>();
            }

            // Check if node 0,0 is already present in the selected addresses
            bool hasDeliveryCenter = selectedAddresses.Any(address => address.StartsWith("Delivery Center: 0, 0"));

            // If not present, add node 0,0 to the selected addresses
            if (!hasDeliveryCenter)
            {
                selectedAddresses.Insert(0, "Delivery Center: 0, 0");
            }

            // Print the selected addresses to the console
            Console.WriteLine("Selected Addresses:");
            foreach (string address in selectedAddresses)
            {
                Console.WriteLine(address);
                // Add the address to the route text
                AddToRoute(address);
            }
        }

        // Method to add an address to the route
        private void AddToRoute(string address)
        {
            // Get the existing route text
            string existingRoute = infoText.Text;

            // If there are existing addresses, add a newline separator
            if (!string.IsNullOrEmpty(existingRoute))
            {
                existingRoute += "\n";
            }

            // Append the new address to the existing route text
            existingRoute += address;

            // Update the route text
            infoText.Text = existingRoute;
        }

    }

}