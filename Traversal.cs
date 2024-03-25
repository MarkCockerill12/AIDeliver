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
        private Van van = new Van();


        public Traversal() { }
        

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
            BFS breadthFirstSeatch = new BFS(root, target);
        }

    }
}
