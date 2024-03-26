using System;
using System.Collections.Generic;

namespace Route_Finder
{
    internal class PointsCreation
    {
        private const int size = 10;
        private List<Node> points = new List<Node>();
        

        public PointsCreation()
        {

            for (int j = 0; j < size; j++)
            {
                for (int ii = 0; ii < size; ii++)
                {
                    points.Add(new Node(j, ii, 1));
                }
            }

            string f = System.IO.File.ReadAllText("data.csv");
            f = f.Replace('\n', '\r');
            string[] lines = f.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            int r = lines.Length;
            int c = lines[0].Split(',').Length;

            int[,] val = new int[r, c];

            for (int j = 0; j < r; j++)
            {
                string[] line_i = lines[j].Split(',');
                for (int jj = 0;jj< c; jj++)
                {
                    Int32.TryParse(line_i[jj], out val[j,jj]);
                }


            }

            int index = 0;
            int i = 0;
            foreach (Node p in points)
            {
                while (index < c)
                {
                try
                    {
                    p.setAllConnections(points[val[i, index]], 1);
                    }
                    catch (Exception e) {
                        Console.WriteLine(e);
                    }

                    index++;
                }
                i++;
                index = 0;
                if(i > r)
                {
                    break;
                }
            }
        }

        public Node getNode(int index)
        {
            return this.points[index];
        }

        public List<Node> getAllNodes() { return this.points; }
        public int getNumberOfNodes() {  return this.points.Count; }
    }
}
