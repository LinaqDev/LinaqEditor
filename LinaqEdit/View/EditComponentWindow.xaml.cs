using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Highlighting;
using LinaqEdit.Model;
using LinaqEdit.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace LinaqEdit.View
{
    /// <summary>
    /// Interaction logic for EditComponentWindow.xaml
    /// </summary>
    public partial class EditComponentWindow : Window
    {
        public EditComponentWindow()
        {
            InitializeComponent();
            initAvalon(); ;
            DataContext = new EditComponentWindowViewModel(); 
        }

        public EditComponentWindow(Component cmp)
        {
            InitializeComponent();
            initAvalon();
            DataContext = new EditComponentWindowViewModel(cmp);
        }
        private void initAvalon()
        {
            textEditor.Options.ShowTabs = true;
            textEditor.ShowLineNumbers = true;
            textEditor.WordWrap = true;
            textEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
            textEditor.TextArea.TextEntered += textEditor_TextArea_TextEntered;
        }


        CompletionWindow completionWindow;
        void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            if (highlightingComboBox.SelectedValue.ToString() == "HTML")
            {
                if (e.Text == ".")
                {
                    // open code completion after the user has pressed dot:
                    completionWindow = new CompletionWindow(textEditor.TextArea);
                    // provide AvalonEdit with the data:
                    IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
                    data.Add(new MyCompletionData("Item1"));
                    data.Add(new MyCompletionData("Item2"));
                    data.Add(new MyCompletionData("Item3"));
                    data.Add(new MyCompletionData("Another item"));
                    completionWindow.Show();
                    completionWindow.Closed += delegate
                    {
                        completionWindow = null;
                    };
                }
                if (e.Text == "<")
                {
                    // open code completion after the user has pressed dot:
                    completionWindow = new CompletionWindow(textEditor.TextArea);
                    // provide AvalonEdit with the data:
                    IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
                    data.Add(new MyCompletionData("HTML"));
                    data.Add(new MyCompletionData("Header"));
                    data.Add(new MyCompletionData("Footer"));
                    data.Add(new MyCompletionData("Body"));
                    completionWindow.Show();
                    completionWindow.Closed += delegate
                    {
                        completionWindow = null;
                    };
                }
                if (e.Text == ">")
                {

                }
                if (e.Text == " ")
                {
                    //// open code completion after the user has pressed dot:
                    //completionWindow = new CompletionWindow(textEditor.TextArea);
                    //// provide AvalonEdit with the data:
                    //IList<ICompletionData> data = completionWindow.CompletionList.CompletionData; 
                    //foreach (var item in textEditor.Document.Text.Split(',').Where(x => x != string.Empty).Distinct())
                    //{
                    //    data.Add(new MyCompletionData(item.Trim()));
                    //}
                    //completionWindow.Show();
                    //completionWindow.Closed += delegate
                    //{
                    //    completionWindow = null;
                    //};
                }
            }
        }

        void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open,
                    // insert the currently selected element.
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
            // do not set e.Handled=true - we still want to insert the character that was typed
        }

    }
}
