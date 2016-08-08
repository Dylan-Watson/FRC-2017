using System.ComponentModel;
using System.Threading;

namespace Dashboard2017
{
    /// <summary>
    /// A background worker that can be aborted
    /// </summary>
    public class AbortableBackgroundWorker : BackgroundWorker
    {
        private Thread workerThread;

        /// <summary>
        /// Method called by the worker
        /// </summary>
        /// <param name="e">DoWorkEventArgs</param>
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            workerThread = Thread.CurrentThread;
            try
            {
                base.OnDoWork(e);
            }
            catch (ThreadAbortException)
            {
                e.Cancel = true; //We must set Cancel property to true!
                Thread.ResetAbort(); //Prevents ThreadAbortException propagation
            }
        }

        /// <summary>
        /// Aborts the worker
        /// </summary>
        public void Abort()
        {
            if (workerThread == null) return;
            workerThread.Abort();
            workerThread = null;
        }
    }
}