using System;
using Eto.Forms;
using Eto.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Eto;
using System.Diagnostics;
using Newtonsoft.Json;

namespace iTrip
{
    /// <summary>
    /// Your application's main form
    /// </summary>
    public class MainView : Form
    {
        public MainViewModel MainViewModel { get; set; }
        SelectableFilterCollection<JourneyViewModel> JourneyList { get; set; }
        Process mongoDbProcess { get; set; }

        public MainView()
        {
            ProcessStartInfo mongoDbProcessInfo = new ProcessStartInfo();
            mongoDbProcessInfo.FileName = "/usr/local/Cellar/mongodb/3.4.0/bin/mongod";
            mongoDbProcessInfo.Arguments = "--config /usr/local/etc/mongod.conf";

            mongoDbProcessInfo.UseShellExecute = false;
            mongoDbProcessInfo.CreateNoWindow = true;
            mongoDbProcess = Process.Start(mongoDbProcessInfo);
            //p.WaitForExit();

            MainViewModel = new MainViewModel();

            this.DataContext = MainViewModel;

            Title = "iTrip";
            ClientSize = new Size(1200, 600);

            var gridView = new GridView();
            JourneyList = new SelectableFilterCollection<JourneyViewModel>(gridView, MainViewModel.Journeys);
            gridView.ShowHeader = false;
            gridView.DataStore = JourneyList;
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

            var searchBox = CreateSearchBox(JourneyList);

            var addButton = new Label();
            addButton.Text = "+";
            addButton.Font = new Font(FontFamilies.SansFamilyName, 30);
            addButton.TextColor = Colors.DarkGray;
            addButton.MouseUp += (sender, e) => { AddNewJourney(); ((Label)sender).TextColor = Colors.DarkGray; };
            addButton.MouseDown += (sender, e) => ((Label)sender).TextColor = Colors.Black;

            var deleteButton = new Label();
            deleteButton.Text = "–";
            deleteButton.Font = new Font(FontFamilies.SansFamilyName, 32);
            deleteButton.TextColor = Colors.DarkGray;
            deleteButton.MouseUp += (sender, e) => { DeleteSelectedJourney(); ((Label)sender).TextColor = Colors.DarkGray; };
            deleteButton.MouseDown += (sender, e) => ((Label)sender).TextColor = Colors.Black;

            TableLayout bottomBar = ViewHelper.AppendH(addButton, deleteButton) as TableLayout;
            bottomBar.Padding = new Padding(5, 0);

            var layout = new TableLayout
            {
                Rows =
                {
                    new TableRow(searchBox) { ScaleHeight = false },
                    new TableRow(gridView) { ScaleHeight = true },
                    new TableRow(bottomBar) { ScaleHeight = false },
                    //new TableRow(new TableLayout { Rows = { new TableRow(addButton, deleteButton, null) } } ) { ScaleHeight = false }
                },
                Padding = new Padding(0, 20, 0, 0)
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
                },
                Padding = new Padding(0, 0, 0, 0),
            };  


            Content = mainLayout;
            

            Style = "main";

            var addJourneyCommand = new Command { MenuText = "New Journey", Image = Icon.FromResource("iTrip.Images.AddIcon.png"), ToolBarText = "New Journey", Shortcut = Keys.Application | Keys.N };
            addJourneyCommand.Executed += (sender, e) => AddNewJourney();

            var saveJourneyCommand = new Command { MenuText = "Save Journey", ToolBarText = "Save Journey", Shortcut = Keys.Application | Keys.S };
            saveJourneyCommand.Executed += (sender, e) => MainViewModel.Save();

            var addEventCommand = new Command { MenuText = "Add Event", ToolBarText = "Add Event" };
            addEventCommand.Executed += (sender, e) => MainViewModel.SelectedJourney?.AddEvent();

            var addExpenseCommand = new Command { MenuText = "Add Expense", Image = Icon.FromResource("iTrip.Images.AddExpenseIcon.png"), ToolBarText = "Add Event" };
            addExpenseCommand.Executed += (sender, e) => MainViewModel.SelectedJourney?.AddSpending();

