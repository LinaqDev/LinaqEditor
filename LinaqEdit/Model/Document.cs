using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqEdit.Model
{
    public class Document : BaseModel
    {
        public Document()
        {
            DocumentGuid = Guid.NewGuid();
            CreationDateTime = DateTime.Now;
        }

        private string _title = "New Document";
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string FileName { get; set; }
        public Guid DocumentGuid { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime LastEditDateTime { get; set; }

        private ObservableCollection<Component> _components = new ObservableCollection<Component>();
        public ObservableCollection<Component> Components
        {
            get { return _components; }
            set
            {
                _components = value;
                RaisePropertyChanged("Components");
            }
        }
    }
}
