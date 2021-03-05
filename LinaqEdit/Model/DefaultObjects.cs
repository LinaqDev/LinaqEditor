using ICSharpCode.AvalonEdit.Document;
using LinaqEdit.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqEdit.Model
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
           // CreateDefaultComponents();
        }

        public void CreateDefaultComponents()
        {
            Guid tmp = Guid.NewGuid();
            JsonService.SaveObjectToFile(new Component(tmp)
            {
                Name = "SourceCode1",
                Background = Helpers.Helpers.RandomBrush(),
                Description= "Description for SourceCode1", 
                AvalonDocument =  new TextDocument(""),
                SyntaxHighlighting = "HTML"
        }
            , Path.Combine(Constants.PreparedComponentsFolder, $"{tmp}.linaq"));

            tmp = Guid.NewGuid();
            JsonService.SaveObjectToFile(new Component(tmp)
            {
                Name = "SourceCode2",
                Background = Helpers.Helpers.RandomBrush(),
                Description = "Description for SourceCode2",
                AvalonDocument = new TextDocument(""),
                SyntaxHighlighting = "HTML"
            }
            , Path.Combine(Constants.PreparedComponentsFolder, $"{tmp}.linaq"));


            tmp = Guid.NewGuid();
            JsonService.SaveObjectToFile(new Component(tmp)
            {
                Name = "SourceCode3",
                Background = Helpers.Helpers.RandomBrush(),
                Description = "Description for SourceCode3",
                AvalonDocument = new TextDocument(""),
                SyntaxHighlighting = "C#"
            }
            , Path.Combine(Constants.PreparedComponentsFolder, $"{tmp}.linaq"));


            tmp = Guid.NewGuid();
            JsonService.SaveObjectToFile(new Component(tmp)
            {
                Name = "SourceCode4",
                Background = Helpers.Helpers.RandomBrush(),
                Description = "Description for SourceCode4",
                AvalonDocument = new TextDocument(""),
                SyntaxHighlighting = "HTML"
            }
            , Path.Combine(Constants.PreparedComponentsFolder, $"{tmp}.linaq"));

            //tmp = Guid.NewGuid();
            //JsonService.SaveObjectToFile(new DocumentItem(tmp)
            //{
            //    Name = "Header",
            //    hasEndTag = true

            //}
            //, Path.Combine(Constants.PreparedComponentsFolder, $"{tmp}.linaq"));

            //tmp = Guid.NewGuid();
            //JsonService.SaveObjectToFile(new DocumentItem(tmp)
            //{
            //    Name = "Footer",
            //    hasEndTag = true
            //}
            //, Path.Combine(Constants.PreparedComponentsFolder, $"{tmp}.linaq"));
        }
    }
}
