using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace LinaqEditor.Helpers
{
    public static class Log
    {
        public static void LogException(Exception ex)
        {
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("Hello world!", "Extended WPF ToolKit MessageBox", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}
