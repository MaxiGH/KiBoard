﻿using KiBoard.ui;
using System.Drawing;
using System.Numerics;

namespace KiBoard.calibration
{
    class VisibleKeyCalibrator : KeyCalibrator
    {
        private KiForm form;
        private Bitmap bitmap;
        private Graphics g;
        private Color color;
        private Brush brush;
        private Pen pen;
        
        public VisibleKeyCalibrator(Tracker t, KiForm f) : base(t)
        {
            form = f;
            bitmap = new Bitmap(form.Size.Width, form.Size.Height);
            g = Graphics.FromImage(bitmap);
            color = Color.FromArgb(255, 255, 255);
            brush = new SolidBrush(color);
            pen = new Pen(brush);
        }

        protected override void initiateWallPoints()
        {
            wallPoints.Add(new Vector3(0, 0, 0));
            wallPoints.Add(new Vector3(0, 0.5f, 0));
            wallPoints.Add(new Vector3(1, 0, 0));
        }

        private void drawCalibrationPoint()
        {
            g.Clear(Color.Black);

            if (points.Count == 0)
            {
                // First point - vertical Line
                Point vFrom = new Point(3, form.Size.Height);
                Point vTo = new Point(3, form.Size.Height - 6);
                g.DrawLine(pen, vFrom, vTo);
                // First point - horizontal Line
                Point hFrom = new Point(0, form.Size.Height - 3);
                Point hTo = new Point(6, form.Size.Height - 3);
                g.DrawLine(pen, hFrom, hTo);
            }
            else if (points.Count == 1)
            {
                // Second point - vertical line
                Point vFrom = new Point(3, (form.Size.Height / 2) + 3);
                Point vTo = new Point(3, (form.Size.Height / 2) - 3);
                g.DrawLine(pen, vFrom, vTo);
                // Second point - horizontal Line
                Point hFrom = new Point(0, form.Size.Height / 2);
                Point hTo = new Point(5, form.Size.Height / 2);
                g.DrawLine(pen, hFrom, hTo);
            } 
            else if (points.Count == 2)
            {
                // Third point - vertical line
                Point vFrom = new Point(form.Size.Width - 2, form.Size.Height);
                Point vTo = new Point(form.Size.Width - 2, form.Size.Height - 5);
                g.DrawLine(pen, vFrom, vTo);
                // Third point - horizontal Line
                Point hFrom = new Point(form.Size.Width - 5, form.Size.Height - 2);
                Point hTo = new Point(form.Size.Width, form.Size.Height - 2);
                g.DrawLine(pen, hFrom, hTo);
            }
        }

        public override void tick()
        {
            base.tick();

            if(!hasCalibrationPoints())
            {
                drawCalibrationPoint();
            }
        }
    }
}
