using System;

namespace ActivityLogger.Core.Services
{
    public abstract class ActivityReporter<TType> : IActivityReporter, IObserver<TType>
    {
        public DateTime LastActivity { get; private set; } = DateTime.MinValue;
        public bool UserIsActive => DateTime.Now - LastActivity < TimeSpan.FromSeconds(Settings.SecondsBeforeConsideredIdle);

        protected IDisposable Unsubscriber;
        protected Settings Settings;
        
        protected ActivityReporter(Settings settings)
        {
            Settings = settings;
        }

        public virtual void Subscribe(IObservable<TType> provider)
        {
            Unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            Unsubscriber.Dispose();
        }

        public void OnNext(TType value)
        {
            LastActivity = DateTime.Now;

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