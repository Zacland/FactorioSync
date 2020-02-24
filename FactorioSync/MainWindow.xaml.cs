using System.Windows;


namespace FactorioSync
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ZacFunctions.CopyFile(TxtSource.Text.ToString(), TxtDestination.Text.ToString());
            ZacFunctions.CopyFile(TxtDestination.Text.ToString(), TxtSource.Text.ToString());
        }
    }
}
