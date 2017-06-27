using System;
using System.Collections.Generic;
using System.Linq;

namespace iTrip
{
    public class DirectionRequest
    {
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public List<string> WayPoints { get; set; }

        public DirectionRequest(List<string> points)
        {
            WayPoints = new List<string>();
            if (points.Count >= 2)
            {
                StartPoint += points.First();
                EndPoint += points.Last();

                if (points.Count > 2)
                {
                    for (int i = 1; i < points.Count - 1; i++)
                    {
                        WayPoints.Add(points[i]);
                    }
                }
            }
        }
    }
}
