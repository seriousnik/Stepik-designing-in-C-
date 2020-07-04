using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Reflection.Randomness
{
    public class GeneratorWithSelectedMember<T> where T : new()
    {
        public T t;
        public string propertyName;
        public Dictionary<string, IContinousDistribution> fieldsWithNewValue;
        public GeneratorWithSelectedMember(T someElement, string propertyName, Dictionary<string, IContinousDistribution> fieldsWithNewValue)
        {
            t = someElement;
            this.propertyName = propertyName;
            this.fieldsWithNewValue = fieldsWithNewValue;
        }
        public Generator<T> Set(IContinousDistribution distribution)
        {
            fieldsWithNewValue.Add(propertyName, distribution);
            return new Generator<T>() { t = t, fieldsWithNewValue = fieldsWithNewValue };
        }
    }



    public class Generator<T> where T : new()
    {
        public T t = new T();
        public Dictionary<string, IContinousDistribution> fieldsWithNewValue = new Dictionary<string, IContinousDistribution>();
        private bool isGenerated = false;
        public GeneratorWithSelectedMember<T> For(Expression<Func<T, object>> function)
        {
            try
            {
                var expression = function.Body;
                var unaryExpression = (UnaryExpression)expression;
                var memberExpression = (MemberExpression)unaryExpression.Operand;
                var name = memberExpression.Member.Name;
                if (typeof(T).GetProperties().Where(e => e.Name == name).Count() == 0)
                    throw new ArgumentException();
                return new GeneratorWithSelectedMember<T>(t, name, fieldsWithNewValue);
            }
            catch
            {
                throw new ArgumentException();
            }

        }
        public T Generate(Random rnd)
        {
            if (isGenerated)
                t = new T();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (!fieldsWithNewValue.ContainsKey(property.Name))
                {
                    var customAttributes = property.GetCustomAttributes(false);
                    foreach (var attr in customAttributes)
                    {

                        FromDistribution distr = attr as FromDistribution;
                        if (distr != null)
                        {
                            property.SetValue(t, distr.distr.Generate(rnd));
                        }

                    }
                }
                else
                {
                    property.SetValue(t, fieldsWithNewValue[property.Name].Generate(rnd));
                }
            }
            isGenerated = true;
            return t;
        }
    }

    public class FromDistribution : Attribute
    {
        public IContinousDistribution distr;
        public FromDistribution(Type Distribution, params int[] parameters)
        {

            if (Distribution.Name == "NormalDistribution")
            {
                if (parameters.Length == 2)
                {
                    distr = (IContinousDistribution)Distribution.GetConstructor(new Type[] { typeof(double), typeof(double) }).Invoke(new object[] { parameters[0], parameters[1] });
                }
                else if (parameters.Length == 0)
                {
                    distr = (IContinousDistribution)Distribution.GetConstructor(Type.EmptyTypes).Invoke(new object[] { });
                }
                else
                    throw new ArgumentException("NormalDistribution must have 2 or 0 parameters");
            }

            if (Distribution.Name == "ExponentialDistribution")
            {
                if (parameters.Length == 1)
                    distr = (IContinousDistribution)Distribution.GetConstructor(new Type[] { typeof(double) }).Invoke(new object[] { parameters[0] });
                else
                    throw new ArgumentException("ExponentialDistribution must have 1 parameter");
            }
        }

    }
}


//Для нагрузочного тестирования вашей программы вам нужно уметь создавать большое количество экземпляров классов, 
//при этом они должны быть существенно различны.Вы решили использовать для этой цели генератор случайных чисел, 
//и решили использовать атрибуты для того, чтобы указать, из какого распределения брать значения для тех или иных свойств в объектах.
//Понятно, что решение с прикреплением распределения к свойствам "намертво" недостаточно гибкое, поэтому вы заранее озаботились тем, 
//    чтобы можно было это распределение менять настройками генератора объектов.
//Для простоты, будем рассматривать только значения типа double и два распределения: нормальное и экспоненциальное.
//Вы можете сделать оптимизированное решение (с Expression.Bind), если хотите, но и менее оптимальное решение тоже подойдет.

