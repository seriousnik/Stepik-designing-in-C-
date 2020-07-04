using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.RationalNumbers
{
    public class Rational
    {
        public Rational(int numerator, int denominator = 1)
        {
            var nok = NOK(Math.Abs(numerator), Math.Abs(denominator));
            Numerator = ((numerator > 0 && denominator > 0) || (numerator < 0 && denominator < 0)) ? Math.Abs(numerator) / nok : (-1) * Math.Abs(numerator / nok);
            Denominator = Math.Abs(denominator / nok);
            isNan = (denominator == 0);
        }
        public int Numerator { get; set; }
        public int Denominator { get; set; }
        private bool isNan;
        public bool IsNan { get { return isNan; } private set { isNan = value; } }
        static public Rational operator +(Rational rat1, Rational rat2)
        {
            var newNumerator = rat1.Numerator * rat2.Denominator + rat2.Numerator * rat1.Denominator;
            var newDenominator = rat1.Denominator * rat2.Denominator;
            var nok = NOK(newNumerator, newDenominator);
            return new Rational(newNumerator / nok, newDenominator / nok);
        }

        static public Rational operator -(Rational rat1, Rational rat2)
        {
            var newNumerator = rat1.Numerator * rat2.Denominator - rat2.Numerator * rat1.Denominator;
            var newDenominator = rat1.Denominator * rat2.Denominator;
            var nok = NOK(newNumerator, newDenominator);
            return new Rational(newNumerator / nok, newDenominator / nok);
        }

        static public Rational operator *(Rational rat1, Rational rat2)
        {
            var newNumerator = rat1.Numerator * rat2.Numerator;
            var newDenominator = rat1.Denominator * rat2.Denominator;
            var nok = NOK(newNumerator, newDenominator);
            var ratNew = new Rational(newNumerator / nok, newDenominator / nok);
            if (rat2.Denominator == 0 || rat1.Denominator == 0) ratNew.IsNan = true;
            return ratNew;
        }

        static public Rational operator /(Rational rat1, Rational rat2)
        {
            var newNumerator = rat1.Numerator * rat2.Denominator;
            var newDenominator = rat1.Denominator * rat2.Numerator;
            var nok = NOK(newNumerator, newDenominator);
            var ratNew = new Rational(newNumerator / nok, newDenominator / nok);
            if (rat2.Denominator == 0 || rat1.Denominator == 0 || rat1.Numerator == 0) ratNew.IsNan = true;
            return ratNew;

        }

        static public implicit operator double(Rational rat)
        {
            if (rat.Denominator == 0) return double.NaN;
            return (double)rat.Numerator / rat.Denominator;
        }

        static public explicit operator int(Rational rat)
        {
            if (rat.Numerator % rat.Denominator != 0) throw new InvalidCastException();
            return rat.Numerator / rat.Denominator;
        }

        static public implicit operator Rational(int rat)
        {
            return new Rational(rat);
        }

        public static int NOK(int a, int b)
        {
            if (a == b) return 1;
            var min = (a > b) ? b : a;
            var max = (a > b) ? a : b;
            int nok = 1;
            for (var i = 1; i <= min; i++)
            {
                if (max % i == 0 && min % i == 0) nok = i;
            }
            return nok;
        }

    }

}
