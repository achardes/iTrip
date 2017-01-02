using System;
using Eto.Forms;

namespace iTrip
{
    public static class ViewHelper
    {
        public static Control AddLabelToControl(Control control)
        {
            TableLayout layout = new TableLayout();

            Label label = new Label();
            label.VerticalAlignment = VerticalAlignment.Center;
            label.Text = control.Tag + ": ";
            label.Width = 80;
            control.Width = 100;

            var row = new TableRow();
            row.Cells.Add(label);
            row.Cells.Add(control);

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