            var displayMapCommand = new Command { MenuText = "Map", ToolBarText = "Map" };
            displayMapCommand.Executed += (sender, e) => DisplayMap();

            var displayReporting = new Command { MenuText = "Reporting", ToolBarText = "Reporting" };
            displayReporting.Executed += (sender, e) => DisplaySummary();

            //var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            //quitCommand.Executed += (sender, e) => Quit();

            var aboutCommand = new Command { MenuText = "About..." };
            aboutCommand.Executed += (sender, e) => MessageBox.Show(this, "iTrip v1.1");

            // create menu
            Menu = new MenuBar
            {
                Items = {
					// File submenu
                    new ButtonMenuItem { Text = "&File", Items = { addJourneyCommand, saveJourneyCommand, addEventCommand, addExpenseCommand } },
                    new ButtonMenuItem { Text = "&View", Items = { displayMapCommand, displayReporting } }
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
            //ToolBar = new ToolBar { Items = { addJourneyCommand, new SeparatorToolItem() { Type = SeparatorToolItemType.FlexibleSpace }, saveJourneyCommand, addEventCommand, addExpenseCommand } };
        }

        private Control GetStatusBar()
        {
            var layout = new TableLayout();
            layout.Height = 24;
            layout.Rows.Add(new TableRow(new Panel() { Height = 1, BackgroundColor = Color.FromArgb(12, 12, 12) }){ ScaleHeight = false });
            layout.Rows.Add(new TableRow(new Label() { Text = "Ready", TextColor = Color.FromArgb(215, 125, 69) }){ ScaleHeight = true });
            return layout;
        }

        private void DisplaySummary()
        {
            Dialog summaryWindow = new Dialog();
            summaryWindow.Height = 500;
            summaryWindow.Width = 600;


            Control reporting = ReportingView.GetView(JourneyList.SelectedItems.ToList().OrderBy(x => x.Journey.FromDateTime).ToList());

            Button closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Click += (sender, e) => summaryWindow.Close();


            summaryWindow.Content = ViewHelper.AppendV(reporting, ViewHelper.AppendV(null, ViewHelper.AppendH(null, closeButton, null), null));
            summaryWindow.DisplayMode = DialogDisplayMode.Attached;
            summaryWindow.ShowModal(this);
        }

        private void DisplayMap()
        {
            List<string> wayPoints = JourneyList.SelectedItems.ToList().OrderBy(x => x.Journey.FromDateTime).SelectMany(x => x.GetWayPoints()).ToList();
            List<DirectionRequest> directionRequests = MapHelper.GetDirectionRequests(wayPoints);

            var jsonDirectionRequests = JsonConvert.SerializeObject(directionRequests);


            string script = "calculateAndDisplayRoute(" + jsonDirectionRequests + ");";
            Dialog mapWindow = new Dialog();

            WebView webView = new WebView();
            webView.Url = MapHelper.GoogleMapUrl();
            webView.Size = new Size(800, 500);
            //webView.LoadComplete += (sender, e) => ((WebView)sender).ExecuteScript(script);

            Button loadButton = new Button();
            loadButton.Text = "Load";
            loadButton.Click += (sender, e) => webView.ExecuteScript(script);

            Button exportButton = new Button();
            exportButton.Text = "Export";
            exportButton.Click += (sender, e) => webView.ExecuteScript("displayLastResponse()");

            Button closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Click += (sender, e) => mapWindow.Close();

            mapWindow.Content = ViewHelper.AppendV(webView, ViewHelper.AppendH(loadButton, exportButton, closeButton, null));
            mapWindow.DisplayMode = DialogDisplayMode.Attached;
            mapWindow.ShowModal(this);
            
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (MainViewModel.IsAnythingToSave())
            {
                DialogResult result = MessageBox.Show(this, "It Seems that there is unsaved journey... Are you sure you want to quit?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
                if (result == DialogResult.No) { e.Cancel = true; mongoDbProcess.Kill(); }
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
