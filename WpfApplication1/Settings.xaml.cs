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

        /// <summary>
        ///     The window that is calling this
        /// </summary>
        private readonly Window main;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="sender">The window invoking this class</param>
        public Settings(Window sender)
        {
            InitializeComponent();
            populatefields();
            main = sender;
            Closed += Settings_Closed;
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        ///     Event handler for the clicking of the cancel button
        /// </summary>
        /// <param name="sender">Object invoking this</param>
        /// <param name="e">The event arguments passed</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Event Handler for typing in the numbers in the IP Adress Box
        ///     This simply checks to only allow numerical values
        /// </summary>
        /// <param name="sender">Object invoking this</param>
        /// <param name="e">The event arguments passed</param>
        private void IPBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Prohibit non-numeric
            if (!IsNumeric(e.Text))
                e.Handled = true;
        }

        /// <summary>
        ///     Utility method to check for numeric values
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool IsNumeric(string text)
        {
            var regex = new Regex("^[0-9]+$");
            return regex.IsMatch(text);
        }

        /// <summary>
        ///     Utility method to poplate the IP adress fields with the current address
        /// </summary>
        private void populatefields()
        {
            var IP = Properties.Settings.Default.IP.Split('.');
            IPTextBox1.Text = IP[0];
            IPTextBox2.Text = IP[1];
            IPTextBox3.Text = IP[2];
            IPTextBox4.Text = IP[3];
        }

        /// <summary>
        ///     Event Handler for the save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            saveSettings();
            Close();
        }

        /// <summary>
        ///     Utility method to save the current settings
        /// </summary>
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

        /// <summary>
        ///     Event handler to re-activate the main window when the settings window is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_Closed(object sender, EventArgs e)
        {
            if (main != null)
                main.IsEnabled = true;
        }

        #endregion Private Methods
    }
}