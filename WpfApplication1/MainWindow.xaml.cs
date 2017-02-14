using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Xml;

namespace WpfApplication1
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Public Constructors

        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
        }

        #endregion Public Constructors

        #region Private Properties

        private bool beenSaved { get; set; }

        private string filePath { get; set; }
        private bool recentSaved { get; } = false;

        #endregion Private Properties

        #region Public Methods

        public void saveFile()
        {
            if (!beenSaved)
            {
                var dlg = new SaveFileDialog();
                dlg.FileName = "robot_config"; // Default file name
                dlg.DefaultExt = ".xml"; // Default file extension
                dlg.Filter = "XML Files (.xml)|*.xml"; // Filter files by extension

                // Show save file dialog box
                var result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result != true)
                    return;
                filePath = dlg.FileName;
            }
            try
            {
                // Save document
                var doc = new XmlDocument();
                string content =
                    $@"<?xml version=""1.0"" encoding=""UTF-8""?>{new TextRange(MainEditor.Document.ContentStart,
                        MainEditor.Document.ContentEnd).Text}";
                doc.LoadXml(content);
                //validate here
                var writer = new XmlTextWriter(filePath, Encoding.UTF8) {Formatting = Formatting.Indented};
                doc.WriteTo(writer);

                writer.Close();
                beenSaved = true;
            }
            catch (Exception ex)
            {
                var result =
                    MessageBox.Show($"XML is not Valid.\nThe file will not save!\ndo you want to see the error?",
                        "Warning", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                    MessageBox.Show($"ERROR:\n{ex.Message}", "Error", MessageBoxButton.OK);
            }
        }

        public bool w3SpecCheck()
        {
            return true;
        }

        #endregion Public Methods

        #region Private Methods

        private void BuildButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private bool checkClose()
        {
            if (!recentSaved || !beenSaved)
            {
                var result = MessageBox.Show("You have unsaved changes. Do you want to save?", "Confirmation",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Cancel)
                    return false;
                if (result == MessageBoxResult.No)
                    return true;
                //    saveFile();
                return true;
            }
            return true;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!checkClose())
                e.Cancel = true;
            else
                Environment.Exit(0);
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            saveFile();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var set = new Settings(this);
            set.Show();
            IsEnabled = false;
        }

        #endregion Private Methods
    }
}