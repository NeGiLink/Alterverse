using UnityEngine;
namespace Kusume
{
    public interface IState
    {
        void DoStart();
        void DoUpdate(float time);
        void DoFixedUpdate(float time);
        void DoLateUpdate(float time);
        void DoAnimatorIKUpdate();
        void DoExit();

        void DoTriggerEnter(GameObject thisObject, Collider2D collider);
        void DoTriggerStay(GameObject thisObject, Collider2D collider);
        void DoTriggerExit(GameObject thisObject, Collider2D collider);
    }
    public interface IStateKey<TKey>
    {
        TKey Key { get; }
    }
    public interface IState<TKey> : IState, IStateKey<TKey>
    {
        IStateChanger<TKey> StateChanger { set; }
    }
};

