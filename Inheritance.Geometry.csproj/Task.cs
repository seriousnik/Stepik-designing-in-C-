using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.Geometry
{
	public abstract class Body
	{
		public abstract double GetVolume();
		public abstract void Accept(IVisitor visitor);
	}

	public class Ball : Body
	{
		public double Radius { get; set; }
        public override double GetVolume()
        {
			return 4.0 * Math.PI * Math.Pow(Radius, 3) / 3;
		}
		public override void Accept(IVisitor visitor)
		{
			visitor.SetDimensions(new Dimensions(2 * Radius, 2 * Radius));
			visitor.SetSurfaceArea(4*Math.PI*Radius*Radius);
		}

    }

	public class Cube : Body
	{
		public double Size { get; set; }

        public override double GetVolume()
        {
			return Math.Pow(Size, 3);
		}
		public override void Accept(IVisitor visitor)
		{
			visitor.SetDimensions(new Dimensions(Size, Size));
			visitor.SetSurfaceArea(Size*Size*6);
		}
	}

	public class Cylinder : Body
	{
		public double Height { get; set; }
		public double Radius { get; set; }

        public override double GetVolume()
        {
			return Math.PI* Math.Pow(Radius, 2) * Height;
		}
		public override void Accept(IVisitor visitor)
		{
			visitor.SetDimensions(new Dimensions(2 * Radius, Height));
			visitor.SetSurfaceArea(2 * Math.PI * Radius * (Height + Radius));
		}
	}

	public class SurfaceAreaVisitor : IVisitor
	{
		public double SurfaceArea { get; private set; }

		public void SetSurfaceArea(double surfaceArea)
		{
			SurfaceArea = surfaceArea;
		}
        public Dimensions GetDimensions()
        {
            throw new NotImplementedException();
        }

        public double GetSurfaceArea()
        {
            return SurfaceArea;
        }

        public void SetDimensions(Dimensions dimensions)
        {
            
        }
    }
	public class DimensionsVisitor : IVisitor
	{
		public Dimensions Dimensions { get; private set; }

        public Dimensions GetDimensions()
        {
			return Dimensions;
        }

		public void SetDimensions(Dimensions dimensions)
		{
			Dimensions = dimensions;
		}

        public double GetSurfaceArea()
        {
            throw new NotImplementedException();
        }

        public void SetSurfaceArea(double area)
        {
            
        }
    }

	public interface IVisitor
	{
		double GetSurfaceArea();
		Dimensions GetDimensions();

		void SetDimensions(Dimensions dimensions);
		void SetSurfaceArea(double surfaceArea);
	}
}

//Давайте теперь предположим, что в предыдущей задаче новых геометрических примитивов добавлять мы не собираемся.
//	Зато собираемся добавлять новые методы для работы с уже имеющимися — они могут вычислять объем, 
//	площадь поверхности, рассчитывать точку пересечения объекта с прямой и т.д.
//В этом случае часто используется шаблон Visitor.
//Определите интерфейс IVisitor и реализуйте его в двух классах DimensionsVisitor и SurfaceAreaVisitor, 
//для рассчёта размеров (ширина, высота) и площади поверхности фигур.
//В класс Body добавьте абстрактный метод Accept(IVisitor visitor).
//Автоматизированные тесты проверяют лишь базовые требования. Проверить, что вы всё сделали правильно можно самостоятельно так:

//В реализациях Visitor не должно быть ни одного приведения типов и ни одного if-а.
//Именно этой простотой решение с Visitor-ом лучше исходного с длинным if-else.
//Работа с каждой фигурой должна оказаться в отдельном методе.А значит даже если добавится новая фигура, 
//будет меньше возможностей случайно внести ошибку в обработку старых фигур.
//Компилятор должен контролировать, что вы не забыли обработать ни одну из фигур: 
//если вы забудете написать один из методов, программа даже не скомпилируется.
//В интерфейсе IVisitor, в классе Body и всех его подклассах не должно быть никакого упоминания площади поверхности,
//размеров или конкретных классов Visitor-ов.А значит при добавлении новых методов, эти классы не нужно будет трогать.
//Для добавления нового метода работы с фигурами, должно быть достаточно добавить новый класс Visitor-а.