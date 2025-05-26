using System.Collections.Generic;
using UnityEngine;

namespace Kusume
{
    //TODO : �S�L�����N�^�[��State�N���X�̃x�[�X�N���X
    public abstract class CharacterStateBase<TKey, TCharacterSetup> : ICharacterState<TKey, TCharacterSetup>
    where TCharacterSetup : ICharacterSetup
    {
        public abstract TKey Key { get; }
        public override string ToString() => Key.ToString();

        public IStateChanger<TKey> StateChanger { protected get; set; }

        private List<ICharacterStateTransition<TKey>> transitionList = new List<ICharacterStateTransition<TKey>>();

        public abstract List<ICharacterStateTransition<TKey>> CreateTransitionList(TCharacterSetup actor);

        public virtual void DoSetup(TCharacterSetup actor)
        {
            transitionList = CreateTransitionList(actor);
        }

        public virtual void DoStart() { }
        //State�̕ύX���`�F�b�N
        public virtual void TransitionCheck()
        {
            foreach (var check in transitionList)
            {
                if (check.IsTransition())
                {
                    check.DoTransition();
                    break;
                }
            }
        }

        public virtual void DoUpdate(float time)
        {
            TransitionCheck();
        }

        public virtual void DoFixedUpdate(float time) { }

        public virtual void DoLateUpdate(float time) { }

        public virtual void DoAnimatorIKUpdate() { }

        public virtual void DoExit() { }

        public virtual void DoTriggerEnter(GameObject thisObject, Collider2D collider) { }

        public virtual void DoTriggerStay(GameObject thisObject, Collider2D collider) { }

        public virtual void DoTriggerExit(GameObject thisObject, Collider2D collider) { }
    }

    //���L�͊e�L�����N�^�[���Ƃ�StateBase��

    public abstract class PlayerStateBase : CharacterStateBase<string, IPlayerSetup>, IPlayerState<string>
    {
        public override abstract string Key { get; }

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            // PlayerState��p�̃g�����W�V�������X�g�쐬����������
            return new List<ICharacterStateTransition<string>>();
        }
    }

    public abstract class CharacterStateTransitionBase : ICharacterStateTransition<string>
    {
        readonly IStateChanger<string> stateChanger;
        readonly string changeKey;
        protected CharacterStateTransitionBase(IStateChanger<string> stateChanger, string changeKey)
        {
            this.stateChanger = stateChanger;
            this.changeKey = changeKey;
        }
        public abstract bool IsTransition();
        public void DoTransition() => stateChanger.ChangeState(changeKey);
    }
}
