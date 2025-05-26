using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace Kusume
{
    /*
         * ƒvƒŒƒCƒ„[‚ÌˆÚ“®ó‘Ô
         */
    [System.Serializable]
    public class MoveState : PlayerStateBase
    {

        private IMovement movement;

        private Animator animator;

        private ActionInput actionInput;

        [SerializeField]
        private float moveGravityMultiply;
        [SerializeField]
        private float mSpeed;

        public static readonly string StateKey = "Move";

        public override string Key => StateKey;
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotMoveTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
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
            animator.SetInteger("Move", (int)actionInput.GMoveValue.x);
        }

        public override void DoUpdate(float time)
        {
            animator.SetInteger("Move", (int)actionInput.GMoveValue.x);
            base.DoUpdate(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            float speed = mSpeed * time;
            if(speed > 0)
            {
                movement.Move(speed);
            }
        }

        public override void DoLateUpdate(float time)
        {
            base.DoLateUpdate(time);
        }

    }
}
