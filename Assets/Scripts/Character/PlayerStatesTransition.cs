using UnityEngine;

namespace Kusume
{
    //移動していた時
    public class IsMoveTransition : CharacterStateTransitionBase
    {
        private readonly ActionInput input;
        public IsMoveTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.GActionInput;
        }
        public override bool IsTransition() => input.GMoveValue.magnitude > 0;
    }
    //止まった時
    public class IsNotMoveTransition : CharacterStateTransitionBase
    {
        private readonly ActionInput input;
        public IsNotMoveTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.GActionInput;
        }
        public override bool IsTransition() => input.GMoveValue.magnitude < 0.01f;
    }

    //ジャンプ入力による遷移
    public class IsJumpPushTransition : CharacterStateTransitionBase
    {
        private readonly ActionInput input;
        private readonly LandingChecker landingChecker;
        public IsJumpPushTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.GActionInput;
            landingChecker = actor.gameObject.GetComponent<LandingChecker>();
        }
        public override bool IsTransition() => input.GMoveValue.y > 0 && landingChecker.GLanding;
    }

    //着地した時
    public class IsNotJumpTransition : CharacterStateTransitionBase
    {
        private readonly LandingChecker landingChecker;
        private readonly IVelocityComponent velocity;
        public IsNotJumpTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            landingChecker = actor.gameObject.GetComponent<LandingChecker>();
            velocity = actor.Velocity;
        }
        public override bool IsTransition() => velocity.Rigidbody2D.linearVelocityY < 0 && landingChecker.GLanding;
    }
    //攻撃開始した時
    public class IsAttackTransition : CharacterStateTransitionBase
    {
        private readonly ActionInput actionInput;
        private readonly LandingChecker landingChecker;
        private readonly IVelocityComponent velocity;
        public IsAttackTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            actionInput = actor.GActionInput;
            landingChecker = actor.gameObject.GetComponent<LandingChecker>();
            velocity = actor.Velocity;
        }
        public override bool IsTransition() => actionInput.GAttack;
    }

    //攻撃開始した時
    public class IsAttackEndTransition : CharacterStateTransitionBase
    {
        private readonly ActionInput actionInput;
        private readonly LandingChecker landingChecker;
        private readonly IVelocityComponent velocity;
        private readonly Animator animator;
        public IsAttackEndTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            actionInput = actor.GActionInput;
            landingChecker = actor.gameObject.GetComponent<LandingChecker>();
            velocity = actor.Velocity;
            animator = actor.gameObject.GetComponent<Animator>();
        }
        public override bool IsTransition() => animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
    }
}
