﻿using System.Windows;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows.Controls;
using FactorioSync.Annotations;

namespace FactorioSync
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _source;
        private string _destination;

        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
                OnPropertyChanged(nameof(Source));
            }
        }

        public string Destination
        {
            get
            {
                return _destination;
            }
            set
            {
                _destination = value;
                OnPropertyChanged(nameof(Destination));
            }
        }


        private readonly string[]  _args = Environment.GetCommandLineArgs();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Source = @"C:\Users\C.ROUXEL\AppData\Roaming\Factorio\saves";
            Destination = @"C:\Users\C.ROUXEL\Dropbox\__FACTORIO_saved_games";
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBtnGo();

            if ((_args.Length>1) && _args[1] == "/silent")
            {
                BtnGo_Click(sender, e);
            }
        }

        private bool CheckBtnGo()
        {
            bool retour;

            bool isSourceExist = Directory.Exists(Source);
            bool isDestinationExist = Directory.Exists(Destination);

            BtnGo.IsEnabled = retour = (isSourceExist && isDestinationExist);

            return retour;
        }

        private void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            BtnReset_Click(sender, e);
            ZacFunctions.CopyFile(Source, Destination, listBox);
            ZacFunctions.CopyFile(Destination, Source, listBox);


            if ((_args.Length > 1) && _args[1] == "/silent" && CheckBtnGo())
            {
                this.Close();
            }
        }

        private void TxtSource_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CheckBtnGo();
        }

        private void TxtDestination_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CheckBtnGo();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
