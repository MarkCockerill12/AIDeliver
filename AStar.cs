using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Route_Finder
{



    internal class AStar
    {
        Node root = null;
        Node target = null;
        List<Node> nodes = new List<Node>();
        Heuristics heuristics = new Heuristics();

        public AStar()
        {

        }

        public void setNodes(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public void setRoot(Node root)
        {
            this.root = root;
        }

        public void setTarget(Node target)
        {
            this.target = target;
        }

        public List<Node> aStar_search()
        {

            // Assuming you have start and goal nodes
            Node startNode = root;
            Node goalNode = target;

            if (startNode != null && goalNode != null)
            {
                // Perform A* search algorithm
                List<Node> path = aStar(startNode, goalNode, heuristics.HeuristicCostEstimate(root, target));
                if (path != null)
                {
                    // Path found, do something with it
                    return path;
                }
                else
                {
                    MessageBox.Show("No path found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Start or goal node not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

       

        private List<Node> aStar(Node start, Node goal, int heuristicEstimate)
        {
            // Implementation of the A* algorithm
            HashSet<Node> closedSet = new HashSet<Node>();
            HashSet<Node> openSet = new HashSet<Node> { start };
            Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>(); //key is root node, value is node connected to
            Dictionary<Node, double> gScore = new Dictionary<Node, double>();
            Dictionary<Node, double> fScore = new Dictionary<Node, double>();

            foreach (Node node in nodes)
            {
                gScore[node] = (double)int.MaxValue;
                fScore[node] = (double)int.MaxValue;
            }

            gScore[start] = 0;
            fScore[start] = heuristicEstimate;

            while (openSet.Count > 0)
            {
                Node current = openSet.OrderBy(node => fScore[node]).First();

                if (current == goal)
                {
                    return heuristics.ReconstructPath(cameFrom, current);
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var neighbor in current.getConnections())
                {
                    Node neighborNode = neighbor.Key;
                    double tentativeGScore = gScore[current] + neighbor.Value;

                    if (tentativeGScore < gScore[neighborNode])
                    {
                        cameFrom[neighborNode] = current;
                        gScore[neighborNode] = tentativeGScore;
                        fScore[neighborNode] = gScore[neighborNode] + heuristicEstimate;

                        if (!openSet.Contains(neighborNode))
                        {
                            openSet.Add(neighborNode);
                        }
                    }
                }
            }

            return null; // No path found
        }
    }

}