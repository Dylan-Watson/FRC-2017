using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        #region Private Variables

        #endregion

        private Window main = null;

        #region Constructors

        public Settings(Window sender)
        {
            InitializeComponent();
            main = sender;
            Closed += Settings_Closed;
        }

        private void Settings_Closed(object sender, EventArgs e)
        {
            if (main != null)
                main.IsEnabled = true;
        }

        #endregion

        #region Events

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            saveSettings();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

        #region Methods

        private void saveSettings()
        {

        }

        private void IPBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Prohibit non-numeric
            if (!IsNumeric(e.Text))
                e.Handled = true;
        }

        private bool IsNumeric(string text)
        {
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(text);
        }

        #endregion
    }
}
