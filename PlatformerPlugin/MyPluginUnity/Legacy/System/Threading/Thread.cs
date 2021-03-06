﻿#if NETFX_CORE
using System;
using System.Threading;

using System.Threading.Tasks;
#endif

#if NETFX_CORE
namespace System
{

    public delegate void ParameterizedThreadStart(object target);
    public delegate void ThreadStart();

    public class Thread
    {
        /*
         * pretty sure Task.Start doesn't always spin up a new thread (depends on synccontext)
         * pretty sure that we'll need try/catching as tasks can throw exceptions when their state isn't as expected (e.g. waiting on a completed task?)
         * */

        private ParameterizedThreadStart _paramThreadStart;
        private ThreadStart _threadStart;

        private Task _task = null;
        private CancellationTokenSource _taskCancellationTokenSource;

        /// <summary>
        /// Currently this value is ignored, not sure how to implement this
        /// </summary>
        public bool IsBackground
        {
            get { return true; }
            set { throw new NotImplementedException("currently always on background"); }
        }

        public Thread(ThreadStart start)
        {
            _taskCancellationTokenSource = new CancellationTokenSource();
            _threadStart = start;
        }

        public Thread(ParameterizedThreadStart start)
        {
            _taskCancellationTokenSource = new CancellationTokenSource();
            _paramThreadStart = start;
        }

        public void Abort()
        {
            if (_taskCancellationTokenSource != null)
            { 
                _taskCancellationTokenSource.Cancel();
            }
        }

        public bool Join(int ms)
        {
            EnsureTask();
            return _task.Wait(ms, _taskCancellationTokenSource.Token);
        }

        public void Start()
        {
            EnsureTask();
            _task.Start(TaskScheduler.Default);
        }

        public void Start(Object param)
        {
            EnsureTask(param);
            _task.Start(TaskScheduler.Default);
        }

        /// <summary>
        /// Ensures the underlying Task is created and initialized correctly
        /// </summary>
        /// <param name="paramThreadStartParam"></param>
        private void EnsureTask(object paramThreadStartParam = null)
        {
            if (_task == null)
            { 
                if (_paramThreadStart != null)
                {
                    _task = new Task(() => _paramThreadStart(paramThreadStartParam), _taskCancellationTokenSource.Token);
                }
                else if (_threadStart != null)
                {
                    _task = new Task(() => _threadStart(), _taskCancellationTokenSource.Token);
                }
            }
        }

        public static void Sleep(int ms)
        {
            new ManualResetEvent(false).WaitOne(ms);
        }
    }

    public class ThreadAbortException : Exception
    {

    }

}
#endif