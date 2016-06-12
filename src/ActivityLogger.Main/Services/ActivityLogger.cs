using System;

namespace ActivityLogger.Core.Services
{
    public abstract class ActivityLogger<TType> : IObservable<TType>, IDisposable
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
    }
}
