using LinaqEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqEditor.Models
{
    public class LinaqDocument : BaseModel
    {
        public LinaqDocument()
        {
            gId = Guid.NewGuid();
        }
        public Guid gId { get; set; }
        public string FileName { get; set; }
        public LinaqDocType DocumentType { get; set; }
        public string Extension { get; set; }

        private ObservableCollection<DocumentItem> _items = new ObservableCollection<DocumentItem>();
        public ObservableCollection<DocumentItem> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                RaisePropertyChanged("Items");
            }
        }
    }
}
