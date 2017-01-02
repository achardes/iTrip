using Eto.Mac.Forms.Controls;
using Eto.Mac.Forms;
using Eto.Mac;
using System.Diagnostics;
using Eto.Drawing;
using Eto.Mac.Forms.ToolBar;
using MonoMac.AppKit;
using Eto.Forms;
using iTrip;

namespace Eto.Test.Mac
{
    class Startup
    {
        static void Main(string[] args)
        {
            AddStyles();

            var platform = new Eto.Mac.Platform();

            var app = new Application(platform);

            // use this to use your own app delegate:
            // ApplicationHandler.Instance.AppDelegate = new MyAppDelegate();

            app.Run(new MainView());
        }

        static void AddStyles()
        {
            // support full screen mode!
            Style.Add<FormHandler>("main", handler =>
                {
                    handler.Control.CollectionBehavior |= NSWindowCollectionBehavior.FullScreenPrimary;
                    //handler.Control.Appearance = NSAppearance.GetAppearance(new MonoMac.Foundation.NSString("NSAppearanceNameVibrantDark"));
                    handler.Control.IsOpaque = true;
                });

            Style.Add<ApplicationHandler>("application", handler =>
                {
                    handler.EnableFullScreen();

                });

            Style.Add<TextBoxHandler>(null, handler =>
             {
                 //handler.Control.Bordered = true;
             });

            Style.Add<ComboBoxHandler>(null, handler =>
             {
                 handler.Control.DrawsBackground = false;
                //handler.Control.Bordered = false;
                //handler.ShowBorder = false;
             });

            Style.Add<DialogHandler>(null, handler =>
             {
                //handler.Control.Appearance = NSAppearance.GetAppearance(new MonoMac.Foundation.NSString("NSAppearanceNameVibrantDark"));
             });

            // other styles
            Style.Add<GridViewHandler>(null, handler =>
                {
                    handler.ScrollView.BorderType = NSBorderType.NoBorder;
                    handler.ScrollView.AlphaValue = 1.0f;
                    handler.ScrollView.DrawsBackground = true;
                    handler.ScrollView.BackgroundColor = NSColor.DarkGray;
                    handler.Control.AlphaValue = 1.0f;
                    handler.Control.WantsLayer = true;
                    handler.Control.BackgroundColor = NSColor.DarkGray;

                    handler.Control.SelectionHighlightStyle = NSTableViewSelectionHighlightStyle.SourceList;
                });

            Style.Add<ButtonToolItemHandler>(null, handler =>
                {
                    // tint the images in grayscale
                    handler.Tint = Colors.Gray;
                });

            Style.Add<ToolBarHandler>(null, handler =>
                {
                    // change display mode or other options
                    handler.Control.DisplayMode = NSToolbarDisplayMode.IconAndLabel;
                    handler.Control.AllowsUserCustomization = true;
                    handler.Control.AutosavesConfiguration = true;
                });
        }
    }
}

