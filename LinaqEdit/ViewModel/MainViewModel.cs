using LinaqEdit.Helpers;
using LinaqEdit.Model;
using LinaqEdit.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LinaqEdit.ViewModel
{
    public class MainViewModel : BaseModel
    {
        #region Constructors
        public MainViewModel()
        {
            ExitApplicationCmd = new RelayCommand(ExitApplicationExe);
            SaveDocumentCmd = new RelayCommand(SaveDocumentExe);
            CreateNewDocumentCmd = new RelayCommand(CreateNewDocumentExe);
            OpenDocumentCmd = new RelayCommand(OpenDocumentExe);
            RestoreDefaultComponentsCmd = new RelayCommand(RestoreDefaultComponentsExe);
            SelectedPage = new DefaultEditPage();
        }

       

        #endregion

        #region Commands
        public ICommand ExitApplicationCmd { get; set; }
        public ICommand CreateNewDocumentCmd { get; set; }
        public ICommand OpenDocumentCmd { get; set; }
        public ICommand SaveDocumentCmd { get; set; }
        public ICommand ShowDocumentPreviewCmd { get; set; }
        public ICommand ExportDocumentToHtmlCmd { get; set; }
        public ICommand RestoreDefaultComponentsCmd { get; set; }
        #endregion

        #region Properties
        private Page _selectedPage = new Page();
        public Page SelectedPage
        {
            get
            {
                return _selectedPage;
            }
            set
            {
                _selectedPage = value;
                RaisePropertyChanged("SelectedPage");
            }
        }
        public string CurrentSaveLocation { get; set; }
        #endregion

        #region Methods
        private void ExitApplicationExe(object obj)
        {
            Application.Current.MainWindow.Close();
        }

        private void SaveDocumentExe(object obj)
        {
            try
            {
                Document DocumentToSave = null;
                if (SelectedPage.DataContext is DefaultEditPageViewModel)
                    DocumentToSave = (SelectedPage.DataContext as DefaultEditPageViewModel).CurrentOpenDocument;

                if (DocumentToSave != null)
                    if (string.IsNullOrWhiteSpace(CurrentSaveLocation))
                    {
                        var saveFileDialog = new Microsoft.Win32.SaveFileDialog()
                        {
                            DefaultExt = "*.linaq",
                            Filter = "Linaq Files (*.linaq)|*.linaq",
                            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        };

                        var result = saveFileDialog.ShowDialog();
                        if (result != null && result == true)
                        {
                            JsonService.SaveObjectToFile(DocumentToSave, saveFileDialog.FileName);
                            CurrentSaveLocation = saveFileDialog.FileName;
                            // OperationInfo = "Saved Successfully";
                        }
                    }
                    else
                    {
                        JsonService.SaveObjectToFile(DocumentToSave, CurrentSaveLocation);
                    }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
                //  OperationInfo = "Failed to save file...";
            }
        }

        private void CreateNewDocumentExe(object obj)
        {
            SelectedPage = new DefaultEditPage();
        }
        private void OpenDocumentExe(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Linaq files |*.linaq";
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Title = "Please select file";
            openFileDialog.FileName = "NewProject.Linaq";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                var dc = JsonService.GetObjectFromFile<Document>(openFileDialog.FileName);
                CurrentSaveLocation = openFileDialog.FileName;
                SelectedPage = new DefaultEditPage(dc);
            } 
        }

        private void RestoreDefaultComponentsExe(object obj)
        {
            DefaultObjects df = new DefaultObjects();
            df.CreateDefaultComponents();
            if (SelectedPage.DataContext is DefaultEditPageViewModel)
                (SelectedPage.DataContext as DefaultEditPageViewModel).RefreshPreparedComponents();
        }
        #endregion
    }
}
