using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Eto.Forms;

namespace iTrip
{
    public static class MapHelper
    {
        public static Uri GoogleMapUrl()
        {
            string curDir = Directory.GetCurrentDirectory();
            return new Uri(string.Format("{0}/googleMap.html", curDir));
        }

        public static List<DirectionRequest> GetDirectionRequests(List<string> wayPoints)
        {
            List<DirectionRequest> directionRequests = new List<DirectionRequest>();
            int wayPointSize = 20;

            if (wayPoints.Count < 2) { return directionRequests; }

            while (wayPoints.Count > 0)
            {
                int cursor = wayPointSize + 2;
                var directionRequestPoints = new List<string>();

                while (cursor >= 0 && wayPoints.Count > 0)
                {
                    directionRequestPoints.Add(wayPoints.First());
                    wayPoints.RemoveAt(0);
                    cursor -= 1;
                }

                if (wayPoints.Count <= 2)
                {
                    directionRequestPoints.AddRange(wayPoints);
                    wayPoints.Clear();
                }
                else
                {
                    wayPoints.Insert(0, directionRequestPoints.Last());
                }

                directionRequests.Add(new DirectionRequest(directionRequestPoints));
            }
            return directionRequests;
        }


        public static string GetGoogleMapParameters(List<string> waypoints)
        {
            string str = "";

            if (waypoints.Count >= 2)
            {
                str += "'" + waypoints.First() + "',";
                str += "'" + waypoints.Last() + "'";

                if (waypoints.Count > 2)
                {
                    str += ",[";
                    for (int i = 1; i < waypoints.Count - 1; i++)
                    {
                        str += "'" + waypoints[i] + "',";
                    }
                    str.TrimEnd('\'');
                    str += "]";
                }
            }

            if (!string.IsNullOrEmpty(str))
            {
                str = "calculateAndDisplayRoute(" + str + ")";
            }
            Console.WriteLine(str);
            return str;
        }

        public static bool IsValidCoordinates(string coordinates)
        {
            return new Regex(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$").IsMatch(coordinates);
        }
    }
}
