using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace WpfApplication1
{
    /// <summary>
    ///     Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        #region Private Fields

        private readonly Window main;

        #endregion Private Fields

        #region Public Constructors

        public Settings(Window sender)
        {
            InitializeComponent();
            populatefields();
            main = sender;
            Closed += Settings_Closed;
        }

        #endregion Public Constructors

        #region Private Methods

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void IPBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Prohibit non-numeric
            if (!IsNumeric(e.Text))
                e.Handled = true;
        }

        private bool IsNumeric(string text)
        {
            var regex = new Regex("^[0-9]+$");
            return regex.IsMatch(text);
        }

        private void populatefields()
        {
            var IP = Properties.Settings.Default.IP.Split('.');
            IPTextBox1.Text = IP[0];
            IPTextBox2.Text = IP[1];
            IPTextBox3.Text = IP[2];
            IPTextBox4.Text = IP[3];
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            saveSettings();
            Close();
        }

        private void saveSettings()
        {
            if (IPTextBox1.Text == "" || IPTextBox2.Text == "" || IPTextBox3.Text == "" || IPTextBox4.Text == "")
            {
                var result = MessageBox.Show("You have unsaved changes. Do you want to save?", "Confirmation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                    return;
            }
            string IP = $"{IPTextBox1.Text}.{IPTextBox2.Text}.{IPTextBox3.Text}.{IPTextBox4.Text}";
            Properties.Settings.Default.IP = IP;
        }

        private void Settings_Closed(object sender, EventArgs e)
        {
            if (main != null)
                main.IsEnabled = true;
        }

        #endregion Private Methods
    }
}