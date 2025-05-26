using Kusume;
using UnityEngine;

public class PlayerStateController : MonoBehaviour,IPlayerSetup
{
    private FangEffectAnimatorController mFangEffecter;

    public FangEffectAnimatorController GFangEffecter => mFangEffecter;


    [SerializeField]
    private StateMachine<string> stateMachine;
    public IStateMachine StateMachine => stateMachine;

    [SerializeField]
    protected VelocityComponent velocity;

    public IVelocityComponent Velocity => velocity;

    [SerializeField]
    private Movement movement;
    public IMovement Movement => movement;

    private Rigidbody2D mRigidbody2D;

    public Rigidbody2D GRigidbody2D => mRigidbody2D;

    private Animator mAnimator;

    public Animator GAnimator => mAnimator;

    private ActionInput mActionInput;

    public ActionInput GActionInput => mActionInput;

    private LandingChecker mLandingChecker;
    public LandingChecker GLandingChecker => mLandingChecker;

    [Header("下記はキャラクターの状態クラス")]
    [SerializeField]
    private PlayerIdleState idleState;

    [SerializeField]
    private MoveState moveState;

    [SerializeField]
    private JumpState jumpState;

    [SerializeField]
    private AttackState attackState;

    IPlayerState<string>[] states;

    [SerializeField]
    private string defaultStateKey;

    private void Awake()
    {
        mFangEffecter = GetComponentInChildren<FangEffectAnimatorController>();

        mActionInput = GetComponentInChildren<ActionInput>();
        mRigidbody2D = GetComponentInParent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mLandingChecker = GetComponentInChildren<LandingChecker>();
        velocity.DoSetup(this);
        movement.DoSetup(this);

        states = new IPlayerState<string>[]
        {
                idleState,
                moveState,
                jumpState,
                attackState
        };
        stateMachine.DoSetup(states);
        foreach (var state in states)
        {
            state.DoSetup(this);
        }
        stateMachine.ChangeState(defaultStateKey);
    }

    private void Start()
    {
        mFangEffecter.EffectEnd();
    }

    private void Update()
    {
        float t = Time.deltaTime;
        velocity.CurrentVelocity = mActionInput.GMoveValue;
        transform.localScale = new Vector3(mActionInput.GFlip, transform.localScale.y, transform.localScale.z);
        stateMachine.DoUpdate(t);
    }

    private void FixedUpdate()
    {
        stateMachine.DoFixedUpdate(Time.deltaTime);
    }

    protected virtual void LateUpdate()
    {
        stateMachine.DoLateUpdate(Time.deltaTime);
    }

    private void OnAnimatorIK()
    {
        stateMachine.DoAnimatorIKUpdate();
    }

    private void OnDestroy()
    {
        stateMachine.Dispose();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        stateMachine.DoTriggerEnter(gameObject, collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        stateMachine.DoTriggerStay(gameObject, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        stateMachine?.DoTriggerExit(gameObject, collision);
    }
}
