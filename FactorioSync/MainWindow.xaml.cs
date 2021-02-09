using System.Windows;
using System;

namespace FactorioSync
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string[]  _args = Environment.GetCommandLineArgs();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if ((_args.Length>1) && _args[1] == "/silent")
            {
                BtnGo_Click(sender, e);
            }
        }

        private void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            BtnReset_Click(sender, e);
            ZacFunctions.CopyFile(TxtSource.Text.ToString(), TxtDestination.Text.ToString(), listBox);
            ZacFunctions.CopyFile(TxtDestination.Text.ToString(), TxtSource.Text.ToString(), listBox);


            if ((_args.Length > 1) && _args[1] == "/silent")
            {
                this.Close();
            }
        }
    }
}
