using System;
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
        private Queue<Node> queue = new Queue<Node>();
        Node[] cameFrom = new Node[100];

        private Node target = null;
        private double distance;

        public BFS()
        {
            distance = 0;
        }

        private int searchNode(Node root)
        {
            Node previousNode = root;
            Node node = null;
            while (queue.Count > 0)
            {
                node = queue.Dequeue();
                node.SetExplor();
                previousNode = node;

                foreach (Node n in node.getConnection())
                {
                    if (n == target)
                    {
                        cameFrom[n.getIndex()] = previousNode;
                        return 0;
                    }

                    if (!n.isExplored())
                    {

                        queue.Enqueue(n);
                        n.SetExplor();
                        cameFrom[n.getIndex()] = previousNode;

                    }
                }


            }

            return 1; //only if graph is not connected. 
        }

        public List<Node> getPath(bool flag)
        {

            List<Node> path = new List<Node>();
            Node currentNode = target;

            while (currentNode.getIndex() != 0)
            {
                path.Add(currentNode);
                if (currentNode.getIndex() != 0)
                {
                    currentNode = cameFrom[currentNode.getIndex()];    
                }
                if(currentNode == null)
                {
                    break;
                }

            }
            path.Add(currentNode);
            path.Reverse();

            if (flag)
            {
                BFS setDistances;
                for (int i = 0; i < path.Count; i++)
                {
                    setDistances = new BFS();
                    setDistances.search(path[i], target, true);
                    distance = path[i].getConnections()[target];

                    path[i].setDistanceToTarget(distance);
                }
            }
            return path;
        }

            public List<Node> search(Node root, Node target, bool flag)
            {
                if(root == null)
                {
                    return null;
                }
                this.target = target;
                queue.Enqueue(root);
                if (searchNode(root) == 0)
                {
                    return getPath(flag);
                }
                return null;
            }
        }
    }

