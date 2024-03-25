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
        private string description;
        private float weight;
        private float cost;
        private int quantity;

        public Item() { }

        public Item(string name, int weight, float cost, int quantity)
        {
            this.name = name;
            this.weight = weight;
            this.cost = cost;
            this.quantity = quantity;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setDescription(string description)
        {
            this.description = description;
        }

        public void setWeight(float weight)
        {
            this.weight = weight;
        }

        public void setCost(float cost)
        {
            this.cost = cost;
        }

        public void setQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        public string getName()
        {
            return this.name;
        }

        public string getDescription()
        {
            return this.description;
        }

        public float getWeight()
        {
            return this.weight;
        }

        public float getCost()
        {
            return this.cost;
        }

        public int getQuantity()
        {
            return this.quantity;
        }

    }
}
