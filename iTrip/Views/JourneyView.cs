using System;
using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;
using Newtonsoft.Json;

namespace iTrip
{
    public static class JourneyView
    {
        public static Control GetView(JourneyViewModel journeyViewModel)
        {
            var webView = GetWebView(journeyViewModel);

            TabControl tabControl = new TabControl();
            tabControl.Pages.Add(new TabPage(EventListView.GetView(journeyViewModel)) { Text = "Events" });
            tabControl.Pages.Add(new TabPage(SpendingListView.GetView(journeyViewModel)) { Text = "Expenses" });
            tabControl.Pages.Add(new TabPage(webView) { Text = "Map" });

            var borderCrossingPage = new TabPage();
            borderCrossingPage.Text = "Border Crossing";
            borderCrossingPage.DataContext = journeyViewModel.Journey;
            borderCrossingPage.BindDataContext(c => c.Enabled, (Journey m) => m.IncludeBorderCrossing);
            borderCrossingPage.EnabledChanged += (sender, e) =>
            {
                if(tabControl.SelectedIndex == 3) tabControl.SelectedIndex = 0;
            };
            borderCrossingPage.Content = BorderCrossingView.GetView(journeyViewModel.Journey.BorderCrossing);

            tabControl.Pages.Add(borderCrossingPage);

            tabControl.SelectedIndexChanged += (sender, e) =>
            {
                if ((sender as TabControl).SelectedPage.Text == "Map")
                {
                    List<string> wayPoints = journeyViewModel.GetWayPoints();
                    List<DirectionRequest> directionRequests = MapHelper.GetDirectionRequests(wayPoints);

                    var jsonDirectionRequests = JsonConvert.SerializeObject(directionRequests);
                    string script = "calculateAndDisplayRoute(" + jsonDirectionRequests + ");";

                    webView.ExecuteScript(script);
                }
            };

            //TableLayout layoutGrids = new TableLayout();
            //layoutGrids.Rows.Add(new TableRow(EventListView.GetView(journeyViewModel)) { ScaleHeight = true });
            //layoutGrids.Rows.Add(new TableRow(SpendingListView.GetView(journeyViewModel)) { ScaleHeight = true });

            //var button = new Button();
            //button.Text = "Update map";
            //button.Click += (sender, e) => { webView.ExecuteScript(journeyViewModel.GoogleMapParameters); };

            TableLayout layout = ViewHelper.AppendV(
                GetTitleBar(journeyViewModel.Journey),
                ViewHelper.AppendH(
                    ViewHelper.AppendV(
                        ViewHelper.AppendH(
                            GetWeatherDropDown(journeyViewModel.Journey), 
                            GetNoteSlider(journeyViewModel.Journey),
                            GetIncludeBorderCrossing(journeyViewModel.Journey),
                            null
                        ),
                        BivouacView.GetView(journeyViewModel.Journey.Bivouac)
                    )
                ), 
                tabControl
            ) as TableLayout;
            layout.Padding = new Padding(10, 0, 10, 10);
            layout.BackgroundColor = Colors.White;

            return layout;
        }

        private static WebView GetWebView(JourneyViewModel journeyViewModel)
        {
            WebView webView = new WebView();
            webView.Url = MapHelper.GoogleMapUrl();
            webView.Size = new Size(500, 300);
            return webView;
        }

        private static Control GetIncludeBorderCrossing(Journey journey)
        {
            var checkbox = new CheckBox();
            checkbox.DataContext = journey;
            checkbox.BindDataContext(c => c.Checked, (Journey m) => m.IncludeBorderCrossing);
            checkbox.Tag = "Include a border crossing";

            return ViewHelper.Labelize(checkbox, 160);
        }

        private static Control GetTitleBar(Journey journey)
        {
            var labelTitle = new Label();
            labelTitle.DataContext = journey;
            labelTitle.BindDataContext(c => c.Text, (Journey m) => m.DisplayName);
            labelTitle.TextColor = Color.FromArgb(100, 100, 100);
            labelTitle.Font = new Font("Helvetica", 13);

            var label = new Label();
            label.DataContext = journey;
            label.BindDataContext(c => c.Visible, (Journey m) => m.HasBeenChanged);
            label.Text = " — Edited";
            label.TextColor = Colors.DarkGray;
            label.Font = new Font("Helvetica", 13);

            var layout = new TableLayout
            {
                Padding = new Padding(0, 3, 0, 10),
                Rows =
                {
                    new TableRow(new TableLayout() { Rows = { new TableRow (null, labelTitle, label, null) } } ) { ScaleHeight = false }      
                }
            };

            return layout;
        }

        private static Control GetWeatherDropDown(Journey journey)
        {
            ComboBox weatherDropDown = new ComboBox();
            weatherDropDown.DataContext = journey;
            weatherDropDown.AutoComplete = true;
            weatherDropDown.DataStore = ConstantManager.Instance.WeatherKinds;
            weatherDropDown.BindDataContext(c => c.SelectedValue, (Journey m) => m.Weather);
            weatherDropDown.Tag = "Weather";

            return ViewHelper.Labelize(weatherDropDown);
        }

        private static Control GetNoteSlider(Journey journey)
        {
            ComboBox noteDropDown = new ComboBox();
            noteDropDown.DataContext = journey;
            noteDropDown.DataStore = ConstantManager.Instance.Notes;
            noteDropDown.BindDataContext(c => c.SelectedKey, (Journey m) => m.Note);
            //noteDropDown.TextColor = Colors.Gold;
            noteDropDown.Tag = "Note";


            return ViewHelper.Labelize(noteDropDown);
        }
    }
}
