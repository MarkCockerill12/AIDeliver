using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Route_Finder
{
    internal class Items
    {
        private List<Item> items;
        private Node deliveryLocation;
        public Items() { }

        public void addToItems(Item item) 
        {
            items.Add(item);
        }

        public void removeFromItems(Item item)
        {
            items.Remove(item);
        }

        public List<Item> getItems() 
        {  
            return items; 
        }

        public void setDeliveryLocation(Node deliveryLocation)
        {
            this.deliveryLocation = deliveryLocation;
        }

        public Node getDeliveryLocation()
        {
            return deliveryLocation;
        }

        public void loadItems()
        {

        }

    }
}
