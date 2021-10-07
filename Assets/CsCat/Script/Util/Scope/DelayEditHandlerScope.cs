using System;
using System.Diagnostics;

namespace CsCat
{
    public class DelayEditHandlerScope : IDisposable
    {
        private DelayEditHandler _delayEditHandler;

        public DelayEditHandlerScope(object editTarget)
        {
            _delayEditHandler = new DelayEditHandler(editTarget);
        }

        public object this[object key]
        {
            set => _delayEditHandler[key] = value;
        }

        public void ToAdd(params object[] args)
        {
            _delayEditHandler.ToAdd(args);
        }

        public void ToRemove(params object[] args)
        {
            _delayEditHandler.ToRemove(args);
        }

        public void ToRemoveAt(int toRemoveIndex)
        {
            _delayEditHandler.ToRemoveAt(toRemoveIndex);
        }

        public void ToRemoveAt_Stack(int toRemoveIndex)
        {
            _delayEditHandler.ToRemoveAt_Stack(toRemoveIndex);
        }

        //后入先出
        public void ToCallback_Stack(Action toCallback)
        {
            _delayEditHandler.ToCallback_Stack(toCallback);
        }

        public void ToSet(object key, object value)
        {
            _delayEditHandler.ToSet(key, value);
        }

        public void ToCallback(Action toCallback)
        {
            _delayEditHandler.ToCallback(toCallback);
        }


        public void Dispose()
        {
            _delayEditHandler.Handle();
        }
    }
}