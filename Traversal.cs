using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Route_Finder
{
    internal class Traversal
    {
        private PointsCreation p = new PointsCreation();

        private Greedy g = new Greedy();
        private BFS b = new BFS();
        private AStar a = new AStar();
        public Van van = new Van();

        public Traversal()
        {
            a.setNodes(p.getAllNodes());
            g.setNodes(p.getAllNodes());
        }

        public void addDelivery(Node targetNode, Items items)
        {
            van.extendItems(items);
            van.extendTargetNode(targetNode);
        }


        public Node findDeliveryLocation(int x, int y)
        {
            int index = 0;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    index++;
                }
            }

            Node targetNode = p.getNode(index);

            return targetNode;
        }

        public void bfs(Node root, Node target)/*This will get a path, but it will not be the most effecient*/
        {
            List<Node> path = b.search(root, target, false);
            showPath(path);
        }

        public void aStar(Node root, Node target)
        {
            a.setRoot(root);
            a.setTarget(target);
            List<Node> path = a.aStar_search();
            showPath(path);
        }

        public void greedy(Node root, Node target)
        {
            g.setNodes(p.getAllNodes());
            g.setTarget(target);
            g.setStart(root);
            g.setDistances(target, root);
            List<Node> path = g.search();
            showPath(path);

        }

        public void showPath(List <Node> path)
        {
            StringBuilder pathString = new StringBuilder();
            foreach (Node node in path)
            {
                pathString.AppendLine(node.getStringCoOrd());
                Console.WriteLine(node.getStringCoOrd());
            }

            // Display the path in a message box
            MessageBox.Show("Route Path:" + pathString.ToString(), "Route Path", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        public List<Node> getNodes()
        {
            return p.getAllNodes();
        }
        public Node getNode(int index)
        {
            return p.getNode(index);
        }

        public int getLengthNodes()
        {
            return p.getNumberOfNodes();
        }

    }
}
