using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ThreadClass
{
	public class Taskify<T>
	{
		private Thread _thread;
		private T? result;

		public Taskify(Func<T> func)
		{
			_thread = new Thread(()=>
			{
				result = func();
			});

			_thread.IsBackground = true;
		}

		public void Start()
		{
			_thread.Start();
		}

		public static Taskify<T> Run(Func<T> action)
		{
			Taskify<T> taskify = new Taskify<T>(action);
			taskify.Start();

			return taskify;
		}

		public void Wait()
		{
			_thread.Join();
		}

		public T? Result
		{
			get
			{
				Wait();
				return result;
			}
		}

		public void ContinueWith(Action<Taskify<T>> action)
		{
			Thread thread = new Thread(() =>
			{
				while (_thread.IsAlive) ;
				action(this);
			});

			thread.IsBackground = true;
			thread.Start();
		}
	}
}