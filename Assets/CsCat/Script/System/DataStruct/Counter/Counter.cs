using System;

namespace CsCat
{
	public class Counter
	{
		public int count = 0;
		public Action changeValueInvokeFunc;

		public void Increase()
		{
			this.count = this.count + 1;
			this.__CheckFunc();
		}

		public void Decrease()
		{
			this.count = this.count - 1;
			this.__CheckFunc();
		}

		public void Reset()
		{
			this.count = 0;
			this.changeValueInvokeFunc = null;
		}


		public void AddChangeValueInvokeFunc(Action func)
		{
			this.changeValueInvokeFunc += func;
		}

		public void __CheckFunc()
		{
			this.changeValueInvokeFunc?.Invoke();
		}
	}

}