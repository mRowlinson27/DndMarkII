﻿
namespace UIUtilities
{
    using System;
    using System.Threading;
    using API;
    using Utilities.API;

    public class UiThreadInvoker : IUiThreadInvoker
    {
        private readonly ILogger _logger;

        private SynchronizationContext _syncContext;

        public UiThreadInvoker(ILogger logger)
        {
            _logger = logger;
        }

        public void Init()
        {
            _syncContext = SynchronizationContext.Current;
        }

        public void Dispatch(Action action)
        {
            if (_syncContext == null || SynchronizationContext.Current == _syncContext)
            {
                action();
            }
            else
            {
                _syncContext.Post(o => action(), null);
            }
        }
    }
}
