using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using ThreadState = System.Diagnostics.ThreadState;
using Timer = WPILib.Timer;

namespace FRCSimulator
{
    /// <summary>
    ///     ThreadStates
    /// </summary>
    public partial class ThreadStates : Form
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public ThreadStates()
        {
            InitializeComponent();
            Shown += THreadStates_Shown;
            richTextBox1.Enabled = false;
        }

        private void THreadStates_Shown(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                while (true)
                {
                    updateTextBox();
                    Timer.Delay(0.5);
                }
            }).Start();
        }

        private void updateTextBox()
        {
            richTextBox1.Invoke((MethodInvoker) (() =>
            {
                richTextBox1.Clear();
                richTextBox1.AppendText($"Total number of threads = {Process.GetCurrentProcess().Threads.Count}\n");
                foreach (ProcessThread thread in Process.GetCurrentProcess().Threads)
                    richTextBox1.AppendText(thread.ThreadState == ThreadState.Wait
                        ? $"Thread {thread.Id} state = {thread.ThreadState} \t reason = {thread.WaitReason}\n"
                        : $"Thread {thread.Id} state = {thread.ThreadState}\n");
            }));
        }
    }
}