using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Kusume
{
    /*
     * プレイヤーのジャンプ状態
     */
    [System.Serializable]
    public class JumpState : PlayerStateBase
    {
        private IMovement movement;

        private IVelocityComponent velocity;

        private Animator animator;

        [SerializeField]
        private float mSpeed;

        [SerializeField]
        private float power;
        [SerializeField]
        private float jumpGravityMultiply;

        [SerializeField]
        private float dashMagnification = 1.5f;

        [SerializeField]
        private float jumpStartCount = 0.25f;

        public static readonly string StateKey = "Jump";
        public override string Key => StateKey;
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotJumpTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
            animator = actor.GAnimator;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.SetInteger("Jump", 0);
            velocity.Rigidbody2D.AddForce(Vector2.up * power, ForceMode2D.Impulse);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            float speed = mSpeed * time;
            movement.Move(speed);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.SetInteger("Jump", -1);
        }
    }
}
