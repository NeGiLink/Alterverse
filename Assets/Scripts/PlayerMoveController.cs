using UnityEngine;
using UnityEngine.InputSystem;

namespace Kusume
{
    public class PlayerMoveController : MonoBehaviour
    {

        [SerializeField]
        private int mFlip;

        [SerializeField]
        private float mMoveSpeed;
        [SerializeField]
        private Vector2 mMoveValue;

        [SerializeField]
        private bool mJump;

        [SerializeField]
        private float mJumpPower;
        [SerializeField]
        private bool mGround;

        [SerializeField]
        private bool mAttack;


        [SerializeField]
        private InputSystem_Actions mInputActions;

        private Rigidbody2D mRigidbody2D;

        private Animator mAnimator;


        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
            mAnimator = GetComponent<Animator>();
            mInputActions = new InputSystem_Actions();
        }

        private void Start()
        {
            mFlip = -1;
            mJump = false;
            mAttack = false;

            
        }

        private void InputUpdate()
        {
            Vector2 moveValue = mInputActions.Player.Move.ReadValue<Vector2>();
            Debug.Log(moveValue);
            float moveX = moveValue.x;
            float moveY = moveValue.y;
            if(moveX > 0)
            {
                mFlip = -1;
            }
            else if (moveX < 0)
            {
                mFlip = 1;
            }

            if(moveY > 0 && mGround)
            {
                mJump = true;
            }


            mMoveValue = moveValue;
        }

        private void FixedUpdate()
        {
            Vector2 velocity = mRigidbody2D.linearVelocity;
            velocity.x = mMoveValue.x * mMoveSpeed * Time.deltaTime;


            mRigidbody2D.linearVelocity = velocity;

            if (mJump)
            {
                mRigidbody2D.AddForce(Vector2.up * mJumpPower,ForceMode2D.Impulse);
                mJump = false;
            }
        }

        private void Update()
        {
            InputUpdate();

            mAnimator.SetInteger("Move", (int)mMoveValue.x);
            mAnimator.SetBool("Jump 0", mGround);

            transform.localScale = new Vector3(mFlip,transform.localScale.y, transform.localScale.z);
        }

        private void OnEnable()
        {
            mInputActions.Enable();
        }

        private void OnDisable()
        {
            mInputActions.Disable();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            mGround = true;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            mGround = false;
        }
    }
}
