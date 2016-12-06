using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripManager
{
    public class ChartPoint<T>
    {
        public string Category { get; set; }
        public double Number { get; set; }
        public List<T> Items { get; set; }
    }
}
