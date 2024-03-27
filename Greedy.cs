using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
        List<Node> visited = new List<Node>();

        Node target = null;
        Node startNode = null;

        public Greedy()
        {
            
        }

        public void setDistances(Node target, Node startNode)
        {
            BFS setDistances = new BFS();
            foreach(Node node in nodes)
            {
                if (node != startNode)
                {
                    setDistances.search(node, target, true);

                }
            }
        }

        public void setNodes(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public void setTarget(Node target)
        {
            this.target = target;
        }

        public void setStart(Node start)
        { 
            this.startNode = start;
        }

        public List<Node> search()
        {
            gbfSearch();
            return visited;
        }


        public int gbfSearch()
        {
            // Perform GBF search algorithm

            // Start GBF from the root node
            priroityQueue.enqueue(startNode, heuristics.heuristicFunction(startNode, target)); // Enqueue with heuristic value
            visited.Add(startNode);
            Node currentNode = null;

            while (priroityQueue.count() > 0)
            {
                currentNode = priroityQueue.dequeue();

                if (currentNode == target)
                {
                    return 0;
                }

                foreach (var neighbor in currentNode.getConnections().Keys)
                {
                    if (!visited.Contains(neighbor))
                    {
                        priroityQueue.enqueue(neighbor, heuristics.heuristicFunction(neighbor, target)); // Enqueue with heuristic value
                        visited.Add(neighbor);
                    }
                }



            }

            return 1;
        }

    }
    
}
