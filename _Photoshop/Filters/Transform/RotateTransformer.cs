﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MyPhotoshop.Filters
{
    public class RotateTransformer : ITransformer<RotationParameters>
    {
        public Size ResultSize { get; set; }

        public Point? MapPoint(Point point)
        {
            var newSize = ResultSize;
            var angle = Angle;
            var oldSize = OriginalSize;
            point = new Point(point.X - newSize.Width / 2, point.Y - newSize.Height / 2);
            var x = oldSize.Width / 2 + (int)(point.X * Math.Cos(angle) + point.Y * Math.Sin(angle));
            var y = oldSize.Height / 2 + (int)(-point.X * Math.Sin(angle) + point.Y * Math.Cos(angle));
            if (x < 0 || x >= oldSize.Width || y < 0 || y >= oldSize.Height) return null;
            return new Point(x, y);
        }

        public Size OriginalSize { get; private set; }
        public void Prepare(Size size, RotationParameters parameters)
        {
            OriginalSize = size;
            Angle = Math.PI * parameters.Angle / 180;
            ResultSize = new Size(
                (int)(size.Width * Math.Abs(Math.Cos(Angle)) + size.Height * Math.Abs(Math.Sin(Angle))),
                (int)(size.Height * Math.Abs(Math.Cos(Angle)) + size.Width * Math.Abs(Math.Sin(Angle))));
        }
        public double Angle { get; set; }
    }
}
