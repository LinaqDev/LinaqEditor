using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace LinaqEdit.Model
{
    public class Component:BaseModel
    {
        public Brush Background { get; set; }
        public Guid GuidId { get; set; } = Guid.Empty;

        private string _name = "";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        private string _description = "";
        public Component()
        {
            this.GuidId = Guid.NewGuid();
            this.AvalonDocument = new TextDocument("");
            SyntaxHighlighting = "HTML";
        }

        public Component(Guid g)
        {
            this.GuidId = g;
            this.AvalonDocument = new TextDocument("");
            SyntaxHighlighting = "HTML";
        }
        public Component(Component sourceItem)
        {
            this.Background = Helpers.Helpers.RandomBrush();
            this.Description = sourceItem.Description;
            this.GuidId = sourceItem.GuidId;
            this.Name = sourceItem.Name;
            this.AvalonDocument = sourceItem.AvalonDocument;
            SyntaxHighlighting = sourceItem.SyntaxHighlighting;
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        private string _componentSourceCode = "";
        public string ComponentSourceCode
        {
            get { return _componentSourceCode; }
            set
            {
                _componentSourceCode = value;
                RaisePropertyChanged("ComponentSourceCode");
            }
        }

        private TextDocument _avalonDocument;
        public TextDocument AvalonDocument
        {
            get { return _avalonDocument; }
            set
            {
                _avalonDocument = value;
                RaisePropertyChanged("AvalonDocument");
            }
        }

        private string _syntaxHighlighting = "";
        public string SyntaxHighlighting
        {
            get { return _syntaxHighlighting; }
            set
            {
                _syntaxHighlighting = value;
                RaisePropertyChanged("SyntaxHighlighting");
            }
        }


    }
}
