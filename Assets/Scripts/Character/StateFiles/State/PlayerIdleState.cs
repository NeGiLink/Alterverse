using System.Collections.Generic;
using UnityEngine;

namespace Kusume
{
    [System.Serializable]
    public class PlayerIdleState : PlayerStateBase
    {
        private IMovement movement;

        private Animator animator;

        private ActionInput actionInput;

        [SerializeField]
        private float idleGravityMultiply;

        public static readonly string StateKey = "Idle";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsMoveTransition(actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(JumpState.StateKey)) { re.Add(new IsJumpPushTransition(actor, StateChanger, JumpState.StateKey)); }
            if (StateChanger.IsContain(AttackState.StateKey)) { re.Add(new IsAttackTransition(actor, StateChanger, AttackState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            animator = actor.GAnimator;
            actionInput = actor.GActionInput;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.SetInteger("Move", 0);
            /*
            animator.Animator.SetFloat(animator.VelocityZAnimationID, 0f);
            animator.Animator.SetFloat(animator.BattleModeAnimationID, 0.0f);
             */
        }

        public override void DoUpdate(float time)
        {
            AnimationUpdate();
            base.DoUpdate(time);
        }

        private void AnimationUpdate()
        {
            animator.SetInteger("Move", (int)actionInput.GMoveValue.x);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(0);
        }
    }
}
