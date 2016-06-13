using System;

namespace AL.Core.Reporters
{
    public abstract class Reporter<TType> : IObserver<TType>
    {
        protected IDisposable Unsubscriber;

        public virtual void Subscribe(IObservable<TType> provider)
        {
            Unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            Unsubscriber.Dispose();
        }

        public virtual void OnNext(TType value)
        {
            Act(value);
        }

        protected abstract void Act(TType value);

        public void OnError(Exception error)
        {
        }

        public void OnCompleted()
        {
        }
    }
}