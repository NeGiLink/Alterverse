using System.Collections.Generic;
using UnityEngine;

namespace Kusume
{
    [System.Serializable]
    public class AttackState : PlayerStateBase
    {
        private FangEffectAnimatorController mFangEffecter;

        private IMovement movement;

        private Animator animator;


        public static readonly string StateKey = "Attack";
        public override string Key => StateKey;
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsAttackEndTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            mFangEffecter = actor.GFangEffecter;
            movement = actor.Movement;
            animator = actor.GAnimator;
        }

        public override void DoStart()
        {
            base.DoStart();
            mFangEffecter.EffectStart();
            animator.SetInteger("Attack", 0);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            mFangEffecter.EffectTimeEnd();
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(0);
        }

        public override void DoExit()
        {
            base.DoExit();
            mFangEffecter.EffectEnd();
            animator.SetInteger("Attack", -1);
        }
    }
}
