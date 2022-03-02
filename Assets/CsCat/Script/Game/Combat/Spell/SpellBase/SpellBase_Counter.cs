namespace CsCat
{
	public partial class SpellBase
	{
		protected Counter counter;

		private void InitCounter()
		{
			this.counter = new Counter();
			this.counter.AddChangeValueInvokeFunc(this._CounterFunc);
		}

		private void _CounterFunc()
		{
			if (this.counter.count < 0)
				LogCat.error("Counter.count < 0");
			if (this.counter.count == 0 && this.isSpellAnimationFinished && !this.IsDestroyed())
				this.RemoveSelf();
		}


		protected void CounterIncrease()
		{
			this.counter.Increase();
		}

		protected void CounterDecrease()
		{
			this.counter.Decrease();
		}
	}
}