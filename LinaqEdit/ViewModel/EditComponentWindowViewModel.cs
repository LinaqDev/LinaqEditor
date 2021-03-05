using LinaqEdit.Helpers;
using LinaqEdit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LinaqEdit.ViewModel
{
    public class EditComponentWindowViewModel : BaseModel
    {
        public EditComponentWindowViewModel()
        {

        }
        public EditComponentWindowViewModel(Component cmp)
        {
            CurrentEditComponent = new Component(cmp);
            SaveComponentCmd = new RelayCommand(SaveComponentExe);
        }

        private void SaveComponentExe(object obj)
        {
            var t = CurrentEditComponent.AvalonDocument.Text;
        }

        public ICommand SaveComponentCmd { get; set; }

        private Component _currentEditComponent = new Component();
        public Component CurrentEditComponent
        {
            get { return _currentEditComponent; }
            set
            {
                _currentEditComponent = value;
                RaisePropertyChanged("CurrentEditComponent");
            }
        }
    }
}
