using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        #region Constructors

        public Settings()
        {
            InitializeComponent();
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
