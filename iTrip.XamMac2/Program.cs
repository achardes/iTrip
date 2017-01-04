using System;
using AppKit;
using Eto;
using Eto.Drawing;
using Eto.Forms;
using Eto.Mac.Forms;
using Eto.Mac.Forms.Controls;
using Eto.Mac.Forms.ToolBar;

namespace iTrip.XamMac2
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            AddStyles();

            var platform = new Eto.Mac.Platform();

            //var platform = Platforms.XamMac2; // mac platform

            // to register your custom control handler, call this before using your class:
            //platform.Add<SearchToolItem.IMyCustomControl>(() => new SearchToolItemHandler());


            new Application(Platforms.XamMac2).Run(new MainView());
        }



        static void AddStyles()
        {
            // support full screen mode!
            Style.Add<FormHandler>("main", handler =>
                {
                    //handler.Control.CollectionBehavior |= NSWindowCollectionBehavior.FullScreenPrimary;
                    ////handler.Control.Appearance = NSAppearance.GetAppearance(NSAppearance.NameVibrantDark);
                    handler.Control.TitleVisibility = NSWindowTitleVisibility.Hidden;
                    handler.Control.TitlebarAppearsTransparent = true;
                    handler.Control.StyleMask |= NSWindowStyle.FullSizeContentView;
                    handler.Control.BackgroundColor = NSColor.White;
                });

            Style.Add<ApplicationHandler>("application", handler =>
                {
                    handler.EnableFullScreen();

                });

     

            Style.Add<ComboBoxHandler>(null, handler =>
             {
                 //handler.Control.DrawsBackground = false;
                 //handler.Control.Bordered = false;
                 //handler.ShowBorder = false;
             });

            Style.Add<DialogHandler>(null, handler =>
             {
                 //handler.Control.Appearance = NSAppearance.GetAppearance(new MonoMac.Foundation.NSString("NSAppearanceNameVibrantDark"));
             });

            // other styles
            Style.Add<GridViewHandler>("journeyList", handler =>
                {
                    handler.ScrollView.BorderType = NSBorderType.NoBorder;
                    handler.Control.SelectionHighlightStyle = NSTableViewSelectionHighlightStyle.SourceList;
                    handler.Control.BackgroundColor = NSColor.White;
                });

            Style.Add<ToolBarHandler>(null, handler =>
                {
                    // change display mode or other options
                    handler.Control.AllowsUserCustomization = true;
                    handler.Control.AutosavesConfiguration = true;
                    handler.Control.ShowsBaselineSeparator = false;
                });
        }
    }
}
