using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Eto.Drawing;
using Eto.Forms;

namespace iTrip
{
    public static class EventListView
    {
        public static Control GetView(JourneyViewModel journeyViewModel)
        {
            var grid = new GridView { DataStore = journeyViewModel.Journey.Events };
            grid.DataContext = journeyViewModel;
            grid.ContextMenu = CreateContextMenu(journeyViewModel);
            grid.AllowColumnReordering = true;
            grid.CanDeleteItem = s => true;
            grid.SelectedItemBinding.BindDataContext((JourneyViewModel m) => m.SelectedEvent);

            grid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Event, int>(r => r.Order).Convert(r => r.ToString(), v => Converters.FromStringToInt(v)) },
                HeaderText = "Order",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

            grid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Event, string>(r => r.Name) },
                HeaderText = "Name",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

            grid.Columns.Add(new GridColumn
            {
                DataCell = new ComboBoxCell { Binding = Binding.Property<Event, object>(r => r.Type), DataStore = ConstantManager.Instance.EventTypes },
                HeaderText = "Type",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

            grid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Event, string>(r => r.Price) },
                HeaderText = "Price",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

            grid.Columns.Add(new GridColumn
            {
                DataCell = new ComboBoxCell { DataStore = ConstantManager.Instance.Notes, Binding = Binding.Property<Event, object>(r => r.Note) },
                HeaderText = "Note",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

            grid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Event, string>(r => r.Duration) },
                HeaderText = "Duration",
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

            grid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Event, string>(r => r.Address) },
                HeaderText = "Address",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

            grid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Event, string>(r => r.City) },
                HeaderText = "City",
                Editable = true,
                Resizable = true,
                Sortable = true
            });

            grid.Columns.Add(new GridColumn
            {
                DataCell = new ComboBoxCell { DataStore = ConstantManager.Instance.Countries, Binding = Binding.Property<Event, object>(r => r.Country) },
                HeaderText = "Country",
                Editable = true,
                Resizable = true,
                Sortable = true,
                Width = 120,
                AutoSize = false
            });

            grid.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Event, string>(r => r.Coordinates) },
                HeaderText = "Lat, Long",
                Editable = true,
                Resizable = true,
                Sortable = true,
                Width = 200,
                AutoSize = false
            });

            return grid;
        }

        static ContextMenu CreateContextMenu(JourneyViewModel journeyViewModel)
        {
            var menu = new ContextMenu();

            var deleteItem = new ButtonMenuItem { Text = "Delete" };
            deleteItem.Click += (s, e) => { journeyViewModel.DeleteEvent(); };
            deleteItem.DataContext = journeyViewModel;
            deleteItem.BindDataContext(c => c.Enabled, (JourneyViewModel m) => m.HasSelectedEvent);

            var addItem = new ButtonMenuItem { Text = "Add" };
            addItem.Click += (s, e) => { journeyViewModel.AddEvent(); };

            menu.Items.Add(deleteItem);
            menu.Items.Add(addItem);

            return menu;
        }
    }
}
