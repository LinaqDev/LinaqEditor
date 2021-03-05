using LinaqEdit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqEdit.Helpers
{
    public static class ComponentService
    {
        public static ObservableCollection<Component> GetSavedComponents()
        {
            var cmp = new ObservableCollection<Component>();
            
            try
            {
                DirectoryInfo d = new DirectoryInfo(Constants.PreparedComponentsFolder);
                FileInfo[] Files = d.GetFiles("*.linaq");

                if (Files.Count() > 0)
                { 
                    int i = 0;
                    foreach (FileInfo file in Files)
                    {
                        cmp.Add(JsonService.GetObjectFromFile<Component>(file.FullName));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
                return null;
            }

            return cmp;
        } 
        public static void DeleteComponentFile()
        {

        }
    }
}
