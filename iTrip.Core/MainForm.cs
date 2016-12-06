using System;
using Eto.Forms;
using Eto.Drawing;

namespace iTrip
{
    /// <summary>
    /// Your application's main form
    /// </summary>
    public class MainForm : Form
    {
        public MainForm()
        {
            Model test = new Model();
            test.MyString = "Woot";

            this.DataContext = test;

            Title = "My Eto Form";
            ClientSize = new Size(400, 350);

            //        Content = new StackLayout
            //        {
            //            Items =
            //            {
            //                "Hello World!",
            //	// Add more controls here
            //}
            //        };

            var Label = new Label();
            Label.TextBinding.BindDataContext((Model m) => m.MyString);

            var ListBox = new ListBox();
            ListBox.BindDataContext(c => c.DataStore, (Model m) => m.MyList);
            ListBox.Size = new Size(200, 200);

            var Table = new TableLayout
            {
                Spacing = new Size(5, 5), // space between each cell
                Padding = new Padding(10, 10, 10, 10), // space around the table's sides

                Rows =
                {
                    new TableRow(
                        new TableCell(Label, true),
                        new TableCell(new Label { Text = "Second Column" }, true),
                        new Label { Text = "Third Column" }
                    ),
                    new TableRow(
                        new TextBox { Text = "Some text" },
                        new DropDown { Items = { "Item 1", "Item 2", "Item 3" } },
                        new CheckBox { Text = "A checkbox" }
                    ),
                    // by default, the last row & column will get scaled. This adds a row at the end to take the extra space of the form.
                    // otherwise, the above row will get scaled and stretch the TextBox/ComboBox/CheckBox to fill the remaining height.
                    new TableRow { ScaleHeight = true }
                }
            };

            Content = new TableLayout
            {
                Spacing = new Size(5, 5), // space between each cell
                Padding = new Padding(10, 10, 10, 10), // space around the table's sides


                Rows =
                {
                    new TableRow(
                        new TableCell(ListBox, false),
                        new TableCell(Table, false)
                    ),
                    // by default, the last row & column will get scaled. This adds a row at the end to take the extra space of the form.
                    // otherwise, the above row will get scaled and stretch the TextBox/ComboBox/CheckBox to fill the remaining height.
                    new TableRow { ScaleHeight = true }
                }
            };


            // create a few commands that can be used for the menu and toolbar
            var clickMe = new Command { MenuText = "Click Me!", ToolBarText = "Click Me!" };
            clickMe.Executed += (sender, e) => MessageBox.Show(this, "I was clicked!");
            clickMe.Executed += (sender, e) => test.MyString = "Plop";

            var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += (sender, e) => Application.Instance.Quit();

            var aboutCommand = new Command { MenuText = "About..." };
            aboutCommand.Executed += (sender, e) => MessageBox.Show(this, "About my app...");

            // create menu
            Menu = new MenuBar
            {
                Items = {
					// File submenu
					new ButtonMenuItem { Text = "&File", Items = { clickMe } },
					// new ButtonMenuItem { Text = "&Edit", Items = { /* commands/items */ } },
					// new ButtonMenuItem { Text = "&View", Items = { /* commands/items */ } },
				},
                ApplicationItems = {
					// application (OS X) or file menu (others)
					new ButtonMenuItem { Text = "&Preferences..." },
                },
                QuitItem = quitCommand,
                AboutItem = aboutCommand
            };

            // create toolbar			
            ToolBar = new ToolBar { Items = { clickMe } };
        }
    }
}
