using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Route_Finder
{

    internal class Van
    {
        private int fuelCapacity; //max fuel capactiy in L
        private float usage;    //fuel usage km per L on the flat
        private float currentFuel; //current amount of fuel
        private int maxCapacity; //max capactiy of items in kg
        private float loadedAmount; // wight of loaded items
        private List<Items> items = new List<Items>(); //list of each set of items to be delivered
        private List<Node> targetNodes = new List<Node>(); //list of each node each list of items is to be delivered to. 
        
        public Van()
        {

        }

        public void setFuel(float fuel)
        {
            currentFuel = fuel;
        }

        public void extendItems(Items items)
        {
            this.items.Add(items);
        }

        public void extendTargetNode(Node targetNode)
        {
            targetNodes.Add(targetNode);
        }

        public void setUsage(float usage)
        {
            this.usage = usage;
        }

        public void setCapacity(int capacity)
        {
            maxCapacity = capacity;
        }

        public void updatePercentage()
        {
            float weight = 0;
            foreach (Items list in items)
            {
                foreach (Item item in list.getItems())
                {
                    weight += item.getWeight();
                }

                if((loadedAmount += weight) <= maxCapacity)
                {
                    loadedAmount =+ weight;
                }
                else
                {
                    //send message to user saying that van is full.
                    break;
                }

            }

        }

    }
}
