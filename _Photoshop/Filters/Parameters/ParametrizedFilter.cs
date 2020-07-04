using MyPhotoshop.Filters.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoshop
{
    abstract public class ParametrizedFilter<TParameters> : IFilter 
        where TParameters : IParameters, new()
    {

        IParametersHandler<TParameters> handler = new ExpressionsParametersHandler<TParameters>();
        public ParameterInfo[] GetParameters()
        {
            //eturn new TParameters().GetDesсription();//GetDescription
            return handler.GetDedcription();
        }

        public Photo Process(Photo original, double[] values)
        {
            var parameters = handler.CreateParameters(values);
            return Process(original, parameters);
        }
        public abstract Photo Process(Photo photo, TParameters parameters);
    }
}

//- сделайте ParametrizedFilter дженерик-классом, принимающий дженерик-параметр, реализующий IParameters
//- протащите дженерик-параметр вниз по иерархии в PixelFilter
//- уберите конструкторы в иерархии наследования за счет требования new()
//- создавайте новый объект параметров каждый раз при использовании
//- уберите даункаст в LighteningFilter