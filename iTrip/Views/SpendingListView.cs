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
            var grid = new GridView { DataStore = journeyViewModel.Journey.Spendings };
            grid.DataContext = journeyViewModel;
            grid.AllowColumnReordering = true;
            grid.CanDeleteItem = s => true;
            grid.ContextMenu = CreateContextMenu(journeyViewModel);
            grid.SelectedItemBinding.BindDataContext((JourneyViewModel m) => m.SelectedSpending);

            grid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Spending, int>(r => r.Order).Convert(r => r.ToString(), v => Converters.FromStringToInt(v)) },
                HeaderText = "Order",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

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
                DataCell = new TextBoxCell { Binding = Binding.Property<Spending, double>(r => r.Euro).Convert(r => r.ToString(), v => Converters.FromStringToDouble(v)) },
                HeaderText = "Euro",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

            grid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Spending, double>(r => r.UnitaryPrice).Convert(r => r.ToString(), v => Converters.FromStringToDouble(v)) },
                HeaderText = "UnitaryPrice",
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
                DataCell = new TextBoxCell { Binding = Binding.Property<Spending, string>(r => r.Coordinates) },
                HeaderText = "Lat, Long",
                Editable = true,
                Resizable = true,
                Sortable = true,
                Width = 200,
                AutoSize = false
            });

            grid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Event, string>(r => r.Comments) },
                HeaderText = "Comments",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

            return grid;
        }

        static ContextMenu CreateContextMenu(JourneyViewModel journeyViewModel)
        {
            var menu = new ContextMenu();

            var deleteItem = new ButtonMenuItem { Text = "Delete" };
            deleteItem.Click += (s, e) => { journeyViewModel.DeleteSpending(); };
            deleteItem.DataContext = journeyViewModel;
            deleteItem.BindDataContext(c => c.Enabled, (JourneyViewModel m) => m.HasSelectedSpending);

            var addItem = new ButtonMenuItem { Text = "Add" };
            addItem.Click += (s, e) => { journeyViewModel.AddSpending(); };

            menu.Items.Add(deleteItem);
            menu.Items.Add(addItem);

            return menu;
        }
    }
}
