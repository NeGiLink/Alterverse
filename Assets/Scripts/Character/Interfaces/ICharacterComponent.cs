using UnityEngine;

namespace Kusume
{
    /*
     * キャラクターのコマンド処理につけるインターフェースのコンポーネント
     * と各キャラクターに対応したStateBaseのインターフェース
     */
    public interface ICharacterComponent<T> where T : ICharacterSetup
    {
        void DoSetup(T chara);
    }

    public interface ICharacterState<TKey, TCharacterSetup> : IState<TKey>, ICharacterComponent<TCharacterSetup>
    where TCharacterSetup : ICharacterSetup
    {
    }

    public interface IPlayerState<TKey> : ICharacterState<TKey, IPlayerSetup> { }
    /*
    public interface IEnemyState<TKey> : ICharacterState<TKey, IEnemySetup> { }

    public interface ISlimeState<TKey> : ICharacterState<TKey, ISlimeSetup> { }

    public interface IMushroomState<TKey> : ICharacterState<TKey, IMushroomSetup> { }
    public interface IBullTankState<TKey> : ICharacterState<TKey, IBullTankSetup> { }
    public interface IGolemState<TKey> : ICharacterState<TKey, IGolemSetup> { }
    public interface INPCState<TKey> : ICharacterState<TKey, INPCSetup> { }
     */

    public interface ICharacterStateTransition<TKey>
    {
        bool IsTransition();
        void DoTransition();
    }
}
