using System;

namespace CsCat
{
  public class Counter
  {
    public int count = 0;
    public Action change_value_invoke_func;

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
      this.change_value_invoke_func = null;
    }


    public void AddChangeValueInvokeFunc(Action func)
    {
      this.change_value_invoke_func += func;
    }

    public void __CheckFunc()
    {
      this.change_value_invoke_func?.Invoke();
    }
  }

}