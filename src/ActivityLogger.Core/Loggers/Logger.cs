using System;

namespace AL.Core.Loggers
{
    public abstract class Logger<TType> : IObservable<TType>, IDisposable
    {
        protected IObserver<TType> Observer;
        
        public IDisposable Subscribe(IObserver<TType> observer)
        {
            Observer = observer;

            return this;
        }

        public virtual void Dispose()
        {
        }

        public abstract void Log();
    }
}
