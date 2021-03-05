using LinaqEditor.Models;
using LinaqEditor.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LinaqEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Constants.InitConstatns();
            if (Settings.Default.FirstInit)
            {
                DefaultObjects InitDefaultObjects = new DefaultObjects();
                Settings.Default.FirstInit = false;
                Settings.Default.Save();
            }
        }

    }
}
