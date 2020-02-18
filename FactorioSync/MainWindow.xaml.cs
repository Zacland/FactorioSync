using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            string[] originalFiles = Directory.GetFiles(TxtSource.Text, "*", SearchOption.AllDirectories);

            Array.ForEach(originalFiles, (originalFileLocation) =>
            {
                FileInfo originalFile = new FileInfo(originalFileLocation);
                FileInfo destFile = new FileInfo(originalFileLocation.Replace(TxtSource.Text, TxtDestination.Text));

                if (destFile.Exists)
                {
                    if (originalFile.Length > destFile.Length)
                    {
                        originalFile.CopyTo(destFile.FullName, true);
                    }
                }
                else
                {
                    Directory.CreateDirectory(destFile.DirectoryName);
                    originalFile.CopyTo(destFile.FullName, false);
                }
            });
        }
    }
}
