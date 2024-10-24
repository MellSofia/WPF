using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xaml.Permissions;

namespace WPF_CreateCopyWriteDocuments10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filename = "doc.xaml";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = File.Open(filename, FileMode.Create))
            {
                if (document.Document != null)
                {
                    XamlWriter.Save(document.Document, fs);
                    MessageBox.Show("Успешное сохранение!");
                }
                else
                {
                    MessageBox.Show("Сначала сделайте записи, прежде чем пытаться сохранить.");
                }
            }
        }
        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            document.ClearValue(FlowDocumentScrollViewer.DocumentProperty);
        }
        private void Button_Load(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = File.Open(filename, FileMode.Open))
            {
                FlowDocument doc = XamlReader.Load(fs) as FlowDocument;
                if (doc != null)
                {
                    document.Document = doc;
                }
            }
        }
    }
}
