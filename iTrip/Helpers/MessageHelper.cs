//using MahApps.Metro.Controls;
//using MahApps.Metro.Controls.Dialogs;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;

//namespace RoadTripManager
//{
//    public static class MessageHelper
//    {
//        public static async void DisplayException(Exception exception)
//        {
//            string errorMessage = exception.Message;
//            if(exception.InnerException != null)
//            {
//                errorMessage += Environment.NewLine;
//                errorMessage += exception.InnerException.Message;
//            }

//            MessageDialogResult result = await (Application.Current.MainWindow as MetroWindow).ShowMessageAsync(
//            "Caution, an unexpected exception has been thrown",
//            errorMessage,
//            MessageDialogStyle.Affirmative
//            );
//        }
//    }
//}
