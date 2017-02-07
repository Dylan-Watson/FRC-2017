using System;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            if (checkClose())
                Environment.Exit(0);
        }

        #region Event Handlers
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings set = new Settings(this);
            set.Show();
            IsEnabled = false;
        }

        private void BuildButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

        #region Methods
        //will put save checking here
        private bool checkClose()
        {
            return true;
        }

        #endregion
    }
}
