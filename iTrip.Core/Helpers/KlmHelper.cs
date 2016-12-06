using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripManager
{
    public static class KlmHelper
    {
        public static string KlmTemplate = @"
        <?xml version='1.0' encoding='UTF-8'?>
        <kml xmlns = 'http://www.opengis.net/kml/2.2'>
            <Document>
                <name>{0}</name>
		        {1}
		        {2}
	        </Document>
        </kml>";

        public static string KlmWaypointsTemplate = @"
        <Placemark>
			<name>{0}</name>
			<styleUrl>#line-1267FF-5-nodesc</styleUrl>
			<ExtendedData>
			</ExtendedData>
			<LineString>
				<tessellate>1</tessellate>
				<coordinates>{1}</coordinates>
			</LineString>
		</Placemark>";

        public static string KlmPushpinTemplate = @"
		<Placemark>
			<name>{0}</name>
			<styleUrl>#icon-503-DB4436-nodesc</styleUrl>
			<ExtendedData>
			</ExtendedData>
			<Point>
				<coordinates>{1}</coordinates>
			</Point>
		</Placemark>";

        public static string GetKlmData(List<Journey> journeys)
        {
            string KlmResult = string.Empty;
            List<string> KlmWaypoints = new List<string>();
            List<string> KlmPushpins = new List<string>();

            foreach (Journey journey in journeys)
            {
                if(journey.LocationCollection != null)
                {
                    List<string> klmLocations = new List<string>();

                    foreach (var item in journey.LocationCollection)
                    {
                        klmLocations.Add(item.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + item.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + ",0.0");
                    }

                    if (klmLocations.Any())
                    {
                        KlmWaypoints.Add(string.Format(KlmWaypointsTemplate, journey.Period.ShortDisplayName, string.Join(" ", klmLocations)));
                    }

                    if (journey.Bivouac.Location.HasValidCoordinates)
                    {
                        KlmPushpins.Add(string.Format(KlmPushpinTemplate, journey.Bivouac.Type, journey.Bivouac.Location.KlmCoordinates));
                    }

                    foreach (Event item in journey.Events.Where(x => x.Location.HasValidCoordinates))
                    {
                        KlmPushpins.Add(string.Format(KlmPushpinTemplate, item.Name, item.Location.KlmCoordinates));
                    }
                }
            }

            KlmResult = string.Format(KlmTemplate, "Export From Road Trip", string.Join(Environment.NewLine, KlmWaypoints), string.Join(Environment.NewLine, KlmPushpins));

            return KlmResult;
        }

    }
}
