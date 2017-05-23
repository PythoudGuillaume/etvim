using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace EtVim
{
    public class Pedal : IObservable<bool>, IDisposable
    {
        private List<IObserver<bool>> _observers;
        private SerialPort _serialPort;
        private bool _continue = true;
        private Thread _readThread;

        private bool _isActivated { get; set; }

        public Pedal()
        {
            _observers = new List<IObserver<bool>>();


            _serialPort = new SerialPort();
            _serialPort.PortName = "COM3";
            _serialPort.BaudRate = 9600;
            _serialPort.ReadTimeout = 50000;
            _serialPort.WriteTimeout = 50000;
            _serialPort.Open();

            _readThread = new Thread(Read);
            _readThread.Start();
        }

        public IDisposable Subscribe(IObserver<bool> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber<bool>(_observers, observer);
        }

        private void Read()
        {
            while (_continue)
            {
                try
                {
                    bool isActivated;
                    var line = _serialPort.ReadLine();
                    
                    switch (line.Trim())
                    {
                        case "on":
                            isActivated = true;
                            break;
                        case "off":
                            isActivated = false;
                            break;
                        default:
                            continue;
                    }

                    if (_isActivated != isActivated)
                    {
                        _isActivated = isActivated;
                        foreach (var observer in _observers)
                        {
                            observer.OnNext(_isActivated);
                        }
                    }
                }
                catch (TimeoutException e)
                {
                    foreach (var observer in _observers)
                    {
                        observer.OnError(e);
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _continue = false;
                _readThread.Join();
                _serialPort.Close();
                _serialPort.Dispose();
            }
        }
    }
}
