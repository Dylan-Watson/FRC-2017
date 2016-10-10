using Emgu.CV;
using System;
using System.Drawing;
using System.Globalization;

namespace Dashboard2017
{
    public class VideoWriterManager : IDisposable
    {
        #region Private Constructors

        private VideoWriterManager()
        {
        }

        #endregion Private Constructors

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            Writer?.Dispose();
        }

        #endregion Protected Methods

        #region Private Fields

        private static VideoWriterManager instance;

        private static Size size;

        private readonly VideoWriter Writer =
            new VideoWriter(
                @"Dashboard_2017" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss", CultureInfo.CurrentCulture) + ".avi",
                24, size, true);

        #endregion Private Fields

        #region Public Properties

        public static VideoWriterManager Instance => instance ?? (instance = new VideoWriterManager());

        public bool Record { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void WriteFrame(Mat frame)
        {
            if (frame == null) return;
            size = new Size(frame.Width, frame.Height);
            Writer.Write(frame);
        }

        #endregion Public Methods
    }
}