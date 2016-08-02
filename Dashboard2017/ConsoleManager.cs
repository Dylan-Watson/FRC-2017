using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Dashboard2017
{
    public class ConsoleManager : TextWriter
    {
        private static ConsoleManager instance;

        private RichTextBox console;

        private ConsoleManager()
        {
        }

        public override Encoding Encoding => Encoding.UTF8;

        public static ConsoleManager Instance => instance ?? (instance = new ConsoleManager());

        public void SetConsoleManager(RichTextBox consoleRichTextBox)
        {
            console = consoleRichTextBox;
            console.TextChanged += Console_TextChanged;
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

        public void AppendError(Exception ex, bool newLine = true)
        {
            if (!console.IsDisposed)
                console.Invoke(new Action(() => { appendText(ex.Message, newLine, Color.Red); }));
        }

        public void AppendError(string ex, bool newLine = true)
        {
            if (!console.IsDisposed)
                console.Invoke(new Action(() => { appendText(ex, newLine, Color.Red); }));
        }

        public void AppendInfo(string info, Color clr, bool newLine = true)
        {
            if (!console.IsDisposed)
                console.Invoke(new Action(() => { appendText(info, newLine, clr); }));
        }

        public void AppendInfo(string info, bool newLine = true)
        {
            if (!console.IsDisposed)
                console.Invoke(new Action(() => { appendText(info, newLine, Color.Black); }));
        }

        public override void Write(char value)
        {
            base.Write(value);
            if (!console.IsDisposed)
                console.Invoke(new Action(() => { console.AppendText(value.ToString()); }));
        }
    }
}