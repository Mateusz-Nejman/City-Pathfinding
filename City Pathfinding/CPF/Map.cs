using System;
using System.Collections.Generic;

namespace Nejman.CPF
{
    public class Map
    {
        private Dictionary<string, Point> points;

        public Dictionary<string, Point> Points
        {
            get
            {
                if (points == null)
                    points = new Dictionary<string, Point>();
                return points;
            }
            set
            {
                points = value;
            }
        }

        public List<string> Find(string startPoint, string endPoint)
        {
            Dictionary<string, ProcessCheckPoint> checkPoints = new Dictionary<string, ProcessCheckPoint>();
            List<string> checkedPoints = new List<string>();

            if (Points[startPoint].Neighbours.Contains(endPoint) || startPoint == endPoint)
                return new List<string>() { startPoint, endPoint };
            else
            {
                List<string> path = new List<string>();
                path.Add(startPoint);
                checkedPoints.Add(startPoint);
                string lastCheckpoint = "null";

                string currentCheck = startPoint;

                do
                {
                    if (!checkPoints.ContainsKey(currentCheck))
                    {
                        checkPoints.Add(currentCheck, new ProcessCheckPoint(currentCheck, lastCheckpoint));
                        lastCheckpoint = currentCheck;
                    }
                    List<string> toCheck = CheckPoint(currentCheck, endPoint, checkedPoints);

                    int checkCount = toCheck.Count;

                    if (checkCount == 0)
                    {
                        checkedPoints.Add(currentCheck);
                        if (lastCheckpoint == "null" || checkPoints[lastCheckpoint].parentCheckpoint == "null")
                            throw new Exception("ERROR " + lastCheckpoint + " " + currentCheck);
                        else
                        {
                            lastCheckpoint = checkPoints[lastCheckpoint].parentCheckpoint;
                            path = RemoveFromIndex(path, path.IndexOf(lastCheckpoint));
                            currentCheck = lastCheckpoint;
                        }
                    }
                    else if (checkCount == 1)
                    {
                        path.Add(toCheck[0]);
                        checkedPoints.Add(toCheck[0]);
                        if (toCheck[0] == endPoint)
                            return path;
                        else
                            currentCheck = toCheck[0];
                    }
                    else if (checkCount >= 2)
                    {
                        path.Add(toCheck[0]);
                        checkedPoints.Add(toCheck[0]);

                        currentCheck = toCheck[0];
                    }
                }
                while (true);
            }
        }

        private List<string> CheckPoint(string point, string end, List<string> checkedP)
        {
            Point tPoint = Points[point];
            Point endPoint = Points[end];
            List<string> pathsToCheck = new List<string>();

            for(int a = 0; a < tPoint.Neighbours.Count; a++)
            {
                string neighbour = tPoint.Neighbours[a];
                Point nPoint = Points[neighbour];

                if (endPoint.Neighbours.Contains(neighbour) || neighbour == end)
                    return new List<string>() { neighbour };
                else
                {
                    if (!checkedP.Contains(neighbour))
                    {
                        if (nPoint.Neighbours.Count == 1)
                            checkedP.Add(neighbour);
                        else
                            pathsToCheck.Add(neighbour);
                    }
                }
            }

            pathsToCheck.Sort((x, y) => Point.Distance(x, end).CompareTo(Point.Distance(y, end)));

            return pathsToCheck;

        }

        private List<string> RemoveFromIndex(List<string> array, int from)
        {
            if (from > array.Count)
                return array;
            else
            {
                List<string> ret = new List<string>();

                for (int a = 0; a <= from; a++)
                {
                    ret.Add(array[a]);
                }

                return ret;
            }
        }

        public void AddPoint(int x, int y)
        {
            string pointName = "P_" + x + "_" + y;
            Points.Add(pointName, new Point());
        }

        public void AddNeighbour(string name1, string name2)
        {
            Points[name1].Neighbours.Add(name2);
            Points[name2].Neighbours.Add(name1);
        }
    }
}
