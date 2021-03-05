using GongSolutions.Wpf.DragDrop;
using LinaqEdit.Helpers;
using LinaqEdit.Model;
using LinaqEdit.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LinaqEdit.ViewModel
{
    public class DefaultEditPageViewModel : BaseModel, IDropTarget
    {
        public DefaultEditPageViewModel()
        {
            init();
            CurrentOpenDocument = new Document();
        }

        public DefaultEditPageViewModel(Document dc)
        {
            init();
            CurrentOpenDocument = dc;
        }
        private void init()
        {
            EditComponentCmd = new RelayCommand(EditComponentExe);
            DeleteComponentCmd = new RelayCommand(DeleteComponentExe);
            AddNewComponentCmd = new RelayCommand(AddNewComponentExe);
            
            RefreshPreparedComponents();
        } 

        public ICommand EditComponentCmd { get; set; }
        public ICommand DeleteComponentCmd { get; set; }
        public ICommand AddNewComponentCmd { get; set; }

        private Document _currentOpenDocument = new Document();
        public Document CurrentOpenDocument
        {
            get { return _currentOpenDocument; }
            set
            {
                _currentOpenDocument = value;
                RaiseStaticPropertyChanged("CurrentOpenDocument");
            }
        }

        private ObservableCollection<Component> _preparedComponents;
        public ObservableCollection<Component> PreparedComponents
        {
            get
            {
                var tmp1 = _preparedComponents.OrderBy(x => x.Name);
                ObservableCollection<Component> tmp = new ObservableCollection<Component>();
                foreach (var item in tmp1)
                {
                    tmp.Add(item);
                }

                return tmp;
            }
        }
        private void EditComponentExe(object obj)
        {
            if(obj is Component)
            {
                var win = new EditComponentWindow(obj as Component);
                win.ShowDialog();
            } 
        }

        private void DeleteComponentExe(object obj)
        {
            if (obj is Component)
            {
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show($"Do you realy want to delete {(obj as Component).Name} ?", $"Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _preparedComponents.Remove(PreparedComponents.FirstOrDefault(x => x.GuidId.ToString() == (obj as Component).GuidId.ToString()));
                    RaisePropertyChanged("PreparedComponents");
                    File.Delete(Path.Combine(Constants.PreparedComponentsFolder, $"{(obj as Component).GuidId.ToString()}.linaq"));
                }
            }
        }

        private void AddNewComponentExe(object obj)
        {
            if (obj is Component)
            {

            }
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            Component sourceItem = dropInfo.Data as Component;
            ObservableCollection<Component> targetItem = dropInfo.TargetCollection as ObservableCollection<Component>;

            if (sourceItem != null && targetItem != null)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            int i = dropInfo.InsertIndex;
            Component sourceItem = dropInfo.Data as Component;
            ObservableCollection<Component> targetItem = dropInfo.TargetCollection as ObservableCollection<Component>;

            if (isSameCollection((ObservableCollection<Component>)dropInfo.DragInfo.SourceCollection, targetItem))
            {
                if (targetItem.Count - 1 < dropInfo.InsertIndex)
                {
                    i = targetItem.Count - 1;
                }
                else
                {
                    i = dropInfo.InsertIndex;
                }
                targetItem.Move(dropInfo.DragInfo.SourceIndex, i);
            }

            else
            {
                targetItem.Insert(i, new Component(sourceItem));

            }
        }

        private bool isSameCollection(ObservableCollection<Component> x, ObservableCollection<Component> y)
        {
            foreach (var item in x)
            {
                if (y.FirstOrDefault(im => im.GuidId == item.GuidId) == null)
                    return false;
            }
            return true;
        }

        internal void RefreshPreparedComponents()
        {
            _preparedComponents = ComponentService.GetSavedComponents();
            RaisePropertyChanged("PreparedComponents");
        }
    }
}
