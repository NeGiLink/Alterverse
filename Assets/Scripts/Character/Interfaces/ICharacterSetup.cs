using UnityEngine;

namespace Kusume
{
    /// <summary>
    /// 全キャラクター共通のインタフェース
    /// </summary>
    public interface ICharacterSetup
    {
        IStateMachine StateMachine { get; }
        GameObject gameObject { get; }
        IVelocityComponent Velocity { get; }
        IMovement Movement { get; }
        /*
        IBaseStatus BaseStatus { get; }
        IStepClimberJudgment StepClimberJudgment { get; }
        IRotation Rotation { get; }

        IDamagement Damagement { get; }
        IDamageContainer DamageContainer { get; }

        IFieldOfView FieldOfView { get; }

        IGuardTrigger GuardTrigger { get; }

        CharacterType CharaType { get; }

        SEHandler SEHandler { get; }
        */
    }
    /// <summary>
    /// プレイヤーで使うインタフェース
    /// </summary>
    public interface IPlayerSetup : ICharacterSetup
    {

        public FangEffectAnimatorController GFangEffecter {  get; }
        public Rigidbody2D GRigidbody2D { get; }

        public Animator GAnimator { get; }


        public ActionInput GActionInput { get; }

        public LandingChecker GLandingChecker {  get; }
        /*
        IPlayerStatus Stauts { get; }

        IMoveInputProvider MoveInput { get; }
        IAttackInputProvider AttackInput { get; }
        IToolInputProvider ToolInput { get; }
        IClimb Climb { get; }
        IPlayerAnimator PlayerAnimator { get; }
        IGroundCheck GroundCheck { get; }
        IObstacleJudgment ObstacleJudgment { get; }
        IAllIK IkController { get; }
        IBattleFlagger BattleFlagger { get; }

        IEquipment Equipment { get; }
         */
    }
}
