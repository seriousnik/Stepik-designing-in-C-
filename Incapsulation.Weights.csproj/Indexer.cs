using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Weights
{
	class Indexer
	{
		public Indexer(double[] arr, int start, int length)
		{
			Start = start;
			LengthParam = length;
			if ((length + start) > arr.Length) throw new ArgumentException();
			this.arr = arr;
		}
		public int Length { get { return Arr.Length; } }
		private int start;
		public int Start { get { return start; } set { if (value < 0) throw new ArgumentException(); start = value; } }
		private int lengthParam;
		public int LengthParam { get { return lengthParam; } set { if (value < 0) throw new ArgumentException(); lengthParam = value; } }

		private double[] arr;
		public double[] Arr { get { return arr.Skip(Start).Take(LengthParam).ToArray(); } set { arr = value; } }
		public double this[int key]
		{
			get { if (key > Length - 1 || key < 0) throw new IndexOutOfRangeException(); return arr[Start + key]; }
			set { if (key > Length - 1 || key < 0) throw new IndexOutOfRangeException(); arr[Start + key] = value; }
		}

	}
}
