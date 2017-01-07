using System.Windows.Forms;

namespace FRCSimulator
{
    using System.Diagnostics;
    using System.Threading;
    using ThreadState = System.Diagnostics.ThreadState;
    using Timer = WPILib.Timer;

    /// <summary>
    ///     ThreadStates
    /// </summary>
    public partial class THreadStates : Form
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public THreadStates()
        {
            InitializeComponent();
            Shown += THreadStates_Shown;
            richTextBox1.Enabled = false;
        }

        private void THreadStates_Shown(object sender, System.EventArgs e)
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
