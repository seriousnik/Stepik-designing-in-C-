using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Differentiation
{
    public static class Algebra
    {
        private static readonly ConstantExpression epsExpr = Expression.Constant(1e-7, typeof(double));
        public static Expression Runner(Expression expression)
        {
            if (expression is ConstantExpression)
            {
                return expression;
            }

            if (expression is ParameterExpression)
            {
                var expr = (ParameterExpression)expression;
                BinaryExpression addEps = Expression.Add(expr, epsExpr);
                return addEps;
            }

            if (expression is BinaryExpression)
            {
                var expr = (BinaryExpression)expression;
                var newExpr = expr.Update(Runner(expr.Left), expr.Conversion, Runner(expr.Right));
                return newExpr;
            }

            if (expression is MethodCallExpression)
            {
                var expr = (MethodCallExpression)expression;
                var args = expr.Arguments;
                var listArgs = new List<Expression>();
                foreach (var arg in args)
                {
                    var newArg = Runner(arg);
                    listArgs.Add(newArg);
                }
                return expr.Update(null, listArgs);
            }
            throw new ArgumentException();
        }

        public static Expression<Func<double, double>> Differentiate(Expression<Func<double, double>> function)
        {
            Expression body = function.Body;
            ParameterExpression param = function.Parameters[0];
            ConstantExpression zeroExpr = Expression.Constant((double)0, typeof(double));
            BinaryExpression addEps = Expression.Add(param, epsExpr);

            if (body is ConstantExpression)
                return Expression.Lambda<Func<double, double>>(zeroExpr, new ParameterExpression[] { param });

            var funcValWithEps = Runner(body);
            BinaryExpression sumExpr = Expression.Subtract(funcValWithEps, body);
            BinaryExpression divideExpr = Expression.Divide(sumExpr, epsExpr);

            return Expression.Lambda<Func<double, double>>(divideExpr, new ParameterExpression[] { param });
        }
    }
}
