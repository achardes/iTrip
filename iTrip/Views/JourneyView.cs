using System;
using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;

namespace iTrip
{
    public static class JourneyView
    {
        public static Control GetView(JourneyViewModel journeyViewModel)
        {
            var checkBox = new CheckBox();
            checkBox.DataContext = journeyViewModel.Journey;
            checkBox.BindDataContext(c => c.Checked, (Journey m) => m.HasBeenChanged);

            DynamicLayout layout = new DynamicLayout();
            layout.BeginVertical();
            layout.AddRow(ViewHelper.AppendH(GetTitleBar(journeyViewModel.Journey), GetWeatherDropDown(journeyViewModel.Journey), GetNoteSlider(journeyViewModel.Journey)));

            //layout.AddRow(ViewHelper.AppendH(BivouacView.GetView(journeyViewModel.Journey.Bivouac), webView));


            TableLayout layoutGrids = new TableLayout();
            layoutGrids.Rows.Add(new TableRow(EventListView.GetView(journeyViewModel)) { ScaleHeight = true });
            layoutGrids.Rows.Add(new TableRow(SpendingListView.GetView(journeyViewModel)) { ScaleHeight = true });

            var webView = GetWebView(journeyViewModel);

            var button = new Button();
            button.Text = "Update map";
            button.Click += (sender, e) => { webView.ExecuteScript(journeyViewModel.GoogleMapParameters); };

            TableLayout plop = ViewHelper.AppendV(
                ViewHelper.AppendH(
                    ViewHelper.AppendV(
                        GetTitleBar(journeyViewModel.Journey), 
                        ViewHelper.AppendH(
                            GetWeatherDropDown(journeyViewModel.Journey), 
                            GetNoteSlider(journeyViewModel.Journey),
                            button,
                            null
                        ),
                        BivouacView.GetView(journeyViewModel.Journey.Bivouac)
                    ), 
                    webView
                ), 
                layoutGrids
            ) as TableLayout;
            plop.Padding = new Padding(10, 10);



            layout.AddRow(layoutGrids);
            layout.EndVertical();
            //layout.BackgroundColor = Color.FromArgb(26, 26, 26);
            layout.Padding = new Padding(10, 10);
            return plop;
        }

        private static WebView GetWebView(JourneyViewModel journeyViewModel)
        {
            WebView webView = new WebView();
            webView.DataContext = journeyViewModel;
            webView.BindDataContext(c => c.Url, (JourneyViewModel m) => m.GoogleMapUrl);
            webView.Size = new Size(500, 300);
            webView.DocumentLoaded += (sender, e) => { Console.WriteLine("SCRIPT"); ((WebView)sender).ExecuteScript("javascript:document.getElementById(\"panel\").style.visibility=\"hidden\";document.getElementById(\"map\").style.marginLeft=\"0\";void(0);"); };
            return webView;
        }

        private static Control GetTitleBar(Journey journey)
        {
            var label = new Label();
            label.DataContext = journey;
            label.BindDataContext(c => c.Visible, (Journey m) => m.HasBeenChanged);
            label.Text = "*";
            label.TextColor = Color.FromArgb(41, 160, 245);
            label.Font = new Font("Helvetica", 24);

            var labelTitle = new Label();
            labelTitle.DataContext = journey;
            labelTitle.BindDataContext(c => c.Text, (Journey m) => m.DisplayName);
            labelTitle.TextColor = Color.FromArgb(41, 160, 245);
            labelTitle.Font = new Font("Helvetica", 24);

            var layout = new TableLayout
            {
                Padding = new Padding(0, 0, 0, 20),
                Rows =
                {
                    new TableRow(new TableLayout() { Rows = { new TableRow (label, labelTitle, null) } } ) { ScaleHeight = false }      
                }
            };

            return layout;
        }

        private static Control GetWeatherDropDown(Journey journey)
        {
            Panel panel = new Panel();
            panel.Height = 10;

            ComboBox weatherDropDown = new ComboBox();
            weatherDropDown.DataContext = journey;
            weatherDropDown.AutoComplete = true;
            weatherDropDown.DataStore = ConstantManager.Instance.WeatherKinds;
            weatherDropDown.BindDataContext(c => c.SelectedValue, (Journey m) => m.Weather);
            weatherDropDown.Tag = "Weather";

            panel.Content = weatherDropDown;
            panel.Tag = "Weather";


            return ViewHelper.AddLabelToControl(panel);
        }

        private static Control GetNoteSlider(Journey journey)
        {
            ComboBox noteDropDown = new ComboBox();
            noteDropDown.DataContext = journey;
            noteDropDown.DataStore = ConstantManager.Instance.Notes;
            noteDropDown.BindDataContext(c => c.SelectedKey, (Journey m) => m.Note);
            noteDropDown.TextColor = Colors.Gold;
            noteDropDown.Tag = "Note";


            return ViewHelper.AddLabelToControl(noteDropDown);
        }
    }
}
