using System;
using Eto.Forms;

namespace iTrip
{
    public static class ViewHelper
    {
        public static Control Labelize(Control control, int labelWidth = 80)
        {
            TableLayout layout = new TableLayout();

            Label label = new Label();
            label.VerticalAlignment = VerticalAlignment.Center;
            label.Text = control.Tag + ": ";
            label.Width = labelWidth;
            if (control.Width == -1) { control.Width = 170;}

            var row = new TableRow();
            row.Cells.Add(label);
            row.Cells.Add(control);
            row.Cells.Add(new Label() { Width = 20 });

            layout.Rows.Add(row);

            return layout;
        }

        public static Control AppendH(params Control[] controls)
        {
            TableLayout layout = new TableLayout();
            var row = new TableRow();

            foreach (var control in controls)
            {
                row.Cells.Add(control);
            }

            layout.Rows.Add(row);

            return layout;
        }

        public static Control AppendV(params Control[] controls)
        {
            TableLayout layout = new TableLayout();

            foreach (var control in controls)
            {
                layout.Rows.Add(new TableRow(control));
            }

            return layout;
        }
    }
}
