using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Eto.Drawing;
using Eto.Forms;

namespace iTrip
{
    public static class SpendingListView
    {
        public static Control GetView(JourneyViewModel journeyViewModel)
        {
            var label = new Label();
            label.Text = "Spendings";
            label.VerticalAlignment = VerticalAlignment.Center;

            var addButton = new Button();
            addButton.Text = "Add";
            addButton.Click += (sender, e) => journeyViewModel.AddSpending();

            var grid = new GridView { DataStore = journeyViewModel.Journey.Spendings };
            grid.AllowColumnReordering = true;
            grid.CanDeleteItem = s => true;

            grid.Columns.Add(new GridColumn
            {
                DataCell = new ComboBoxCell { Binding = Binding.Property<Spending, object>(r => r.Type), DataStore = ConstantManager.Instance.SpendingTypes },
                HeaderText = "Type",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

            grid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Spending, double>(r => r.Price).Convert(r => r.ToString(), v => Converters.FromStringToDouble(v)) },
                HeaderText = "Price",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

            grid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Spending, double>(r => r.Quantity).Convert(r => r.ToString(), v => Converters.FromStringToDouble(v)) },
                HeaderText = "Quantity",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

            grid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Event, string>(r => r.Comments) },
                HeaderText = "Comments",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

            var layout = new TableLayout
            {
                Rows =
                {
                    new TableRow(new TableLayout() { Rows = { new TableRow (label, addButton, null) } } ) { ScaleHeight = false },
                    new TableRow(grid) { ScaleHeight = false }
                }
            };

            return grid;
        }
    }
}
