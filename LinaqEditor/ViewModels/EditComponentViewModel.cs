using LinaqEditor.Helpers;
using LinaqEditor.Models;
using LinaqEditor.Views;
using RtfPipe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace LinaqEditor.ViewModels
{
    public class EditComponentViewModel : BaseModel
    {
        #region Events
        public EventHandler<EventArgs> AddCode;
        public EventHandler<EventArgs> ReturnDocumentItem;
        public Func<int> GetCarrotPosition { get; set; }
        #endregion

        #region Constructors
        public EditComponentViewModel()
        {
            GetDocumentCmd = new RelayCommand(GetDocumentExe);
            SaveCmd = new RelayCommand(SaveExe);
        }

        public EditComponentViewModel(DocumentItem di)
        {
            DocumentText = di.RichTextBoxContent;
            CurrentDocumentItem = new DocumentItem(di,true);
            GetDocumentCmd = new RelayCommand(GetDocumentExe);
            SaveCmd = new RelayCommand(SaveExe);
            CancelCmd = new RelayCommand(CancelExe);
        }

        #endregion

        #region Commands
        public ICommand GetDocumentCmd { get; set; }
        public ICommand SaveCmd { get; set; }
        public ICommand CancelCmd { get; set; }
        #endregion

        #region Properties
        private DocumentItem _currentDocumentItem = new DocumentItem();
        public DocumentItem CurrentDocumentItem
        {
            get
            {
                return _currentDocumentItem;
            }
            set
            {
                _currentDocumentItem = value;
                RaisePropertyChanged("CurrentDocumentItem");
            }
        }

        private string _documentText;
        public string DocumentText
        {
            get
            {
                return _documentText;
            }
            set
            {
                _documentText = value;
                CurrentDocumentItem.RichTextBoxContent = value;
                CurrentDocumentItem.Htmlcontent = ConvertToHtml(value);
                RaisePropertyChanged("DocumentText");
            }
        } 
        public IEnumerable<LinaqCompontentType> ComponentTypes
        {
            get
            { 
                return Enum.GetValues(typeof(LinaqCompontentType)).Cast<LinaqCompontentType>();
            }
        }
        #endregion

        #region Methods
        private void SaveExe(object obj)
        {
            ReturnDocumentItem(CurrentDocumentItem, EventArgs.Empty);
            if (obj is EditComponentWindow)
                (obj as EditComponentWindow).Close();
        }

        private void CancelExe(object obj)
        {
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show($"Unsaved changed will be lost, do you want to continue?", $"Cancel Edit", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                if (obj is EditComponentWindow)
                    (obj as EditComponentWindow).Close();
            }
        }

        private string ConvertToHtml(string rtbtxt)
        {
            return rtfToHtml(rtbtxt);
        }

        private void GetDocumentExe(object obj)
        {
            TextRange range;
            FileStream fileStream;

            range = new TextRange(((FlowDocument)obj).ContentStart,
                                  ((FlowDocument)obj).ContentEnd);
            fileStream = new FileStream("test.rtf", FileMode.Create);
            range.Save(fileStream, DataFormats.Xaml);
            fileStream.Close();
            string contents = File.ReadAllText(@"test.rtf");
            MessageBox.Show("Text File Saved");
        }

        private void InsertCodeExe(object obj)
        {

            AddCode("", EventArgs.Empty);
        }

        private string rtfToHtml(string rtfValue)
        {
            using (TextReader tr = new StringReader(rtfValue))
            {
                RtfSource rtfs = new RtfSource(tr);
                return Rtf.ToHtml(rtfs).Replace("&lt;tab&gt;", "&emsp;");
            }

        }
        #endregion 
    }
}
