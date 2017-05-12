using System;
using System.Collections.Generic;

namespace EtVim
{
    internal class Unsubscriber<T> : IDisposable
    {
        private IList<IObserver<T>> _observers;
        private IObserver<T> _observer;

        internal Unsubscriber(IList<IObserver<T>> observers, IObserver<T> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }
}
