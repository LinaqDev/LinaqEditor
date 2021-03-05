using LinaqEditor.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqEditor.Models
{
    public class DefaultObjects
    {
        public DefaultObjects()
        {
            //DirectoryInfo d = new DirectoryInfo(Constants.PreparedComponentsFolder);
            //FileInfo[] Files = d.GetFiles("*.linaq");
            //if (Files.Count() < 1)
            //{
            //}
            CreateDefaultComponents();
        }

        public void CreateDefaultComponents()
        {
            Guid tmp = Guid.NewGuid();
            JsonService.SaveObjectToFile(new DocumentItem(tmp)
            {
                Name = "SourceCode"
            }
            , Path.Combine(Constants.PreparedComponentsFolder, $"{tmp}.linaq"));

            tmp = Guid.NewGuid();
            JsonService.SaveObjectToFile(new DocumentItem(tmp)
            {
                Name = "Header",
                hasEndTag = true
                
            }
            , Path.Combine(Constants.PreparedComponentsFolder, $"{tmp}.linaq"));

            tmp = Guid.NewGuid();
            JsonService.SaveObjectToFile(new DocumentItem(tmp)
            {
                Name = "Footer",
                hasEndTag = true
            }
            , Path.Combine(Constants.PreparedComponentsFolder, $"{tmp}.linaq"));
        }
    }
}
