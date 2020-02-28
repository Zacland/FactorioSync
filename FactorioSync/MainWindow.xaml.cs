using System.Windows;
using System.Drawing;
using System.Windows.Media;

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
            BtnReset_Click(sender, e);
            ZacFunctions.CopyFile(TxtSource.Text.ToString(), TxtDestination.Text.ToString(), listBox);
            ZacFunctions.CopyFile(TxtDestination.Text.ToString(), TxtSource.Text.ToString(), listBox);
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
        }
    }
}
