using GongSolutions.Wpf.DragDrop;
using LinaqEditor.Helpers;
using LinaqEditor.Models;
using LinaqEditor.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LinaqEditor.ViewModels
{
    public class EditViewModel : BaseModel, IDropTarget
    {
        #region Constructors
        public EditViewModel()
        {
            CurrentLinaqDocument = new LinaqDocument();
            DeleteDocumentItemCmd = new RelayCommand(DeleteDocumentItemExe);
            EditDocumentItemCmd = new RelayCommand(EditDocumentItemExe);
            DeleteComponentCmd = new RelayCommand(DeleteComponentExe);
            EditComponentCmd = new RelayCommand(EditComponentExe);
            LoadPreparedComponents();
        }

        #endregion

        #region Commands
        public ICommand DeleteDocumentItemCmd { get; set; }
        public ICommand EditDocumentItemCmd { get; set; }
        public ICommand DeleteComponentCmd { get; set; }
        public ICommand EditComponentCmd { get; set; }
        #endregion

        #region Properties
        private DocumentItem _selectedDocumentItem = new DocumentItem();
        public DocumentItem SelectedDocumentItem
        {
            get
            {
                return _selectedDocumentItem;
            }
            set
            {
                _selectedDocumentItem = value;
                RaisePropertyChanged("SelectedDocumentItem");
            }
        }

        private ObservableCollection<DocumentItem> _preparedComponents = new ObservableCollection<DocumentItem>();
        public ObservableCollection<DocumentItem> PreparedComponents
        {
            get
            {
                var tmp1 = _preparedComponents.OrderBy(x => x.Name);
                ObservableCollection<DocumentItem> tmp = new ObservableCollection<DocumentItem>();
                foreach (var item in tmp1)
                {
                    tmp.Add(item);
                }

                return tmp;
            }
        }
        #endregion

        #region Methods
        private void DeleteDocumentItemExe(object obj)
        {
            try
            {
                CurrentLinaqDocument.Items.Remove(CurrentLinaqDocument.Items.FirstOrDefault(x => x.gId.ToString() == obj.ToString()));
            }
            catch(Exception ex)
            {
                Log.LogException(ex);
            }
        }

        private void EditDocumentItemExe(object obj)
        {
            DocumentItem editobj = CurrentLinaqDocument.Items.FirstOrDefault(x => x.gId.ToString() == obj.ToString());
            Window win = new EditComponentWindow(editobj);
            win.Owner = Application.Current.MainWindow;
            var dc = (EditComponentViewModel)win.DataContext;
            dc.ReturnDocumentItem += UpdateDocumentItem;
            win.ShowDialog();
        }

        private void UpdateDocumentItem(object sender, EventArgs e)
        {
            if (sender is DocumentItem)
            {
                DocumentItem updatedDc = new DocumentItem((DocumentItem)sender, true);
                int index = CurrentLinaqDocument.Items.IndexOf(CurrentLinaqDocument.Items.FirstOrDefault(x => x.gId == updatedDc.gId));
                CurrentLinaqDocument.Items.RemoveAt(index);
                CurrentLinaqDocument.Items.Insert(index, updatedDc);
            }
        }

        public void LoadPreparedComponents()
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(Constants.PreparedComponentsFolder);
                FileInfo[] Files = d.GetFiles("*.linaq");

                if (Files.Count() > 0)
                {
                    _preparedComponents.Clear();
                    int i = 0;
                    foreach (FileInfo file in Files)
                    {
                        _preparedComponents.Add(JsonService.GetObjectFromFile<DocumentItem>(file.FullName));
                    }
                }
                RaisePropertyChanged("PreparedComponents");
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }

        private void DeleteComponentExe(object obj)
        {
            try
            {
                var tmp = _preparedComponents.FirstOrDefault(x => x.gId.ToString() == obj.ToString());
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show($"Do you realy want to delete {tmp.Name} ?", $"Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _preparedComponents.Remove(_preparedComponents.FirstOrDefault(x => x.gId.ToString() == obj.ToString()));
                    File.Delete(Path.Combine(Constants.PreparedComponentsFolder, $"{obj.ToString()}.linaq"));
                    RaisePropertyChanged("PreparedComponents");
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }

        private void EditComponentExe(object obj)
        {
            DocumentItem editobj = _preparedComponents.FirstOrDefault(x => x.gId.ToString() == obj.ToString());
            Window win = new EditComponentWindow(editobj);
            win.Owner = Application.Current.MainWindow;
            var dc = (EditComponentViewModel)win.DataContext;
            dc.ReturnDocumentItem += UpdateComponent;
            win.ShowDialog();
            RaisePropertyChanged("PreparedComponents");
        }

        private void UpdateComponent(object sender, EventArgs e)
        {
            if (sender is DocumentItem)
            {
                DocumentItem updatedDc = new DocumentItem((DocumentItem)sender, true);
                int index = _preparedComponents.IndexOf(_preparedComponents.FirstOrDefault(x => x.gId == updatedDc.gId));
                _preparedComponents.RemoveAt(index);
                _preparedComponents.Insert(index, updatedDc);
                savePreparedComponenttoFile(updatedDc);
                RaisePropertyChanged("PreparedComponents");
            }
        }

        private void savePreparedComponenttoFile(DocumentItem dc)
        {
            JsonService.SaveObjectToFile(dc, Path.Combine(Constants.PreparedComponentsFolder, $"{dc.gId}.linaq"));
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            DocumentItem sourceItem = dropInfo.Data as DocumentItem;
            ObservableCollection<DocumentItem> targetItem = dropInfo.TargetCollection as ObservableCollection<DocumentItem>;

            if (sourceItem != null && targetItem != null)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            int i = dropInfo.InsertIndex;
            DocumentItem sourceItem = dropInfo.Data as DocumentItem;
            ObservableCollection<DocumentItem> targetItem = dropInfo.TargetCollection as ObservableCollection<DocumentItem>;

            if (CheckIfSameCollection((ObservableCollection<DocumentItem>)dropInfo.DragInfo.SourceCollection, targetItem))
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
                if (sourceItem.hasEndTag)
                {
                    Guid end = Guid.NewGuid();
                    targetItem.Insert(i, new DocumentItem(sourceItem, false, end));
                    targetItem.Insert(i, new DocumentItem(sourceItem, false, end));
                }
                else
                {
                    targetItem.Insert(i, new DocumentItem(sourceItem, false));
                }

            }

        }

        private bool CheckIfSameCollection(ObservableCollection<DocumentItem> x, ObservableCollection<DocumentItem> y)
        {
            foreach (var item in x)
            {
                if (y.FirstOrDefault(im => im.gId == item.gId) == null)
                    return false;
            }
            return true;
        }
        private bool CheckIfCanInsert(int si,int i)
        {
            return false;
        }

        private void UpdateMargin()
        {
            int margin = 0;
            List<string> endtags = new List<string>();
            foreach (var item in CurrentLinaqDocument.Items)
            {
                if (item.hasEndTag)
                {
                    endtags.Add(item.endGuid.ToString());
                    margin += 20;
                }
            }
        }
        #endregion  
    }
}
