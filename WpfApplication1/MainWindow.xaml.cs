﻿using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Utils;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Schema;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Instance Vars

        private bool beenSaved { get; set; } = false;
        
        private string filePath { get; set; } = null;

        private bool recentSaved { get; set; } = false;

        private string validationMessage { get; set; }

        #endregion

        #region Public Constructors

        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            using (StreamReader s = new StreamReader(@"XML.xshd"))
            {
                using (XmlTextReader reader = new XmlTextReader(s))
                {
                    MainEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            KeyBinding SaveCmdKeyBinding = new KeyBinding(ApplicationCommands.Save, Key.S, ModifierKeys.Control);
            KeyBinding OpenCmdKeyBinding = new KeyBinding(ApplicationCommands.Open, Key.S, ModifierKeys.Control);
            CommandBinding sv = new CommandBinding(ApplicationCommands.Save);
            sv.Executed += new ExecutedRoutedEventHandler(SaveButton_Click);
            CommandBinding op = new CommandBinding(ApplicationCommands.Open);
            op.Executed += new ExecutedRoutedEventHandler(OpenButton_Click);
            CommandBindings.Add(op);
            CommandBindings.Add(sv);

        }

        #endregion

        #region Event Handlers

        private void BuildButton_Click(object sender, RoutedEventArgs e)
        {
            bool valid = buildFile();
            if (valid)
                MessageBox.Show("You're all set!", "Valid", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (!valid)
                MessageBox.Show($"Build Failed!\nERROR:\n{validationMessage}", "Not Valid", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MainEditor_TextChanged(object sender, EventArgs e)
        {
            recentSaved = false;
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
            openFile();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            saveFile();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings set = new Settings(this);
            set.Show();
            IsEnabled = false;
        }
   
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            uploadFile();
        }

        #endregion

        #region Methods

        private bool buildFile()
        {
            XmlDocument doc = new XmlDocument();
            string content = MainEditor.Text;
            try
            {
                doc.LoadXml(content);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Build failed on account of invalid XML by W3 Spec\nERROR: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (saveFile())
            {
                XmlElement root = doc.DocumentElement;
                if (doc.FirstChild.NodeType != XmlNodeType.XmlDeclaration)
                {
                    XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    doc.InsertBefore(dec, root);
                }
                Exception e = Build.BuildFile(filePath);
                if (e != null)
                {
                    MessageBox.Show($"Custom Build Failed!\nERROR: {e.Message}", "Custom Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                    return true;
            }
            return false;
        }

        private bool checkClose()
        {
            if ((!recentSaved || !beenSaved) && MainEditor.Text != "")
            {
                MessageBoxResult result = MessageBox.Show("You have unsaved changes. Do you want to save?", "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Cancel)
                    return false;
                else if (result == MessageBoxResult.No)
                    return true;
                else
                {
                    saveFile();
                    return true;
                }

            }
            else
                return true;
        }

        private void openFile()
        {
            try
            {
                OpenFileDialog openDLG = new OpenFileDialog();
                openDLG.DefaultExt = ".xml";
                openDLG.Filter = "XML Files (.xml)|*.xml";
                bool? result = openDLG.ShowDialog();
                if (result == true)
                {
                    filePath = openDLG.FileName;
                    checkClose();
                    StreamReader str = FileReader.OpenFile(filePath, Encoding.UTF8);
                    MainEditor.Text = str.ReadToEnd();
                    str.Close();
                    beenSaved = true;
                    recentSaved = true;
                    Title = $"{filePath}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the file.\nERROR: {ex.Message}", "Error", MessageBoxButton.OK);
            }

        }

        private bool saveFile()
        {
            if (!beenSaved)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "config"; // Default file name
                dlg.DefaultExt = ".xml"; // Default file extension
                dlg.Filter = "XML Files (.xml)|*.xml"; // Filter files by extension
                // Show save file dialog box
                bool? result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result != true)
                    return false;
                filePath = dlg.FileName;
            }
            try
            {
                XmlDocument doc = new XmlDocument();
                string content = MainEditor.Text;
                doc.LoadXml(content);
                if (doc.FirstChild.NodeType != XmlNodeType.XmlDeclaration)
                {
                    XmlElement root = doc.DocumentElement;
                    doc.InsertBefore(doc.CreateXmlDeclaration("1.0", "UTF-8", null), root);
                }
                XmlTextWriter writer = new XmlTextWriter(filePath, Encoding.UTF8) { Formatting = Formatting.Indented };
                doc.WriteTo(writer);

                writer.Close();

                StringWriter stringWriter = new StringWriter();
                XmlWriter xmlTextWriter = XmlWriter.Create(stringWriter);
                doc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();

                StreamReader str = FileReader.OpenFile(filePath, Encoding.UTF8);
                MainEditor.Text = str.ReadToEnd();
                str.Close();

                recentSaved = true;
                beenSaved = true;
                Title = $"{filePath}";
                return true;
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show($"XML is not Valid.\nThe file will not save!\ndo you want to see the error?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                    MessageBox.Show($"ERROR:\n{ex.Message}", "Error", MessageBoxButton.OK);
                return false;
            }
        }
        
        public void uploadFile()
        {
            //if (buildFile())
            if (saveFile())
            {
                Loading load = new Loading();
                try
                {
                    load.Show();
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://{Properties.Settings.Default.IP}/config.xml");
                    request.Timeout = 60000;
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential("anonymous", "");

                    StreamReader sourceStream = new StreamReader(filePath);
                    byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                    sourceStream.Close();
                    request.ContentLength = fileContents.Length;

                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(fileContents, 0, fileContents.Length);
                    requestStream.Close();

                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                    load.Close();
                    response.Close();
                    MessageBox.Show($"{response.StatusDescription}", "Upload", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception thrown in connection\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    MessageBox.Show($"Upload Failed", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                    load.Close();
                }
            }
        }

        #endregion

    }
}
