using UnityEngine;

namespace Kusume
{
    public class PlayerMoveController : MonoBehaviour
    {

        [SerializeField]
        private float mMoveSpeed;

        [SerializeField]
        private float mJumpPower;

        [SerializeField]
        private bool mAttacking;

        private Rigidbody2D mRigidbody2D;

        private Animator mAnimator;

        private ActionInput mActionInput;

        private LandingChecker mLandingChecker;


        private void Awake()
        {
            mActionInput = GetComponent<ActionInput>();
            mRigidbody2D = GetComponentInParent<Rigidbody2D>();
            mAnimator = GetComponentInParent<Animator>();
            mLandingChecker = GetComponentInParent<LandingChecker>();
        }

        private void Start()
        {
            mAttacking = false;

            
        }

        private void FixedUpdate()
        {
            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                return;
            }
            Vector2 velocity = mRigidbody2D.linearVelocity;
            velocity.x = mActionInput.GMoveValue.x * mMoveSpeed * Time.deltaTime;


            mRigidbody2D.linearVelocity = velocity;

            if (mActionInput.GJump&& mLandingChecker.GLanding)
            {
                mRigidbody2D.AddForce(Vector2.up * mJumpPower,ForceMode2D.Impulse);
                mActionInput.SetJumpFrag(false);
            }
        }

        private void Update()
        {
            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") )
            {
                return;
            }
            if (mActionInput.GAttack)
            {
                mAnimator.SetTrigger("Attack");
            }

            mAnimator.SetInteger("Move", (int)mActionInput.GMoveValue.x);
            mAnimator.SetBool("Jump 0", mLandingChecker.GLanding);

            transform.parent.localScale = new Vector3(mActionInput.GFlip, transform.parent.localScale.y, transform.parent.localScale.z);
        }
    }
}
