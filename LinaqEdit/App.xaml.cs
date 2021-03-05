using LinaqEdit.Model;
using LinaqEdit.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LinaqEdit
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
                InitDefaultObjects.CreateDefaultComponents();
                Settings.Default.FirstInit = false;
                Settings.Default.Save();
            }

            using (var stream = new System.IO.MemoryStream(LinaqEdit.Properties.Resources.CustomHighlighting))
            {
                using (var reader = new System.Xml.XmlTextReader(stream))
                {
                    ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.RegisterHighlighting("customLinaq", new string[0],
                        ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(reader,
                            ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance));
                }
            }

            using (var stream = new System.IO.MemoryStream(LinaqEdit.Properties.Resources.htmlHighlight))
            {
                using (var reader = new System.Xml.XmlTextReader(stream))
                {
                    ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.RegisterHighlighting("HTMLLinaq", new string[0],
                        ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(reader,
                            ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance));
                }
            }

            using (var stream = new System.IO.MemoryStream(LinaqEdit.Properties.Resources.cssHighlight))
            {
                using (var reader = new System.Xml.XmlTextReader(stream))
                {
                    ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.RegisterHighlighting("CSSLinaq", new string[0],
                        ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(reader,
                            ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance));
                }
            }

            foreach (var item in ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.HighlightingDefinitions)
            {
                Statics.highlightDefinitions.Add(item.Name);
            }

            // Statics.CurrentGlobalDocument = new Document();
        }

    }
}