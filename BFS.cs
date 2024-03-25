using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route_Finder
{
    internal class BFS
    {
        private Queue<Node> queue = new Queue<Node> ();



        public BFS(Node root)
        {
            queue.Enqueue(root);
            search();
        }

        public void searchNode()
        {
            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                foreach (Node n in node.getConnection())
                {
                    if (!n.isExplored())
                    {
                        queue.Enqueue(n);
                        n.SetExplor();
                        Console.WriteLine(n.getStringCoOrd());
                    }       
                }
            }
        }

        public void search()
        {
            searchNode();
        }
    }
}
