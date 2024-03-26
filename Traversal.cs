using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Route_Finder
{
    internal class Traversal
    {
        private PointsCreation p = new PointsCreation();

        private Greedy g = new Greedy();
        private BFS b = new BFS();
        private AStar a = new AStar();
        private Van van = new Van();

        

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

        public void bfs(Node root, Node target)
        {
            b.search(root, target);
        }

        public void aStar()
        {

        }

        public void greedy()
        {

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
