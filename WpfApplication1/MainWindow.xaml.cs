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
            Closing += MainWindow_Closing; ;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!checkClose())
                e.Cancel = true;
            else
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
            //Check if there is unsaved changes
            MessageBoxResult result = MessageBox.Show("You have unsaved changes. Do you want to save?", "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Cancel)
                return false;
            /*
            If no, return true
            If yes, ask if want to save
                If yes, save
                    Then, return true
                If no, return true
                If cancel, return false
             */
            return true;
        }

        #endregion
    }
}
