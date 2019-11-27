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

            TextBox coordinatesTextBox = new TextBox();
            coordinatesTextBox.DataContext = bivouac;
            coordinatesTextBox.BindDataContext(c => c.Text, (Bivouac m) => m.Coordinates);
            coordinatesTextBox.Tag = "Lat, Long";

            NumericStepper elevationNumericUpDown = new NumericStepper();
            elevationNumericUpDown.DataContext = bivouac;
            elevationNumericUpDown.BindDataContext(c => c.Value, (Bivouac m) => m.Elevation);
            elevationNumericUpDown.Tag = "Height (m)";

            NumericStepper distanceNumericUpDown = new NumericStepper();
            distanceNumericUpDown.DataContext = bivouac;
            distanceNumericUpDown.BindDataContext(c => c.Value, (Bivouac m) => m.Distance);
            distanceNumericUpDown.Tag = "Total (km)";

            NumericStepper distanceTrackNumericUpDown = new NumericStepper();
            distanceTrackNumericUpDown.DataContext = bivouac;
            distanceTrackNumericUpDown.BindDataContext(c => c.Value, (Bivouac m) => m.DistanceTrack);
            distanceTrackNumericUpDown.Tag = "Track (km)";

            NumericStepper walkNumericUpDown = new NumericStepper();
            walkNumericUpDown.DataContext = bivouac;
            walkNumericUpDown.BindDataContext(c => c.Value, (Bivouac m) => m.Walk);
            walkNumericUpDown.Tag = "Walk (m)";

            NumericStepper wakeUpTemperatureNumericUpDown = new NumericStepper();
            wakeUpTemperatureNumericUpDown.DataContext = bivouac;
            wakeUpTemperatureNumericUpDown.BindDataContext(c => c.Value, (Bivouac m) => m.WakeUpTemperature);
            wakeUpTemperatureNumericUpDown.Tag = "Wake T (C°)";

            CheckBox photoCheckBox = new CheckBox();
            photoCheckBox.DataContext = bivouac;
            photoCheckBox.BindDataContext(c => c.Checked, (Bivouac m) => m.Photo);
            photoCheckBox.Tag = "Photo";
            photoCheckBox.Width = 14;

            CheckBox fromiOverLanderCheckBox = new CheckBox();
            fromiOverLanderCheckBox.DataContext = bivouac;
            fromiOverLanderCheckBox.BindDataContext(c => c.Checked, (Bivouac m) => m.FromIOverLander);
            fromiOverLanderCheckBox.Tag = "iOverLander";
            fromiOverLanderCheckBox.Width = 14;

            CheckBox toiOverLanderCheckBox = new CheckBox();
            toiOverLanderCheckBox.DataContext = bivouac;
            toiOverLanderCheckBox.BindDataContext(c => c.Checked, (Bivouac m) => m.ToIOverLander);
            toiOverLanderCheckBox.Tag = "Shared";
            toiOverLanderCheckBox.Width = 14;

            TextArea commentTextArea = new TextArea();
            commentTextArea.DataContext = bivouac;
            commentTextArea.BindDataContext(c => c.Text, (Bivouac m) => m.Comments);

            GridView tagsGrid = new GridView();
            tagsGrid.DataStore = bivouac.GetBivouacTags();
            tagsGrid.ShowHeader = false;
            tagsGrid.Width = 170;

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

            var infoColumn = ViewHelper.AppendV(
                ViewHelper.AppendH(ViewHelper.Labelize(typeDropDown), ViewHelper.Labelize(addressTextBox)),
                ViewHelper.AppendH(ViewHelper.Labelize(noteDropDown), ViewHelper.Labelize(cityTextBox), ViewHelper.Labelize(elevationNumericUpDown)),
                ViewHelper.AppendH(ViewHelper.Labelize(wakeUpTemperatureNumericUpDown), ViewHelper.Labelize(countryDropDown)),
                ViewHelper.AppendH(ViewHelper.Labelize(distanceNumericUpDown), ViewHelper.Labelize(coordinatesTextBox)),
                ViewHelper.AppendH(ViewHelper.Labelize(distanceTrackNumericUpDown), ViewHelper.Labelize(elevationNumericUpDown)),
                ViewHelper.AppendH(ViewHelper.Labelize(walkNumericUpDown), ViewHelper.Labelize(photoCheckBox, 42), ViewHelper.Labelize(fromiOverLanderCheckBox, 79), ViewHelper.Labelize(toiOverLanderCheckBox, 49))
             );

            return new GroupBox() { Padding = 5, Content = ViewHelper.AppendH(infoColumn, tagsGrid, new Label() { Width = 20 }, commentTextArea), Text = "Bivouac"};
        }
    }
}
