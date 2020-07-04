using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Observers
{
	//public interface IObserver
	//{
	//	void HandleEvent(object eventData);
	//}

	//public interface IObservable
	//{
	//	void Add(IObserver observer);
	//	void Remove(IObserver observer);
	//	void Notify(object eventData);
	//}
	public class StackOperationsLogger
	{
		//private readonly Observer observer = new Observer();
		public void SubscribeOn<T>(ObservableStack<T> stack)
		{
			//stack.Add(observer);
			//stack.OnAction += AddToLogList;
			stack.MyEvent += AddToLogList1;
		}

		private void AddToLogList<T>(StackEventData<T> stack) { log += stack.ToString(); }

		private void AddToLogList1<T>(object sender, StackEventData<T> e) { log += e.ToString(); }

		private string log = "";

		//public List<StackEventData<T>> logList;
		public string GetLog()
		{
			return log;
		}
	}

	//public class Observer : IObserver
	//{
	//	public StringBuilder Log = new StringBuilder();

	//	public void HandleEvent(object eventData)
	//	{
	//		Log.Append(eventData);
	//	}
	//}
	//var stack = new ObservableStack<int>();
	//var helper = new StackOperationsLogger();
	//helper.SubscribeOn(stack);
	//Assert.AreEqual("", helper.GetLog());
	public class ObservableStack<T> //: IObservable
	{
		//List<IObserver> observers = new List<IObserver>();
		//private delegate void SomeDelegate(StackEventData<T> stackEventData);
		//private event SomeDelegate OnAction;

		public event EventHandler<StackEventData<T>> MyEvent;
		//public void Add(T observer)
		//{
		//	observers.Add(observer);
		//}
		public void Notify(object eventData)
        {
			//OnAction();
			//foreach (var observer in observers)
			//    observer.HandleEvent(eventData);
			//OnAction += eventData;


		}
        //public void Remove(T observer)
        //{
        //	observers.Remove(observer);
        //}
        List<T> data = new List<T>();
		public void Push(T obj)
		{
			data.Add(obj);
            MyEvent?.Invoke(this, new StackEventData<T> { IsPushed = true, Value = obj });
            //OnAction(new StackEventData<T> { IsPushed = true, Value = obj });
            //Notify(new StackEventData<T> { IsPushed = true, Value = obj });
        }

		public T Pop()
		{
			if (data.Count == 0)
				throw new InvalidOperationException();
			var result = data[data.Count - 1];
			//Notify(new StackEventData<T> { IsPushed = false, Value = result });
			//OnAction(new StackEventData<T> { IsPushed = false, Value = result });
			MyEvent?.Invoke(this, new StackEventData<T> { IsPushed = false, Value = result });
			return result;

		}
	}
}





//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Delegates.Observers
//{
//	public interface IObserver
//	{
//		void HandleEvent(object eventData);
//	}

//	public interface IObservable
//	{
//		void Add(IObserver observer);
//		void Remove(IObserver observer);
//		void Notify(object eventData);
//	}
//	public class StackOperationsLogger
//	{
//		private readonly Observer observer = new Observer();
//		public void SubscribeOn<T>(ObservableStack<T> stack)
//		{
//			stack.Add(observer);
//		}

//		public string GetLog()
//		{
//			return observer.Log.ToString();
//		}
//	}

//	public class Observer : IObserver
//	{
//		public StringBuilder Log = new StringBuilder();

//		public void HandleEvent(object eventData)
//		{
//			Log.Append(eventData);
//		}
//	}
//	//var stack = new ObservableStack<int>();
//	//var helper = new StackOperationsLogger();
//	//helper.SubscribeOn(stack);
//	//		Assert.AreEqual("", helper.GetLog());
//	public class ObservableStack<T> : IObservable
//	{
//		List<IObserver> observers = new List<IObserver>();

//		public void Add(IObserver observer)
//		{
//			observers.Add(observer);
//		}

//		public void Notify(object eventData)
//		{
//			foreach (var observer in observers)
//				observer.HandleEvent(eventData);
//		}

//		public void Remove(IObserver observer)
//		{
//			observers.Remove(observer);
//		}

//		List<T> data = new List<T>();

//		public void Push(T obj)
//		{
//			data.Add(obj);
//			Notify(new StackEventData<T> { IsPushed = true, Value = obj });
//		}

//		public T Pop()
//		{
//			if (data.Count == 0)
//				throw new InvalidOperationException();
//			var result = data[data.Count - 1];
//			Notify(new StackEventData<T> { IsPushed = false, Value = result });
//			return result;

//		}
//	}
//}
