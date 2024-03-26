using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Route_Finder
{
    internal class Greedy
    {
        List<Node> nodes = new List<Node>();
        PriroityQueue priroityQueue = new PriroityQueue();
        Heuristics heuristics = new Heuristics();
        Node target = null;

        public Greedy() { }
        public void setNodes(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public void setTarget(Node target)
        {
            this.target = target;
        }

        
        private void gbfSearch()
        {
            // Perform GBF search algorithm
            HashSet<Node> visited = new HashSet<Node>();

            // Start GBF from the first node
            Node startNode = nodes[0];
            priroityQueue.enqueue(startNode, heuristics.heuristicFunction(startNode, target)); // Enqueue with heuristic value
            visited.Add(startNode);

            while (priroityQueue.count() > 0)
            {
                Node currentNode = priroityQueue.dequeue();

                // Process current node here...

                foreach (var neighbor in currentNode.getConnections().Keys)
                {
                    if (!visited.Contains(neighbor))
                    {
                        priroityQueue.enqueue(neighbor, heuristics.heuristicFunction(neighbor, target)); // Enqueue with heuristic value
                        visited.Add(neighbor);
                    }
                }
            }
        }

    }
    
}
