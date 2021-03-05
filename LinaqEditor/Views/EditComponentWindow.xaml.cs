using LinaqEditor.Models;
using LinaqEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace LinaqEditor.Views
{
    /// <summary>
    /// Interaction logic for EditComponentWindow.xaml
    /// </summary>
    public partial class EditComponentWindow : Window
    { 
        private bool colorChangedByUser = false;
        public EditComponentWindow()
        {
            InitializeComponent();
            DataContext = new EditComponentViewModel();

            var castedContext = (EditComponentViewModel)DataContext;
            castedContext.AddCode += AddCodeExe;
        }

        public EditComponentWindow(DocumentItem editobj)
        {
            InitializeComponent();
            DataContext = new EditComponentViewModel(editobj);

            var castedContext = (EditComponentViewModel)DataContext;
            castedContext.AddCode += AddCodeExe;
        }

        private void AddCodeExe(object sender, EventArgs e)
        {
            _richTextBox.CaretPosition = _richTextBox.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            _richTextBox.Paste();
        }

        private void SaveRtf()
        {
            //System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\test\\test.rtf");
            //file.WriteLine((RichTextBox)_richTextBox.rtf.Rtf);
            //file.Close();
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (colorChangedByUser)
                _richTextBox.Selection.ApplyPropertyValue(TextElement.ForegroundProperty,
                    new SolidColorBrush(_colorPicker.SelectedColor.Value));

            colorChangedByUser = true;
        }

        private void _richTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                colorChangedByUser = false;
                _colorPicker.SelectedColor = new Color();

                Color tmp = Colors.Black;

                var color = (Color)ColorConverter.ConvertFromString(_richTextBox.Selection.GetPropertyValue(TextElement.ForegroundProperty).ToString());
                _colorPicker.SelectedColor = color;
            }
            catch
            {

            }
        }

        private void _richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                _richTextBox.CaretPosition = _richTextBox.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                _richTextBox.Selection.Start.InsertTextInRun($" <tab> ");
            }
        }
    }
}
