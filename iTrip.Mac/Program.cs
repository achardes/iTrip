using System;
using Eto;
using Eto.Forms;

namespace iTrip.Mac
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new Application(Platforms.Mac).Run(new MainView());
        }
    }
}
//using Eto.Forms;
//using Eto;

//namespace iTrip.Mac
//{
//    class Startup
//    {
//        static void Main(string[] args)
//        {
//           // AddStyles();

//            var app = new Application(Platforms.Mac);

//            // use this to use your own app delegate:
//            // ApplicationHandler.Instance.AppDelegate = new MyAppDelegate();

//            app.Run();
//        }

//        //static void AddStyles()
//        //{
//        //    // support full screen mode!
//        //    Style.Add<FormHandler>("main", handler =>
//        //        {
//        //            handler.Control.CollectionBehavior |= NSWindowCollectionBehavior.FullScreenPrimary;
//        //        });

//        //    Style.Add<ApplicationHandler>("application", handler =>
//        //        {
//        //            handler.EnableFullScreen();
//        //        });

//        //    // other styles
//        //    Style.Add<TreeGridViewHandler>("sectionList", handler =>
//        //        {
//        //            handler.ScrollView.BorderType = NSBorderType.NoBorder;
//        //            handler.Control.SelectionHighlightStyle = NSTableViewSelectionHighlightStyle.SourceList;
//        //        });

//        //    Style.Add<ButtonToolItemHandler>(null, handler =>
//        //        {
//        //            // tint the images in grayscale
//        //            handler.Tint = Colors.Gray;
//        //        });

//        //    Style.Add<ToolBarHandler>(null, handler =>
//        //        {
//        //            // change display mode or other options
//        //            handler.Control.DisplayMode = NSToolbarDisplayMode.Icon;
//        //        });
//        //}
//    }
//}

