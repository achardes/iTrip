using System;
using Eto.Forms;
using Eto.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Eto;

namespace iTrip
{
    /// <summary>
    /// Your application's main form
    /// </summary>
    public class MainView : Form
    {
        public MainViewModel MainViewModel { get; set; }

        public MainView()
        {
            MainViewModel = new MainViewModel();

            this.DataContext = MainViewModel;

            Title = "iTrip";
            ClientSize = new Size(1200, 600);

            var gridView = new GridView();
            SelectableFilterCollection<JourneyViewModel> journeyList = new SelectableFilterCollection<JourneyViewModel>(gridView, MainViewModel.Journeys);
            gridView.ShowHeader = false;
            gridView.DataStore = journeyList;
            gridView.SelectedItemBinding.BindDataContext((MainViewModel m) => m.SelectedJourney);
            gridView.ContextMenu = CreateContextMenu();
            gridView.AllowMultipleSelection = true;
            gridView.Style = "journeyList";

            gridView.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<JourneyViewModel, string>(r => r.Text) },
                HeaderText = "Name",
                Editable = false,
                Resizable = false,
                Sortable = false,
            });

            var searchBox = CreateSearchBox(journeyList);

            var addButton = new Button();
            addButton.Text = "+";
            addButton.Click += (sender, e) => AddNewJourney();

            var deleteButton = new Button();
            deleteButton.Text = "-";
            deleteButton.Click += (sender, e) => DeleteSelectedJourney();
            deleteButton.BindDataContext(c => c.Enabled, (MainViewModel m) => m.HasSelectedJourney);

            var layout = new TableLayout
            {
                Rows =
                {
                    new TableRow(searchBox) { ScaleHeight = false },
                    new TableRow(gridView) { ScaleHeight = true },
                    //new TableRow(new TableLayout { Rows = { new TableRow(addButton, deleteButton, null) } } ) { ScaleHeight = false }
                }
            };



            var splitter = new Splitter
            {
                Orientation = Orientation.Horizontal,
                Position = 200,
                FixedPanel = SplitterFixedPanel.Panel1,
                Panel1 = layout,
            };

            splitter.BindDataContext(c => c.Panel2, (MainViewModel m) => m.SelectedJourneyControl);


            var mainLayout = new TableLayout
            {
                Rows =
                {
                    new TableRow(splitter) { ScaleHeight = true },
                    //new TableRow(GetStatusBar()) { ScaleHeight = false }
                }
            };


            Content = mainLayout;
            

            Style = "main";

            var addJourneyCommand = new Command { MenuText = "New Journey", Image = Icon.FromResource("iTrip.Images.AddIcon.png"), ToolBarText = "New Journey", Shortcut = Keys.Application | Keys.N };
            addJourneyCommand.Executed += (sender, e) => AddNewJourney();

            var saveJourneyCommand = new Command { MenuText = "Save Journey", ToolBarText = "Save Journey", Shortcut = Keys.Application | Keys.S };
            saveJourneyCommand.Executed += (sender, e) => MainViewModel.Save();

            var addEventCommand = new Command { MenuText = "Add Event", ToolBarText = "Add Event" };
            addEventCommand.Executed += (sender, e) => MainViewModel.SelectedJourney?.AddEvent();

            var addExpenseCommand = new Command { MenuText = "Add Event", Image = Icon.FromResource("iTrip.Images.AddExpenseIcon.png"), ToolBarText = "Add Event" };
            addExpenseCommand.Executed += (sender, e) => MainViewModel.SelectedJourney?.AddSpending();



            //var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            //quitCommand.Executed += (sender, e) => Quit();

            var aboutCommand = new Command { MenuText = "About..." };
            aboutCommand.Executed += (sender, e) => MessageBox.Show(this, "About my app...");

            // create menu
            Menu = new MenuBar
            {
                Items = {
					// File submenu
                    new ButtonMenuItem { Text = "&File", Items = { addJourneyCommand, saveJourneyCommand, addEventCommand, addExpenseCommand } }
					// new ButtonMenuItem { Text = "&Edit", Items = { /* commands/items */ } },
					// new ButtonMenuItem { Text = "&View", Items = { /* commands/items */ } },
				},
                ApplicationItems = {
					// application (OS X) or file menu (others)
					new ButtonMenuItem { Text = "&Preferences..." },
                },
                //QuitItem = quitCommand,
                AboutItem = aboutCommand
            };

            // create toolbar			
            ToolBar = new ToolBar { Items = { addJourneyCommand, new SeparatorToolItem() { Type = SeparatorToolItemType.FlexibleSpace }, saveJourneyCommand, addEventCommand, addExpenseCommand } };
        }

        private Control GetStatusBar()
        {
            var layout = new TableLayout();
            layout.Height = 24;
            layout.Rows.Add(new TableRow(new Panel() { Height = 1, BackgroundColor = Color.FromArgb(12, 12, 12) }){ ScaleHeight = false });
            layout.Rows.Add(new TableRow(new Label() { Text = "Ready", TextColor = Color.FromArgb(215, 125, 69) }){ ScaleHeight = true });
            return layout;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (MainViewModel.IsAnythingToSave())
            {
                DialogResult result = MessageBox.Show(this, "It Seems that there is unsaved journey... Are you sure you want to quit?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
                if (result == DialogResult.No) { e.Cancel = true; }
            }
        }

        void DeleteSelectedJourney()
        {
            if (MainViewModel.SelectedJourney != null)
            {
                DialogResult result = MessageBox.Show(this, string.Format("Are you sure you want to delete the journey of {0}", MainViewModel.SelectedJourney.Text), MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
                if (result == DialogResult.Yes) { MainViewModel.Delete(); }
            }
        }

        void AddNewJourney()
        {
            var dialog = new Dialog();
            dialog.DisplayMode = DialogDisplayMode.Attached;

            var layout = new DynamicLayout { DefaultSpacing = new Size(5, 5) };

            layout.AddCentered(new Label { Text = "Select the period of the new journey:" }, yscale: true);

            var dateTimePicker = new Calendar();
            dateTimePicker.SelectedDate = DateTime.Now;
            dateTimePicker.Mode = CalendarMode.Range;

            layout.AddCentered(dateTimePicker);

            dialog.DefaultButton = new Button { Text = "Add" };
            dialog.AbortButton = new Button { Text = "Cancel" };

            dialog.DefaultButton.Click += delegate
            {   
                MainViewModel.Add(dateTimePicker.SelectedRange.Start, dateTimePicker.SelectedRange.End);
                dialog.Close();
            };

            dialog.AbortButton.Click += delegate
            {
                dialog.Close();
            };

            layout.BeginVertical();
            layout.AddRow(null, dialog.DefaultButton, dialog.AbortButton);
            layout.EndVertical();

            dialog.Content = layout;

            dialog.ShowModal(this);
        }

        Control CreateSearchBox(SelectableFilterCollection<JourneyViewModel> filtered)
        {
            var filterText = new SearchBox { PlaceholderText = "Filter" };
            filterText.TextChanged += (s, e) =>
            {
                var filterItems = (filterText.Text ?? "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (filterItems.Length == 0)
                    filtered.Filter = null;
                else
                    filtered.Filter = i =>
                {
                    // Every item in the split filter string should be within the Text property
                    foreach (var filterItem in filterItems)
                    {
                        if (i.TextDescription.IndexOf(filterItem, StringComparison.OrdinalIgnoreCase) == -1)
                        {
                            return false;
                        }
                    }
                    return true;
                };
            };

            var panel = new Panel();
            panel.Content = filterText;
            panel.Padding = new Padding(5, 5);

            return panel;
        }

        ContextMenu CreateContextMenu()
        {
            var menu = new ContextMenu();

            var deleteItem = new ButtonMenuItem { Text = "Delete" };
            deleteItem.Click += (s, e) => 
            { 
                DeleteSelectedJourney(); 
            };
            menu.Items.Add(deleteItem);

            return menu;
        }
    }
}
