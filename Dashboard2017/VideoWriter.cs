using Emgu.CV;
using System;
using System.Drawing;
using System.Globalization;

namespace Dashboard2017
{
    public class VideoWriterManager : IDisposable
    {
        private static Size size;

        private static VideoWriterManager instance;

        private readonly VideoWriter Writer =
            new VideoWriter(
                @"Dashboard_2017" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss", CultureInfo.CurrentCulture) + ".avi",
                24, size, true);

        private VideoWriterManager()
        {
        }

        public bool Record { get; set; }

        public static VideoWriterManager Instance => instance ?? (instance = new VideoWriterManager());

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

        protected virtual void Dispose(bool disposing)
        {
            Writer?.Dispose();
        }
    }
}