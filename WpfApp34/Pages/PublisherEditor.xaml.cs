using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp34.Tools;

namespace WpfApp34.Pages
{
    /// <summary>
    /// Логика взаимодействия для PublisherEditor.xaml
    /// </summary>
    public partial class PublisherEditor : Page
    {
        public PublisherEditor(IVM vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
