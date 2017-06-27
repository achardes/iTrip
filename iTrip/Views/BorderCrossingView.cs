using System;
using Eto.Drawing;
using Eto.Forms;

namespace iTrip
{
    public class BorderCrossingView
    {

        public int VisaDuration { get; set; }
        public int VisaPrice { get; set; }

        public int VisaVehicleDuration { get; set; }
        public int VisaVehiclePrice { get; set; }

        public bool Fumigation { get; set; }
        public int FumigationPrice { get; set; }

        public bool VehicleInspection { get; set; }

        public bool Tramidores { get; set; }
        public int TramidoresPrice { get; set; }


        public static Control GetView(BorderCrossing borderCrossing)
        {
            NumericUpDown visaDuration = new NumericUpDown();
            visaDuration.DataContext = borderCrossing;
            visaDuration.BindDataContext(c => c.Value, (BorderCrossing m) => m.VisaDuration);
            visaDuration.Tag = "Visa Duration";
            visaDuration.Width = 50;

            NumericUpDown visaPrice = new NumericUpDown();
            visaPrice.DataContext = borderCrossing;
            visaPrice.BindDataContext(c => c.Value, (BorderCrossing m) => m.VisaPrice);
            visaPrice.Tag = "Visa Price";
            visaPrice.Width = 50;


            NumericUpDown visaVehicleDuration = new NumericUpDown();
            visaVehicleDuration.DataContext = borderCrossing;
            visaVehicleDuration.BindDataContext(c => c.Value, (BorderCrossing m) => m.VisaVehicleDuration);
            visaVehicleDuration.Tag = "Visa Vehicle Duration";
            visaVehicleDuration.Width = 50;

            NumericUpDown visaVehiclePrice = new NumericUpDown();
            visaVehiclePrice.DataContext = borderCrossing;
            visaVehiclePrice.BindDataContext(c => c.Value, (BorderCrossing m) => m.VisaVehiclePrice);
            visaVehiclePrice.Tag = "Visa Vehicle Price";
            visaVehiclePrice.Width = 50;



            CheckBox fumigation = new CheckBox();
            fumigation.DataContext = borderCrossing;
            fumigation.BindDataContext(c => c.Checked, (BorderCrossing m) => m.Fumigation);
            fumigation.Tag = "Fumigation";
            fumigation.Width = 50;

            NumericUpDown fumigationPrice = new NumericUpDown();
            fumigationPrice.DataContext = borderCrossing;
            fumigationPrice.BindDataContext(c => c.Value, (BorderCrossing m) => m.FumigationPrice);
            fumigationPrice.Tag = "Fumigation Price";
            fumigationPrice.Width = 50;



            CheckBox vehicleInspection = new CheckBox();
            vehicleInspection.DataContext = borderCrossing;
            vehicleInspection.BindDataContext(c => c.Checked, (BorderCrossing m) => m.VehicleInspection);
            vehicleInspection.Tag = "Vehicle Inspection";
            vehicleInspection.Width = 50;



            CheckBox tramidores = new CheckBox();
            tramidores.DataContext = borderCrossing;
            tramidores.BindDataContext(c => c.Checked, (BorderCrossing m) => m.Tramidores);
            tramidores.Tag = "Tramidores";
            tramidores.Width = 50;

            NumericUpDown tramidoresPrice = new NumericUpDown();
            tramidoresPrice.DataContext = borderCrossing;
            tramidoresPrice.BindDataContext(c => c.Value, (BorderCrossing m) => m.TramidoresPrice);
            tramidoresPrice.Tag = "Tramidores Price";
            tramidoresPrice.Width = 50;

            TextArea comments = new TextArea();
            comments.DataContext = borderCrossing;
            comments.BindDataContext(c => c.Text, (Bivouac m) => m.Comments);

            var font = new Font("Helvetica", 13, FontStyle.Italic);
            var info = new Label() { Text = "Prices expressed in euros and duration expressed in days.", };
            info.Font = font;

            var row1 = ViewHelper.AppendH(ViewHelper.Labelize(visaDuration, 130), ViewHelper.Labelize(visaVehicleDuration, 130), ViewHelper.Labelize(fumigation, 130), ViewHelper.Labelize(tramidores, 130));
            var row2 = ViewHelper.AppendH(ViewHelper.Labelize(visaPrice, 130), ViewHelper.Labelize(visaVehiclePrice, 130), ViewHelper.Labelize(fumigationPrice, 130), ViewHelper.Labelize(tramidoresPrice, 130));
            var row3 = ViewHelper.AppendH(ViewHelper.Labelize(vehicleInspection, 130), null);

            var layout = ViewHelper.AppendV(new Panel() { Content = info, Padding = new Padding(0, 10) }, row1, row2, row3, new Panel() { Content = comments, Padding = new Padding(0, 10) } );

            return new Panel() { Content = layout, Padding = new Padding(10, 0) };
        }
    }
}
