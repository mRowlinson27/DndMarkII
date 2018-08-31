
namespace UIUtilities.API
{
    using System;

    public interface IUiThreadInvoker
    {
        void Init();

        void Dispatch(Action action);
    }
}
