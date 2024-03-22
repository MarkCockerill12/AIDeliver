using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route_Finder
{
    internal class Item
    {
        private string name;
        private int quantity;
        private float price;
        private float weight;

        public Item()
        {

        }

        public Item(string name, int quantity, float price, float weight)
        {
            this.name = name;
            this.quantity = quantity;
            this.price = price;
            this.weight = weight;
        }

        public string getName()
        {
            return name;
        }

        public int getQuantity()
        {
            return quantity;
        }

        public float getPrice()
        {
            return price;
        }

        public float getWeight()
        { 
            return weight;
        }

        public void setQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        public void setPrice(float price)
        {
            this.price = price;
        }

        public void setWeight(float weight)
        {
            this.weight = weight;
        }
    }
}
