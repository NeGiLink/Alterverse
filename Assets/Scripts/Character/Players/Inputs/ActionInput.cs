using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kusume
{
    public class ActionInput : MonoBehaviour
    {
        [SerializeField]
        private InputSystem_Actions mInputActions;

        [SerializeField]
        private int                 mFlip;


        [SerializeField]
        private Vector2             mMoveValue;


        [SerializeField]
        private bool                mJump;


        [SerializeField]
        private bool                mAttack;

        private InputAction         mAttackAction;
        public int                  GFlip => mFlip; 
 
        public bool                GAttack => mAttack;

        public Vector2              GMoveValue => mMoveValue;
        public bool                 GJump => mJump;
        public void SetJumpFrag(bool b) {  mJump = b; }

        private void Awake()
        {
            mInputActions = new InputSystem_Actions();
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            mFlip = -1;
            mJump = false;
        }
        private void OnEnable()
        {
            mInputActions.Enable();
            mAttackAction = mInputActions.Player.Attack;
            mAttackAction.performed += OnAttack;

            mAttackAction.Enable();
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            mAttack = true;
            // 一瞬だけtrueにして、次のフレームでfalseに戻す
            StartCoroutine(ResetAttackButtonPress());
        }

        private System.Collections.IEnumerator ResetAttackButtonPress()
        {
            yield return null; // 1フレーム待つ
            mAttack = false;
        }

        private void OnDisable()
        {
            mAttackAction.performed -= OnAttack;

            mAttackAction.Disable();

            mInputActions.Disable();
        }

        // Update is called once per frame
        private void Update()
        {
            InputUpdate();
        }

        private void InputUpdate()
        {
            Vector2 moveValue = mInputActions.Player.Move.ReadValue<Vector2>();
            float moveX = moveValue.x;
            float moveY = moveValue.y;

            if (moveX > 0)
            {
                mFlip = -1;
            }
            else if (moveX < 0)
            {
                mFlip = 1;
            }

            if(moveY > 0)
            {
                mJump = true;
            }

            mMoveValue = moveValue;
        }
    }
}
