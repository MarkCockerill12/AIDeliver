using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route_Finder
{
    internal class Heuristics
    {
        public int HeuristicCostEstimate(Node node, Node goal)
        {
            // Example of heuristic function (Euclidean distance)
            return (int)Math.Sqrt(Math.Pow((goal.getX() + goal.getY()) - (node.getX() + node.getY()), 2));
        }

        public List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
        {
            List<Node> totalPath = new List<Node> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Insert(0, current);
            }
            return totalPath;
        }

        public double heuristicFunction(Node node, Node target) //this is the broken bit. 
        {
            return node.getDistanceToTarget();

            if (node == target)
            {
                return 0;
            }
            return 1; //distance between current and target.
                      //So if was at a node 5 from the next and that next node was 2 from target it would return 7.
        }
    }
}
