using System;

namespace Kusume
{
    public interface IStateMachine : IDisposable
    {
        IState CurrentState { get; }

        event Action<IState> OnStateChanged;
    }

    //StateChangerのインターフェース
    public interface IStateChanger<TKey>
    {
        bool IsContain(TKey key);
        bool ChangeState(TKey key);
    }
}
