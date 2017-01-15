/****************************** Header ******************************\
Class Name: ConsoleManager [singleton]
Summary: Manages the console for the Dashboard
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoftware.co
\********************************************************************/

using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Dashboard2017
{
    /// <summary>
    ///     Class to manage the console
    /// </summary>
    public class ConsoleManager : TextWriter
    {
        #region Private Constructors

        private ConsoleManager()
        {
        }

        #endregion Private Constructors

        #region Private Fields

        private static ConsoleManager _instance;

        private RichTextBox console;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        ///     The encoding to be used => set to UTF-8
        /// </summary>
        public override Encoding Encoding => Encoding.UTF8;

        /// <summary>
        ///     Method to return the instance of this class, or a new instance if this instance is null
        /// </summary>
        public static ConsoleManager Instance => _instance ?? (_instance = new ConsoleManager());

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Append an error to the console
        /// </summary>
        /// <param name="ex">Exception to append</param>
        /// <param name="newLine">Boolean to make a newLine upon appending => defaults to true</param>
        public void AppendError(Exception ex, bool newLine = true)
        {
            if (!console.IsDisposed)
                console.Invoke(new Action(() => { appendText(ex.Message, newLine, Color.Red); }));
        }

        /// <summary>
        ///     Append an error to the console
        /// </summary>
        /// <param name="ex">Exception to append as a string</param>
        /// <param name="newLine">Boolean to make a newLine upon appending => defaults to true</param>
        public void AppendError(string ex, bool newLine = true)
        {
            if (!console.IsDisposed)
                console.Invoke(new Action(() => { appendText(ex, newLine, Color.Red); }));
        }

        /// <summary>
        ///     Append some information to the console
        /// </summary>
        /// <param name="info">Information to append</param>
        /// <param name="clr">Color to style the information</param>
        /// <param name="newLine">Boolean to make a newLine upon appending => defaults to true</param>
        public void AppendInfo(string info, Color clr, bool newLine = true)
        {
            if (!console.IsDisposed)
                console.Invoke(new Action(() => { appendText(info, newLine, clr); }));
        }

        /// <summary>
        ///     Append some information to the console -- default information coloring to black
        /// </summary>
        /// <param name="info">Information to append</param>
        /// <param name="newLine">Boolean to make a newLine upon appending => defaults to true</param>
        public void AppendInfo(string info, bool newLine = true)
        {
            if (!console.IsDisposed)
                console.Invoke(new Action(() => { appendText(info, newLine, Color.Black); }));
        }

        /// <summary>
        ///     Method to set the console manager
        /// </summary>
        /// <param name="consoleRichTextBox">RichTextBox to set as the console manager</param>
        public void SetConsoleManager(RichTextBox consoleRichTextBox)
        {
            console = consoleRichTextBox;
            console.TextChanged += Console_TextChanged;
        }

        /// <summary>
        ///     Method to write a character to the console
        /// </summary>
        /// <param name="value">Character to write to the console</param>
        public override void Write(char value)
        {
            base.Write(value);
            if (!console.IsDisposed)
                console.Invoke(new Action(() => { console.AppendText(value.ToString()); }));
        }

        #endregion Public Methods

        #region Private Methods

        private void appendText(string str, bool newLine, Color clr)
        {
            console.SelectionStart = console.TextLength;
            console.SelectionLength = 0;
            console.SelectionColor = clr;
            if (newLine)
                console.AppendText(str + "\n");
            else
                console.AppendText(str);
            console.SelectionColor = Color.Black;
        }

        private void Console_TextChanged(object sender, EventArgs e)
        {
            if (!console.IsDisposed)
                console.Invoke(new Action(() =>
                {
                    console.SelectionStart = console.Text.Length;
                    console.ScrollToCaret();
                }));
        }

        #endregion Private Methods
    }
}