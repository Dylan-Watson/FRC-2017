﻿/****************************** Header ******************************\
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
    using Properties;
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
        private readonly Form1 parent;
        private readonly BackgroundWorker bw;
        private readonly ImageBox destCompositOutputImage;
        private readonly ImageBox destOutputImage;
        private readonly Mat logo = CvInvoke.Imread(@"defaultFeed.jpg", LoadImageType.Color);
        private readonly object source;
        private bool terminate, hasTarget;
        private Capture capture;
        private Mat temp;

        public FeedHandler(int source, ImageBox normal, ImageBox composit, Form1 parentForm1)
        {
            this.source = source;
            parent = parentForm1;
            capture = new Capture(source);
            destOutputImage = normal;
            destCompositOutputImage = composit;

            bw = new BackgroundWorker { WorkerSupportsCancellation = true };
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
        }

        public FeedHandler(string source, ImageBox normal, ImageBox composit, Form1 parentForm1)
        {
            this.source = source;
            parent = parentForm1;
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
            terminate = true;
            dispose(true);
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
                update();
        }

        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        private void update()
        {
            try
            {
                temp = capture.QueryFrame();

                if (Targeting && (temp != null))
                {
                    var output = processImage(temp);
                    destOutputImage.Invoke(new Action(() => destOutputImage.Image = output.Item1));
                    destOutputImage.Invoke(new Action(() => destCompositOutputImage.Image = output.Item2));
                    if (VideoWriterManager.Instance.Record)
                        VideoWriterManager.Instance.WriteFrame(temp);
                }
                else if (temp != null)
                {
                    destOutputImage.Invoke(new Action(() => destOutputImage.Image = temp));
                    destOutputImage.Invoke(new Action(() => destCompositOutputImage.Image = logo));
                    if (VideoWriterManager.Instance.Record)
                        VideoWriterManager.Instance.WriteFrame(temp);
                }
                else
                {
                    destOutputImage.Invoke(new Action(() => destOutputImage.Image = logo));
                    destOutputImage.Invoke(new Action(() => destCompositOutputImage.Image = logo));
                    if (VideoWriterManager.Instance.Record)
                        VideoWriterManager.Instance.WriteFrame(temp);
                }
            }
            catch (Exception)
            {
                temp?.Dispose();
            }
        }

        //This is where frames are proccessed
        private Tuple<Mat, Image<Gray, byte>> processImage(Mat original)
        {
            if (original == null) return new Tuple<Mat, Image<Gray, byte>>(null, null);

            var hsvImage = original.ToImage<Hsv, byte>();
            var lowerLimit = new Hsv(Settings.Default.lowerHue, Settings.Default.lowerSaturation,
                Settings.Default.lowerValue);
            var upperLimit = new Hsv(Settings.Default.upperHue, Settings.Default.upperSaturation,
                Settings.Default.upperValue);
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
                        if ((circle.Area > 3500) && (circle.Area < 25000))
                            circles.Add(circle);
                    }
            }

            if (circles.Count == 0)
            {
                parent.NoTarget();
                return new Tuple<Mat, Image<Gray, byte>>(original, imageHsvDest);
            }
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

            if (!hasTarget)
                parent.HasTarget();
            else
                parent.TargetAquired();

            CvInvoke.Circle(original, new Point((int) largestAndClosest.Center.X, (int) largestAndClosest.Center.Y),
                (int) largestAndClosest.Radius, !hasTarget ? new MCvScalar(0, 0, 255) : new MCvScalar(0, 255, 0), 2,
                LineType.AntiAlias);

            parent.UpdateTargetLabels((int)largestAndClosest.Center.X- xCentre, (int)largestAndClosest.Center.Y- yCentre, (int)largestAndClosest.Radius);

            if (((int) largestAndClosest.Center.X - xCentre >= Settings.Default.targetLeftXBound) && ((int) largestAndClosest.Center.X - xCentre <= Settings.Default.targetRightXBound))
            {
                //parent.UpdateOffsetLabel(((int) largestAndClosest.Center.X - xCentre),
                // System.Windows.Media.Brushes.LimeGreen);
                hasTarget = true;
            }
            else
            {
                hasTarget = false;
                //parent.UpdateOffsetLabel(((int) largestAndClosest.Center.X - xCentre), System.Windows.Media.Brushes.Red);
            }

            if (((int) largestAndClosest.Radius >= Settings.Default.targetRadiusLowerBound) && ((int) largestAndClosest.Radius <= Settings.Default.targetRadiusUpperBound))
            {
                //parent.UpdateRadiusLabel(((int) largestAndClosest.Radius), System.Windows.Media.Brushes.LimeGreen);
            }
            else
            {
                hasTarget = false;
                //parent.UpdateRadiusLabel(((int) largestAndClosest.Radius), System.Windows.Media.Brushes.Red);
            }

            TableManager.Instance.Table.PutNumber("VISION_OFFSET", ((int)largestAndClosest.Center.X - xCentre));
            TableManager.Instance.Table.PutNumber("CIRCLE_RADIUS", (int)largestAndClosest.Radius);

            return new Tuple<Mat, Image<Gray, byte>>(original, imageHsvDest);
        }

        ~FeedHandler()
        {
            dispose(false);
        }

        [HandleProcessCorruptedStateExceptions]
        protected virtual void dispose(bool disposing)
        {
            if (!disposing) return;

            bw.Dispose();
            capture.Dispose();
        }
    }
}