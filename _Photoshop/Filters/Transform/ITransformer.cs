﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MyPhotoshop.Filters
{
    public interface ITransformer<TParameters>
        where TParameters : IParameters, new()
    {
        void Prepare(Size size, TParameters parameters);
        Size ResultSize { get;  }
        Point? MapPoint(Point point);
    }
}
