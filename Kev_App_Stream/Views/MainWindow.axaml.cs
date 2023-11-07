using Avalonia.Controls;

namespace Kev_App_Stream.Views
{
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public TopLevel topLevel;
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            topLevel = this;
        }
    }
}