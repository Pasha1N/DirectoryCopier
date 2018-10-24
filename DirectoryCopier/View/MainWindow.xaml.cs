using DirectoryCopier.ViewModel;
using System.Windows;

namespace DirectoryCopier.View
{

    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}