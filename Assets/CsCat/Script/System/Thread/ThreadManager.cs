using System.Collections.Generic;
using System.Threading;

namespace CsCat
{
	public class ThreadManager : ISingleton
	{
		public static ThreadManager instance => SingletonFactory.instance.Get<ThreadManager>();
		public List<Thread> list = new List<Thread>();
		public Dictionary<string, Thread> dict = new Dictionary<string, Thread>();


		public void SingleInit()
		{
		}

		public void Abort()
		{
			for (int i = 0; i < list.Count; i++)
				list[i].Abort();
			list.Clear();
			foreach (var kv in dict)
				kv.Value.Abort();
			dict.Clear();
		}

		public void Start(object args = null)
		{
			for (var i = 0; i < list.Count; i++)
			{
				var t = list[i];
				if (args == null)
					t.Start();
				else
					t.Start(args);
			}

			foreach (var kv in dict)
			{
				var t = kv.Value;
				if (args == null)
					t.Start();
				else
					t.Start(args);
			}


			foreach (var t in dict.Values)
				if (args == null)
					t.Start();
				else
					t.Start(args);
		}

		public void Add(ParameterizedThreadStart threadCallback)
		{
			list.Add(new Thread(threadCallback));
		}

		public void Add(ThreadStart threadCallback)
		{
			list.Add(new Thread(threadCallback));
		}
	}
}