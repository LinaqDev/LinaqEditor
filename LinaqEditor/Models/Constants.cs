using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinaqEditor
{
    public static class Constants
    {
        public static void InitConstatns()
        {
            ApplicationName = "Linaq Web Editor";
            AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            FileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            ProductVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            CommonAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LinaqEditor");
            CreateFoldersIfNotExists();
        }

        public static string AssemblyVersion { get; set; }
        public static string ApplicationName { get; set; }
        public static string FileVersion { get; set; }
        public static string ProductVersion { get; set; }
        public static string CommonAppDataFolder { get; set; }
        public static string LogFilePath { get { return Path.Combine(CommonAppDataFolder, "Components","Log.linog"); } }
        public static string PreparedComponentsFolder { get { return Path.Combine(CommonAppDataFolder, "Components"); } }

        private static void CreateFoldersIfNotExists()
        {
            if (!Directory.Exists(CommonAppDataFolder))
            {
                Directory.CreateDirectory(CommonAppDataFolder);
            }

            if (!Directory.Exists(Path.GetDirectoryName(LogFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath));
            }

            if (!Directory.Exists(PreparedComponentsFolder))
            {
                Directory.CreateDirectory(PreparedComponentsFolder);
            }
        }
    }
}
