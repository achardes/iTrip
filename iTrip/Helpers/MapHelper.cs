//using Microsoft.Maps.MapControl.WPF;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Runtime.Serialization.Json;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Media;

//namespace RoadTripManager
//{
//    public static class MapHelper
//    {
//        public static LocationCollection CallBingRestApi(List<Location> locations)
//        {
//            try
//            {
//                string key = new ConstantManager().BingMapKey;
//                List<string> waypoints = new List<string>();
//                int i = 0;

//                foreach (Location location in locations)
//                {
//                    waypoints.Add("wp." + i + "=" + location.Coordinates);
//                    i++;
//                }

//                Uri requestURI = new Uri(string.Format("http://dev.virtualearth.net/REST/V1/Routes/Driving?{0}&rpo=Points&key={1}", string.Join("&", waypoints), key));
//                BingRouteDrivingResponse bingRouteDrivingResponse = null;


//                    HttpWebRequest request = WebRequest.Create(requestURI) as HttpWebRequest;
//                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
//                    {
//                        using (Stream stream = response.GetResponseStream())
//                        {

//                            using (StreamReader reader = new StreamReader(stream))
//                            {
//                                string result = reader.ReadToEnd();

//                                bingRouteDrivingResponse = JsonConvert.DeserializeObject<BingRouteDrivingResponse>(result);
//                            }
//                        }
//                    }

//                if(bingRouteDrivingResponse != null)
//                {
//                    List<List<double>> coordinatesList = bingRouteDrivingResponse.resourceSets[0].resources[0].routePath.line.coordinates;
//                    LocationCollection locs = new LocationCollection();

//                    foreach (var coordinates in coordinatesList)
//                    {
//                        locs.Add(new Microsoft.Maps.MapControl.WPF.Location(coordinates.First(), coordinates.Last()));
//                    }

//                    return locs;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageHelper.DisplayException(ex);
//            }
//            return null;
//        }

//    }
//}
