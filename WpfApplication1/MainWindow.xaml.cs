using ICSharpCode.AvalonEdit.Highlighting;
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
using System.Windows.Threading;
using System.Xml;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Instance Vars

        /// <summary>
        ///     Boolean property to determine if the current file has ever been saved
        /// </summary>
        private bool beenSaved { get; set; } = false;
        
        /// <summary>
        ///     The full system file path of the current file
        /// </summary>
        private string filePath { get; set; } = null;

        /// <summary>
        ///     Boolean property to determine if the current file has been saved with all current changes
        /// </summary>
        private bool recentSaved { get; set; } = false;

        /// <summary>
        ///     The message returned by the validation checker
        /// </summary>
        private string validationMessage { get; set; }

        /// <summary>
        ///     The loading window to show while connecting to the RoboRIO
        /// </summary>
        private Loading load;

        #endregion

        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            
            ///Set the event handler for the window closing event
            Closing += MainWindow_Closing;

            ///Set up the custom XMl Highlighting definition
            using (StreamReader s = new StreamReader(@"XML.xshd"))
            {
                using (XmlTextReader reader = new XmlTextReader(s))
                {
                    MainEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }

            ///Set up the custom key bindings
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

        /// <summary>
        ///     Event handler for the build button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuildButton_Click(object sender, RoutedEventArgs e)
        {
            ///Check to see if the XML is valid
            bool valid = buildFile();

            ///Display the results
            if (valid)
                MessageBox.Show("You're all set!", "Valid", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (!valid)
                MessageBox.Show($"Build Failed!\nERROR:\n{validationMessage}", "Not Valid", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        ///     Event handler for the exit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Event handler to manage the recent saved property whenever the textbox content is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainEditor_TextChanged(object sender, EventArgs e)
        {
            recentSaved = false;
        }

        /// <summary>
        ///     Event handler to check for unsaved changed when the window closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {   
            ///Cancel the closing of the window
            if (!checkClose())
                e.Cancel = true;

            ///Close the window
            else
                Environment.Exit(0);
        }

        /// <summary>
        ///     Event handler for the open file button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            openFile();
        }

        /// <summary>
        ///     Event handler for the save file button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            saveFile();
        }

        /// <summary>
        ///     Event handler for the open settings window button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ///Open the settings window
            Settings set = new Settings(this);
            set.ShowDialog();

            ///Deactivate this window
            IsEnabled = false;
        }
        
        /// <summary>
        ///     Event handler for the upload file button
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            uploadFile();
        }

        /// <summary>
        ///     Event handler for the download file button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            getFile();
        }

        /// <summary>
        ///     Event handler for the upload file background worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Helpme_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            load.Close();
        }

        /// <summary>
        ///     Event handler for the DoWork function in the upload file
        ///     BackgroundWorker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Helpme_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ///Initiate an FTP request
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://{Properties.Settings.Default.IP}/config.xml");
                request.Timeout = 10000;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("anonymous", "");

                ///Open a StreamReader to the filePath
                StreamReader sourceStream = new StreamReader(filePath);
                ///Get the contents of the file
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;
                
                ///Open a Stream to the RoboRIO and upload the file
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                ///Get a response from the RoboRIO
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                response.Close();
                MessageBox.Show($"{response.StatusDescription}", "Upload", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception thrown in connection\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show($"Upload Failed", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Event handler for the DoWork function in the download file BackgroundWorker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ///Initiate an FTP request
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://{Properties.Settings.Default.IP}/config.xml");
                request.Timeout = 10000;
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential("anonymous", "");

                ///Get the XML Text of the file on the RoboRIO
                MainEditor.Dispatcher.Invoke(DispatcherPriority.Normal,
                        new Action(() => { MainEditor.Clear(); MainEditor.AppendText(ReadLines(request.GetResponse().GetResponseStream())); }));

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                response.Close();
                MessageBox.Show($"{response.StatusDescription}", "Upload", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception thrown in connection\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show($"Upload Failed", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Utility method to handle validating the file
        /// </summary>
        /// <returns>IsXMLValid</returns>
        private bool buildFile()
        {
            ///Get the current content of the textbox
            XmlDocument doc = new XmlDocument();
            string content = MainEditor.Text;
            try
            {
                ///NOTE: This will validate the XML against the XML Spec
                doc.LoadXml(content);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Build failed on account of invalid XML by W3 Spec\nERROR: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            ///Save the file
            if (saveFile())
            {
                XmlElement root = doc.DocumentElement;
                if (doc.FirstChild.NodeType != XmlNodeType.XmlDeclaration)
                {
                    XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    doc.InsertBefore(dec, root);
                }
                ///Test the XML against the custom config
                Tuple<string, Exception> e = Build.BuildFile(filePath);
                if (e.Item2 != null)
                {
                    MessageBoxResult answer =  MessageBox.Show($"Custom Build Failed!\nERROR: {e.Item1}\n{e.Item2.Message}\n\nWould you like to see the exception?", "Custom Error", MessageBoxButton.YesNo, MessageBoxImage.Error);
                    if (answer == MessageBoxResult.Yes)
                        MessageBox.Show($"{e.Item2}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                    return true;
            }
            return false;
        }

        /// <summary>
        ///     Utility method to handle checking for saved changes
        /// </summary>
        /// <returns>IsRecentlySaved</returns>
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

        /// <summary>
        ///     Utility method to handle opening a file
        /// </summary>
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
                    if (checkClose())
                    {
                        StreamReader str = FileReader.OpenFile(filePath, Encoding.UTF8);
                        MainEditor.Text = str.ReadToEnd();
                        str.Close();
                        beenSaved = true;
                        recentSaved = true;
                        Title = $"{filePath}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the file.\nERROR: {ex.Message}", "Error", MessageBoxButton.OK);
            }

        }

        /// <summary>
        ///     Utility method to handle saving a file
        /// </summary>
        /// <returns>IsFileSaved</returns>
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
        
        /// <summary>
        ///     Utility method to handle uploading a file to the RoboRIO
        /// </summary>
        public void uploadFile()
        {
            if (buildFile())
            {
                load = new Loading();
                BackgroundWorker helpme = new BackgroundWorker();
                helpme.DoWork += Helpme_DoWork;
                helpme.RunWorkerCompleted += Helpme_RunWorkerCompleted;
                helpme.RunWorkerAsync();
                load.ShowDialog();
            }
        }

        /// <summary>
        ///     Utility method to handle downloading 
        ///     a file from the RoboRIO
        /// </summary>
        public void getFile()
            {
                if (!checkClose())
                    return;
                load = new Loading();
                BackgroundWorker help = new BackgroundWorker();
                help.DoWork += Help_DoWork;
                help.RunWorkerCompleted += Helpme_RunWorkerCompleted;
                help.RunWorkerAsync();
                load.ShowDialog();
            }

        /// <summary>
        ///     Utility method to read all lines from a configuration file 
        ///     and return them
        /// </summary>
        /// <param name="streamProvider">The stream to read the file from</param>
        /// <returns>XML File Lines</returns>
        public string ReadLines(Stream streamProvider)
        {
            using (var reader = new StreamReader(streamProvider))
                return reader.ReadToEnd();
        }

        #endregion

    }
}
