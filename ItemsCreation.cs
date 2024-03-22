using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Route_Finder
{
    internal class ItemsCreation
    {
        List<Item> items = new List<Item>();
        const int size = 4;

        public ItemsCreation() 
        {
            string f = System.IO.File.ReadAllText("shoppingList.csv");
            f = f.Replace('\n', '\r');
            string[] lines = f.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            int r = lines.Length;
            int c = lines[0].Split(',').Length;
            string name = "";
            float price = 0, weight = 0;
            int quantity = 0;

            for (int j = 0; j < r; j++)
            {
                string[] line_i = lines[j].Split(',');

                for (int jj = 0; jj < c; jj++)
                {
                    switch(jj){
                        case 0:
                            name = line_i[jj];
                            break;

                        case 1:
                            price = float.Parse(line_i[jj]);
                            break;
                        case 2:
                            quantity = int.Parse(line_i[jj]);
                            break;
                        
                        case 3:
                            weight = int.Parse(line_i[jj]);
                            break;
                    }
                }

                items.Add(new Item(name, quantity, price, weight));
            }
        }



    }
}
