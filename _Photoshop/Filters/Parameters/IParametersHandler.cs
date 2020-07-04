using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public interface IParametersHandler<TParameters>
    {
        ParameterInfo[] GetDedcription();
        TParameters CreateParameters(double[] values);
    }
}
