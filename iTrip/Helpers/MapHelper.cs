using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eto.Forms;

namespace iTrip
{
    public static class MapHelper
    {
        public static Uri GoogleMapUrl()
        {
            string curDir = Directory.GetCurrentDirectory();
            return new Uri(String.Format("{0}/googleMap.html", curDir));
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

            return str;
        }
    }
}
