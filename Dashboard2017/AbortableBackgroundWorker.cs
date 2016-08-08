using System.ComponentModel;
using System.Threading;

namespace Dashboard2017
{
    /// <summary>
    /// A background worker that can be aborted
    /// </summary>
    public class AbortableBackgroundWorker : BackgroundWorker
    {
        #region Private Fields

        private Thread workerThread;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Aborts the worker
        /// </summary>
        public void Abort()
        {
            if (workerThread == null) return;
            workerThread.Abort();
            workerThread = null;
        }

        #endregion Public Methods

        #region Protected Methods

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

        #endregion Protected Methods
    }
}