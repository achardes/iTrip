using System;
using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;

namespace iTrip
{
    public class BivouacView
    {
        public static Control GetView(Bivouac bivouac)
        {
            ComboBox typeDropDown = new ComboBox();
            typeDropDown.DataContext = bivouac;
            typeDropDown.DataStore = ConstantManager.Instance.BivouacTypes;
            typeDropDown.BindDataContext(c => c.SelectedKey, (Bivouac m) => m.Type);
            typeDropDown.Tag = "Type";

            ComboBox noteDropDown = new ComboBox();
            noteDropDown.DataContext = bivouac;
            noteDropDown.DataStore = ConstantManager.Instance.Notes;
            noteDropDown.BindDataContext(c => c.SelectedKey, (Bivouac m) => m.Note);
            noteDropDown.TextColor = Colors.Gold;
            noteDropDown.Tag = "Note";

            TextBox addressTextBox = new TextBox();
            addressTextBox.DataContext = bivouac;
            addressTextBox.BindDataContext(c => c.Text, (Bivouac m) => m.Address);
            addressTextBox.Tag = "Address";

            TextBox cityTextBox = new TextBox();
            cityTextBox.DataContext = bivouac;
            cityTextBox.BindDataContext(c => c.Text, (Bivouac m) => m.City);
            cityTextBox.Tag = "City";

            ComboBox countryDropDown = new ComboBox();
            countryDropDown.DataContext = bivouac;
            countryDropDown.DataStore = ConstantManager.Instance.Countries;
            countryDropDown.BindDataContext(c => c.SelectedKey, (Bivouac m) => m.Country);
            countryDropDown.Tag = "Country";

            TextBox latitudeTextBox = new TextBox();
            latitudeTextBox.DataContext = bivouac;
            latitudeTextBox.Width = 100;
            latitudeTextBox.TextBinding.BindDataContext(
                Binding.Property((Bivouac b) => b.Latitude)
                .Convert(r => r.ToString(), v => Converters.FromStringToDouble(v))
            );
            latitudeTextBox.Tag = "Latitude";

            TextBox longitudeTextBox = new TextBox();
            longitudeTextBox.DataContext = bivouac;
            longitudeTextBox.Width = 100;
            longitudeTextBox.TextBinding.BindDataContext(
                Binding.Property((Bivouac b) => b.Longitude)
                .Convert(r => r.ToString(), v => Converters.FromStringToDouble(v))
            );
            longitudeTextBox.Tag = "Longitude";

            NumericUpDown elevationNumericUpDown = new NumericUpDown();
            elevationNumericUpDown.DataContext = bivouac;
            elevationNumericUpDown.BindDataContext(c => c.Value, (Bivouac m) => m.Elevation);
            elevationNumericUpDown.Tag = "Elevation";

            NumericUpDown distanceTrackNumericUpDown = new NumericUpDown();
            distanceTrackNumericUpDown.DataContext = bivouac;
            distanceTrackNumericUpDown.BindDataContext(c => c.Value, (Bivouac m) => m.DistanceTrack);
            distanceTrackNumericUpDown.Tag = "DistanceTrack";

            NumericUpDown distanceNumericUpDown = new NumericUpDown();
            distanceNumericUpDown.DataContext = bivouac;
            distanceNumericUpDown.BindDataContext(c => c.Value, (Bivouac m) => m.Distance);
            distanceNumericUpDown.Tag = "DistanceTrack";

            NumericUpDown wakeUpTemperatureNumericUpDown = new NumericUpDown();
            wakeUpTemperatureNumericUpDown.DataContext = bivouac;
            wakeUpTemperatureNumericUpDown.BindDataContext(c => c.Value, (Bivouac m) => m.WakeUpTemperature);
            wakeUpTemperatureNumericUpDown.Tag = "WakeUpTemperature";

            CheckBox photoCheckBox = new CheckBox();
            photoCheckBox.DataContext = bivouac;
            photoCheckBox.BindDataContext(c => c.Checked, (Bivouac m) => m.Photo);
            photoCheckBox.Tag = "Photo";

            TextArea textArea = new TextArea();
            textArea.DataContext = bivouac;
            textArea.BindDataContext(c => c.Text, (Bivouac m) => m.Comments);

            GridView tagsGrid = new GridView();
            tagsGrid.DataStore = bivouac.GetBivouacTags();
            tagsGrid.ShowHeader = false;

            tagsGrid.Columns.Add(new GridColumn
            {
                DataCell = new CheckBoxCell { Binding = Binding.Property<Tag, bool?>(r => r.IsChecked) },
                Editable = true
            });

            tagsGrid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Tag, string>(r => r.Name) },
                Editable = false
            });

            TableLayout layout = new TableLayout();
            layout.Rows.Add(new TableRow(ViewHelper.AppendH(ViewHelper.AddLabelToControl(typeDropDown), ViewHelper.AddLabelToControl(noteDropDown), null)));
            layout.Rows.Add(new TableRow(ViewHelper.AppendH(ViewHelper.AddLabelToControl(addressTextBox), ViewHelper.AddLabelToControl(cityTextBox), ViewHelper.AddLabelToControl(countryDropDown), null)));
            layout.Rows.Add(new TableRow(ViewHelper.AppendH(ViewHelper.AddLabelToControl(elevationNumericUpDown), ViewHelper.AddLabelToControl(distanceNumericUpDown), ViewHelper.AddLabelToControl(distanceTrackNumericUpDown), null)));
            layout.Rows.Add(new TableRow(ViewHelper.AppendH(ViewHelper.AddLabelToControl(latitudeTextBox), ViewHelper.AddLabelToControl(longitudeTextBox), null)));
            layout.Rows.Add(new TableRow(ViewHelper.AppendH(ViewHelper.AddLabelToControl(wakeUpTemperatureNumericUpDown), ViewHelper.AddLabelToControl(photoCheckBox), null)));
            layout.Rows.Add(textArea);

            return new GroupBox() { Padding = 5, Content = ViewHelper.AppendH(layout, tagsGrid), Text = "Bivouac"};
        }
    }
}
