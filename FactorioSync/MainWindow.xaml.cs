using System.Windows;
using System;
using System.Drawing;
using System.Windows.Media;

namespace FactorioSync
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string[]  args = Environment.GetCommandLineArgs();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            BtnReset_Click(sender, e);
            ZacFunctions.CopyFile(TxtSource.Text.ToString(), TxtDestination.Text.ToString(), listBox);
            ZacFunctions.CopyFile(TxtDestination.Text.ToString(), TxtSource.Text.ToString(), listBox);


            if ((args.Length>1) && args[1] == "/silent")
            {
                this.Close();
            }
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if ((args.Length>1) && args[1] == "/silent")
            {
                button_Click(sender, e);
            }
        }
    }
}
