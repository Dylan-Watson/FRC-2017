using System;
using System.Windows;
using Microsoft.Win32;
using System.Xml;
using System.Text;
using System.Windows.Documents;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool beenSaved { get; set; } = false;

        private bool recentSaved { get; set; } = false;

        private string filePath { get; set; } = null;

        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            saveFile();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
     /*       try {
                beenSaved = true;
                recentSaved = true;
            }

            catch (Exception ex) {
            }*/
        }

        #endregion

        #region Methods

        public void saveFile() {
            if (!beenSaved) {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "robot_config"; // Default file name
                dlg.DefaultExt = ".xml"; // Default file extension
                dlg.Filter = "XML Files (.xml)|*.xml"; // Filter files by extension

                // Show save file dialog box
                bool? result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result != true)
                    return;
                filePath = dlg.FileName;
            }
            try
            {
                // Save document
                XmlDocument doc = new XmlDocument();
                string content = $@"<?xml version=""1.0"" encoding=""UTF-8""?>{new TextRange(MainEditor.Document.ContentStart, MainEditor.Document.ContentEnd).Text}";
                doc.LoadXml(content);
                //validate here
                XmlTextWriter writer = new XmlTextWriter(filePath, Encoding.UTF8) { Formatting = Formatting.Indented };
                doc.WriteTo(writer);

                writer.Close();
                beenSaved = true;
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show($"XML is not Valid.\nThe file will not save!\ndo you want to see the error?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                    MessageBox.Show($"ERROR:\n{ex.Message}", "Error", MessageBoxButton.OK);
            }
        }

        public bool w3SpecCheck()
        {

            return true;
        }

        private bool checkClose()
        {
            if(!recentSaved || !beenSaved){
                MessageBoxResult result = MessageBox.Show("You have unsaved changes. Do you want to save?", "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Cancel)
                    return false;
                else if (result == MessageBoxResult.No)
                    return true;
                else
                {
                //    saveFile();
                    return true;
                }

            }
            else
                return true;
        }

        #endregion

    }
}
