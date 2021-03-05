using LinaqEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LinaqEditor.Models
{
    public class DocumentItem : BaseModel
    {
        #region Constructors
        public DocumentItem()
        {
            Background = RandomBrush();
            gId = Guid.NewGuid();
        }

        public DocumentItem(Guid _gId)
        {
            gId = _gId; 
            Background = RandomBrush();
        }

        public DocumentItem(DocumentItem sourceItem, bool keepGuid)
        {
            if (keepGuid)
                gId = sourceItem.gId;
            else
                gId = Guid.NewGuid();
            Name = sourceItem.Name;
            Background = sourceItem.Background;
            Description = sourceItem.Description;
            Htmlcontent = sourceItem.Htmlcontent;
            RichTextBoxContent = sourceItem.RichTextBoxContent;
            endGuid = sourceItem.endGuid;
        }
        public DocumentItem(DocumentItem sourceItem, bool keepGuid,Guid endg)
        {
            if (keepGuid)
                gId = sourceItem.gId;
            else
                gId = Guid.NewGuid();
            Name = sourceItem.Name;
            Background = sourceItem.Background;
            Description = sourceItem.Description;
            Htmlcontent = sourceItem.Htmlcontent;
            RichTextBoxContent = sourceItem.RichTextBoxContent;
            hasEndTag = sourceItem.hasEndTag;
            endGuid = endg;
        }
        #endregion

        #region Commands

        #endregion

        #region Properties
        static Random rand = new Random();
        public Guid endGuid = Guid.NewGuid();
        public bool hasEndTag = false;
        private int _marg = 0;
        public int Marg
        {
            get
            {
                return _marg;
            }
            set
            {
                _marg = value;
                RaisePropertyChanged("Marg");
            }
        }
        public bool CanAcceptChildren { get; set; } = true;
        //if can accept children then display empty list
        public ObservableCollection<DocumentItem> Children { get; set; } = new ObservableCollection<DocumentItem>();
        public Brush Background { get; set; }
        public Guid gId { get; set; } = Guid.Empty;

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

        private string _htmlContent = " ";
        public string Htmlcontent
        {
            get
            {
                return _htmlContent;
            }
            set
            {
                _htmlContent = value;
                RaisePropertyChanged("Htmlcontent");
            }
        }

        private string _richTextBoxContent = "";
        public string RichTextBoxContent
        {
            get
            {
                return _richTextBoxContent;
            }
            set
            {
                _richTextBoxContent = value;
                RaisePropertyChanged("RichTextBoxContent");
            }
        }
        #endregion

        #region Methods
        private Brush RandomBrush()
        {
            return new SolidColorBrush(Color.FromRgb((byte)rand.Next(0, 256), (byte)rand.Next(0, 256), (byte)rand.Next(0, 256)));
        }
        #endregion  
    }
}
