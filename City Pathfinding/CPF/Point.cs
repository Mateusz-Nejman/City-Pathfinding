using System;
using System.Collections.Generic;

namespace Nejman.CPF
{
    public class Point
    {
        private List<string> neighbours;

        public List<string> Neighbours
        {
            get
            {
                return neighbours;
            }
            set
            {
                neighbours = value;
            }
        }

        public Point()
        {
            neighbours = new List<string>();
        }

        public Point(List<string> neighs)
        {
            neighbours = neighs;
        }

        public static double Distance(string point1, string point2)
        {
            //P_X_Y
            double x1 = double.Parse(point1.Split('_')[1]);
            double y1 = double.Parse(point1.Split('_')[2]);

            double x2 = double.Parse(point2.Split('_')[1]);
            double y2 = double.Parse(point2.Split('_')[2]);

            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public static string NearestPoint(string pointName, Point point)
        {
            double[] distances = new double[point.Neighbours.Count];

            int nearestIndex = 0;
            for(int a = 0; a < distances.Length; a++)
            {
                distances[a] = Distance(pointName, point.Neighbours[a]);
                if (distances[a] < distances[nearestIndex])
                    nearestIndex = a;
            }
            return point.Neighbours[nearestIndex];
        }

        public static string NearestPoint(string pointName, Point point, List<string> extrude)
        {
            Point pointTemp = new Point();
            
            for(int a = 0; a < point.Neighbours.Count; a++)
            {
                if (!extrude.Contains(point.Neighbours[a]))
                    pointTemp.Neighbours.Add(point.Neighbours[a]);
            }

            return NearestPoint(pointName, pointTemp);
        }
    }
}
