using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Route_Finder
{
    internal class Node
    {
        private int height = 1;
        private Dictionary<Node, double> connectionsOutOf = new Dictionary<Node, double>();
        private Dictionary<Node, double> connectionsInto = new Dictionary<Node, double>();
        private int x, y;
        private bool explored = false;
        public double priority = 0;
        private double dist;
        private double distanceToTarget;

        public Node(int x, int y, int height)
        {
            this.x = x; this.y = y; this.height = height;
        }

        public int getHeight()
        {
            return height;
        }

        public void setHeight(int height)
        {
            this.height = height;
        }

        public Dictionary<Node, double> getConnections()
        {
            return connectionsOutOf;
        }

        public Dictionary<Node, double> getConnectionsIn()
        {
            return connectionsInto;
        }

        public int getX()
        {
            return x;

        }

        public int getY()
        {
            return y;
        }

        public void setAllConnections(Node p, int connectedIndex)
        {
            dist = Math.Sqrt((connectedIndex + getIndex()) / 2);
            this.connectionsOutOf.Add(p, dist);
        }

        public int getSpecificPoint(Node p)
        {
            return (int)connectionsOutOf[p];
        }

        public List<Node> getConnection()
        {
            List<Node> pointConnections = new List<Node>();
            foreach (Node p in connectionsOutOf.Keys) { pointConnections.Add(p); }
            return pointConnections;
        }

        public string getStringCoOrd()
        {
            return x + ":" + y;
        }

        public string getStringConnectinos()
        {
            String s = "";
            foreach (Node p in getConnection())
            {
                s += p.getStringCoOrd() + ", ";
            }
            return s;
        }

        public int getIndex()
        {
            return Int32.Parse(x.ToString() + y.ToString());
        }

        public void SetExplor()
        {
            explored = true;
        }

        public bool isExplored()
        {
            return explored;
        }

        public void setConnectionsTo(Node node)
        {
            connectionsInto.Add(this, node.getConnections()[this]);
        }

        public void setDistanceToTarget(double distanceToTarget)
        {
            this.distanceToTarget = distanceToTarget;
        }

        public double getDistanceToTarget()
        {
            return distanceToTarget;
        }

    }
}
