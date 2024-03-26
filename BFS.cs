﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Route_Finder
{
    internal class BFS
    {
        private Queue<Node> queue = new Queue<Node> ();
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

        private Node target = null;
        private int distance;

        public BFS()
        {
            distance = 0;
        }

        private Node searchNode()
        {
            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                foreach (Node n in node.getConnection())
                {
                    if (n == target)
                    {
                        return n;
                    }

                    if (!n.isExplored())
                    {

                        queue.Enqueue(n);
                        n.SetExplor();
                        Console.WriteLine(n.getStringCoOrd());
                    }       
                }
            }

            return null;
        }

        public void search(Node root, Node target)
        {
            this.target = target;
            queue.Enqueue(root);
            searchNode();
        }
    }
}
