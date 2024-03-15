// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.XPath;





public class Point
{
    private int height = 1;
    private Dictionary<Point, int> connections = new Dictionary<Point, int>();
    private int x, y;

    public Point(int x, int y, int height)
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

    public Dictionary<Point, int> getConnections()
    {
        return connections;
    }

    public void setAllConnections(Point p, int dist)
    {
        this.connections.Add(p, dist);

    }

    public int getSpecificPoint(Point p)
    {
        return (int)connections[p];
    }

    public List<Point> getConnection()
    {
        List<Point> pointConnections = new List<Point>();
        foreach(Point p in connections.Keys) { pointConnections.Add(p); }
        return pointConnections;

    }
    public string getStringCoOrd()
    {
        return x + ":" + y;
    }

    public string getStringConnectinos()
    {
        String s = "";
        foreach(Point p in getConnection())
        {
            s += p.getStringCoOrd() + ", ";
        }
        return s;
    }
}



public class PointsCreation
{
    public static void Main(string[] args)
    {
        const int size = 10;
        var points = new List<Point>();
        

        for (int i = 0; i < size; i++)
        {
            for(int ii = 0; ii < size; ii++)
            {
                points.Add(new Point(i, ii, 1));
                
            }
        }

        var rnd = new Random();
        int rnd1;

        foreach(Point p in points)
        {
            
            try
            {
                for(int i=0; i<size; i++)
                {
                    rnd1 = rnd.Next(size*size);
                    p.setAllConnections(points[rnd1], 5);
                }
            }
            catch (Exception e)
            {

            }
            Console.WriteLine(p.getStringCoOrd());
            Console.WriteLine(p.getStringConnectinos());
            Console.WriteLine("");

        }
        

    }
}
