using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.API
{
    public class APIObject : IDisposable
    {
		private int n;
        public APIObject(int n)
        {
            MagicAPI.Allocate(n);
			this.n = n;
        }

		private bool isDisposed = false;
		~APIObject()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this); //финализатор не будет вызываться
		}

		protected virtual void Dispose(bool fromDisposeMethod)
		{
			if (!isDisposed)
			{
				if (fromDisposeMethod)
				{
					//Console.WriteLine("Очистка управляемых ресурсов в {0}", name);
				}

				MagicAPI.Free(n);
				//Console.WriteLine("Очистка неуправляемых ресурсов в {0}", name);
				isDisposed = true;
				// base.Dispose(isDisposing); // если унаследован от Disposable класса
			}
		}
	}
}


//Используем паттерн Disposable в ситуации, 
//схожей с работой со внешним API.В проекте Memory.API в роли внешнего API выступает класс MagicAPI, 
//методы которого позволяют выделить ресурс, освободить его, и проверить, какие ресурсы выделены в настоящий момент.

//Реализуйте класс APIObject, который будет оберткой над API.
//Выделение ресурса через внешнее API должна происходить в конструкторе, а освобождение - в соответствие с паттерном Disposable.