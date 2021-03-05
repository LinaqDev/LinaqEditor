using HtmlAgilityPack;
using LinaqEditor.Helpers;
using LinaqEditor.Models;
using LinaqEditor.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LinaqEditor.ViewModels
{
    public class MainViewModel : BaseModel
    {
        #region Constructors
        public MainViewModel()
        {
            SaveCmd = new RelayCommand(SaveExe);
            NewCmd = new RelayCommand(NewExe);
            OpenCmd = new RelayCommand(OpenExe);
            ExportToHtmlCmd = new RelayCommand(ExportToHtmlExe);
            RestoreDefaultComponentsCmd = new RelayCommand(RestoreDefaultComponentsExe);
            SelectedPage = ep;
        }




        #endregion

        #region Commands
        public ICommand NewCmd { get; set; }
        public ICommand SaveCmd { get; set; }
        public ICommand OpenCmd { get; set; }
        public ICommand ExportToHtmlCmd { get; set; }
        public ICommand RestoreDefaultComponentsCmd { get; set; }
        #endregion

        #region Properties
        EditPage ep = new EditPage();
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

        private string _operationInfo = "";
        public string OperationInfo
        {
            get
            {
                return _operationInfo;
            }
            set
            {
                _operationInfo = value;
                RaisePropertyChanged("OperationInfo");
            }
        }

        public string CurrentSaveLocation { get; set; }
        #endregion

        #region Methods
        private void RestoreDefaultComponentsExe(object obj)
        {
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show($"Old components wont be delete.\nAll default components will be added to list, do you want to continue?", $"Restore Default", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                DefaultObjects defaulto = new DefaultObjects();
                var dc = (EditViewModel)ep.DataContext;
                dc.LoadPreparedComponents();
            }
        }
        private void SaveExe(object obj)
        {
            try
            {
                if (CurrentLinaqDocument != null)
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
                            JsonService.SaveObjectToFile(CurrentLinaqDocument, saveFileDialog.FileName);
                            CurrentSaveLocation = saveFileDialog.FileName;
                            OperationInfo = "Saved Successfully";
                        }
                    }
                    else
                    {
                        JsonService.SaveObjectToFile(CurrentLinaqDocument, CurrentSaveLocation);
                    }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
                OperationInfo = "Failed to save file...";
            }
        }

        private void NewExe(object obj)
        {
            CurrentSaveLocation = "";
            CurrentLinaqDocument = new Models.LinaqDocument();
        }

        private void OpenExe(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Linaq files |*.linaq";
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Title = "Please select file";
            openFileDialog.FileName = "NewProject.Linaq";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                CurrentLinaqDocument = JsonService.GetObjectFromFile<LinaqDocument>(openFileDialog.FileName);
                CurrentSaveLocation = openFileDialog.FileName;
                //  var t = openFileDialog.FileName;
            }
        }

        private string CreateHTMLDocument()
        {

            if (CurrentLinaqDocument != null && CurrentLinaqDocument.Items.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in CurrentLinaqDocument.Items)
                {
                    sb.Append(item.Htmlcontent);
                }
                return sb.ToString();
            }
            return "";
        }
        private void ExportToHtmlExe(object obj)
        {
            try
            {
                if (CurrentLinaqDocument != null)
                {
                    var saveFileDialog = new Microsoft.Win32.SaveFileDialog()
                    {
                        DefaultExt = "*.html",
                        Filter = "HTML Files (*.html)|*.html",
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    };

                    var result = saveFileDialog.ShowDialog();
                    if (result != null && result == true)
                    {
                        HtmlDocument htmlf = new HtmlDocument();
                        htmlf.OptionFixNestedTags = true;
                        htmlf.LoadHtml(CreateHTMLDocument());
                        htmlf.OptionFixNestedTags = true;
                        htmlf.Save(saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
                OperationInfo = "Failed to save file...";
            }
        }
        #endregion 
    }
}
