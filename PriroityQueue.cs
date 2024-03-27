using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Route_Finder
{
    internal class PriroityQueue
    {

        //this code is contributed by phasing17
        //from geeksforgeeks.com on the 26th march '24 with some edits to make it work in our position. 



            // Store the element of a priority queue
            private Node[] pr = new Node[100000];
            // Pointer to the last index
            private int size = -1;
            // Function to insert a new element
            // into priority queue
            public void enqueue(Node node, double priority)
            {
                // Increase the size
                size++;

                // Insert the element
                pr[size] = node;
                pr[size].priority = priority;
            }

            // Function to check the top element
            public int peek()
            {
                double highestPriority = int.MinValue;
                int ind = -1;

                // Check for the element with
                // highest priority
                for (int i = 0; i <= size; i++)
                {

                    // If priority is same choose
                    // the element with the
                    // highest value
                    if (highestPriority == pr[i].priority && ind > -1 && pr[ind].getIndex() < pr[i].getIndex())
                    {
                        highestPriority = pr[i].priority;
                        ind = i;
                    }
                    else if (highestPriority < pr[i].priority)
                    {
                        highestPriority = pr[i].priority;
                        ind = i;
                    }
             
                }

                // Return position of the element
                return ind;
            }

            // Function to remove the element with
            // the highest priority
            public Node dequeue()
            {
                // Find the position of the element
                // with highest priority
                int ind = peek();
                Node temp = pr[ind];

                // Shift the element one index before
                // from the position of the element
                // with highest priority is found
                for (int i = ind; i < size; i++)
                {
                    pr[i] = pr[i + 1];
                }

                // Decrease the size of the
                // priority queue by one
                size--;
                return temp;
            }

            public int count()
            {
                return size+1;
            }
            
      
        
    }
}
