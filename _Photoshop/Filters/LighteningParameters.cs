using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop.Filters
{
    public class LighteningParameters : IParameters
    {
        [ParameterInfo(Name = "Коэффициент", MaxValue = 10, MinValue = 0, Increment = 0.1, DefaultValue = 1)]
        public double Coefficient { get; set; }

    }
}



//- создайте интерфейс IParameters - по смыслу, это интерфейс класса, который содержит настройки какого-то фильтра.
//  В этом интерфейсе определите методы:
//    ParameterInfo[] GetDesсription(), который будет возвращать информацию о настройках
//    void Parse(double[]), который будет устанавливать поля класса в соответствие с массивом переданных величин
//- создайте класс LighteningParameters с полем Coefficient, реализующий IParameters.
//  Реализуйте метод Parse так, чтобы он устанавливал это поле в нулевой значение массива.
//  Аналогично с GrayscaleParameters.
//- создайте класс ParametrizedFilter, который бы имел поле IParameters parameters, устанавливаемый
//  в конструкторе.
//- Метод ParametrizedFilter.GetParameters() перенаправьте на parameters.GetDescription()
//- сделайте абстрактный метод ParametrizedFilter.Process(Photo photo, IParameters parameters)
//- В методе ParametrizedFilter.Process(Photo, double[]) вызовите parameters.Parse и затем
//  ParametrizedFilter.Process(Photo, parameters)
//- выведите PixelFilter из ParametrizedFilter и заставьте все заработать
