/****************************** Header ******************************\
Class Name:
Summary:
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

namespace Dashboard2017
{
    using Dashboard2017.Properties;
    using Emgu.CV;
    using Emgu.CV.CvEnum;
    using Emgu.CV.Structure;
    using Emgu.CV.UI;
    using Emgu.CV.Util;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.ExceptionServices;
    using System.Security;

    public enum CaptureType
    {
        Usb,
        Network,
        Local,
        Test = -1
    }

    public class FeedHandler : IDisposable
    {
        private readonly BackgroundWorker bw;
        private readonly ImageBox destCompositOutputImage;
        private readonly ImageBox destOutputImage;
        private readonly Mat logo = CvInvoke.Imread(@"defaultFeed.jpg", LoadImageType.Color);
        private readonly object source;
        private readonly bool terminate = false;
        private Capture capture;
        private Mat temp;

        public FeedHandler(int source, ImageBox normal, ImageBox composit)
        {
            this.source = source;
            capture = new Capture(source);
            destOutputImage = normal;
            destCompositOutputImage = composit;

            bw = new BackgroundWorker { WorkerSupportsCancellation = true };
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
        }

        public FeedHandler(string source, ImageBox normal, ImageBox composit)
        {
            this.source = source;
            capture = new Capture(source);
            destOutputImage = normal;
            destCompositOutputImage = composit;

            bw = new BackgroundWorker { WorkerSupportsCancellation = true };
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
        }

        /// <summary>
        ///     boolean to enable and disable targeting
        /// </summary>
        public bool Targeting { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Refresh()
        {
            capture.Dispose();
            if (source is int)
                capture = new Capture((int)source);
            else
                capture = new Capture((string)source);
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!terminate)
                Update();
        }

        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        private void Update()
        {
            try
            {
                temp = capture.QueryFrame();

                if (Targeting && (temp != null))
                {
                    var output = ProcessImage(temp);
                    destOutputImage.Invoke(new Action(() => destOutputImage.Image = output.Item1));
                    destOutputImage.Invoke(new Action(() => destCompositOutputImage.Image = output.Item2));
                }
                else if (temp != null)
                {
                    destOutputImage.Invoke(new Action(() => destOutputImage.Image = temp));
                    destOutputImage.Invoke(new Action(() => destCompositOutputImage.Image = logo));
                }
                else
                {
                    destOutputImage.Invoke(new Action(() => destOutputImage.Image = logo));
                    destOutputImage.Invoke(new Action(() => destCompositOutputImage.Image = logo));
                }
            }
            catch (Exception)
            {
                temp?.Dispose();
            }
        }

        //This is where frames are proccessed
        private Tuple<Mat, Image<Gray, byte>> ProcessImage(Mat original)
        {
            if (original == null) return new Tuple<Mat, Image<Gray, byte>>(null, null);

            var hsvImage = original.ToImage<Hsv, byte>();
            var lowerLimit = new Hsv(Settings.Default.lowerHue, Settings.Default.lowerSaturation,
                Settings.Default.lowerBrightness);
            var upperLimit = new Hsv(Settings.Default.upperHue, Settings.Default.upperSaturation,
                Settings.Default.upperBrightness);
            var imageHsvDest = hsvImage.InRange(lowerLimit, upperLimit);

            var circles = new List<CircleF>();

            //find contours and draw smallest possible circle around it
            using (var contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(hsvImage.InRange(lowerLimit, upperLimit), contours, null, RetrType.List,
                    ChainApproxMethod.ChainApproxSimple);
                var contourSize = contours.Size;
                if (contourSize > 2)
                    for (var i = 0; i < contourSize; i++)
                    {
                        var circle = CvInvoke.MinEnclosingCircle(contours[i]);
                        if ((circle.Area > 3200) && (circle.Area < 25000))
                            circles.Add(circle);
                    }
            }

            if (circles.Count == 0) return new Tuple<Mat, Image<Gray, byte>>(original, imageHsvDest);
            if (circles.Count == 0)
                return new Tuple<Mat, Image<Gray, byte>>(original, imageHsvDest);
            //if there are no good targets, return the frame unaltered

            var sortedCircles = circles.OrderBy(o => o.Area).ToList();
            sortedCircles.Reverse(); //reverse to largets to smallest

            var xCentre = original.Size.Width / 2;
            var yCentre = original.Size.Height / 2;
            var largestAndClosest = sortedCircles[0]; //where our final circle will be stored

            if (sortedCircles.Count != 1)
                foreach (var cir in from cir in sortedCircles
                                    let offset = cir.Center.X - xCentre
                                    let offset2 = largestAndClosest.Center.X - xCentre
                                    let offsetY = cir.Center.Y - yCentre
                                    let offsetY2 = largestAndClosest.Center.Y - yCentre
                                    where (offset < offset2)
                                          || ((offsetY < offsetY2) && !(offset > offset2))
                                    select cir) largestAndClosest = cir;

            CvInvoke.Circle(original, new Point((int)largestAndClosest.Center.X, (int)largestAndClosest.Center.Y),
                (int)largestAndClosest.Radius, new MCvScalar(0, 0, 255), 2, LineType.AntiAlias);

            /* if (((int)largestAndClosest.Center.X - xCentre) >= -16 && ((int)largestAndClosest.Center.X - xCentre) <= 9)
                         parent.UpdateOffsetLabel(((int)largestAndClosest.Center.X - xCentre), System.Windows.Media.Brushes.LimeGreen);
                     else
                         parent.UpdateOffsetLabel(((int)largestAndClosest.Center.X - xCentre), System.Windows.Media.Brushes.Red);

                     if (((int)largestAndClosest.Radius) >= 61 && ((int)largestAndClosest.Radius) <= 72)
                         parent.UpdateRadiusLabel(((int)largestAndClosest.Radius), System.Windows.Media.Brushes.LimeGreen);
                     else
                         parent.UpdateRadiusLabel(((int)largestAndClosest.Radius), System.Windows.Media.Brushes.Red);*/

            //TableWrapper.Instance.Table.PutNumber("VISION_OFFSET", ((int)largestAndClosest.Center.X - xCentre));
            //TableWrapper.Instance.Table.PutNumber("CIRCLE_RADIUS", (int)largestAndClosest.Radius);

            return new Tuple<Mat, Image<Gray, byte>>(original, imageHsvDest);
        }

        ~FeedHandler()
        {
            Dispose(false);
        }

        [HandleProcessCorruptedStateExceptions]
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            bw.Dispose();
            capture.Dispose();
        }
    }
}