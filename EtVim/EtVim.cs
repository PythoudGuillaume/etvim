using EyeXFramework;
using System;
using System.Threading;
using Tobii.EyeX.Framework;

namespace EtVim
{
    public class EtVim : IObserver<bool>
    {
        private int i = 0;
        private EventWaitHandle _waitHandle;
        private Vim _vim;
        private bool _isPedalActivated = false;
        private Thread _eyeTrackerThread;

        public EtVim(EventWaitHandle e)
        {
            _vim = new Vim();
            _vim.Foreground();

            using (var pedal = new Pedal())
            {
                pedal.Subscribe(this);
                var signaled = false;
                do
                {
                    signaled = e.WaitOne(100);
                } while (!signaled);
            }
        }

        private void RunEyeTracker()
        {
            using (var eyeXHost = new EyeXHost())
            {
                // Create a data stream: lightly filtered gaze point data.
                using (var lightlyFilteredGazeDataStream = eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered))
                {
                    // Start the EyeX host.
                    eyeXHost.Start();
                    lightlyFilteredGazeDataStream.Next += OnGazeData;

                    var signaled = false;
                    do
                    {
                        Console.WriteLine("Eyex");
                        signaled = _waitHandle.WaitOne(100);
                    } while (!signaled);
                }
            }

        }

        private void OnGazeData(object o, GazePointEventArgs args)
        {
            i++;
            if (i % 100 != 0)
            {
                return;
            }

            _vim.Foreground();
            _vim.LeftMouseClick((int)args.X, (int)args.Y);
        }

        public void OnNext(bool value)
        {
            _isPedalActivated = value;
            if (value)
            {
                _waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, Guid.NewGuid().ToString());
                _eyeTrackerThread = new Thread(RunEyeTracker);
                _eyeTrackerThread.Start();
            }
            else
            {
                _waitHandle.Set();
                _eyeTrackerThread.Join();
            }
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnCompleted()
        {
            Console.WriteLine("Done");
        }
    }
}
