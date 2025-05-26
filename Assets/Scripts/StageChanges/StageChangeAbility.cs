using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Kusume
{
    public class StageChangeAbility : MonoBehaviour
    {
        [SerializeField]
        private bool mChangeing;

        [SerializeField]
        [Range(0f, 1f)]
        private float mCutoffRadius;

        [SerializeField]
        private bool mCurrentFrontStage;


        [SerializeField]
        private GameObject mStageA;

        [SerializeField]
        private GameObject mStageB;

        [SerializeField]
        private Camera mCameraA;

        [SerializeField]
        private Camera mCameraB;

        [SerializeField]
        private RawImage mRenderTexture;




        [SerializeField]
        private InputSystem_Actions mInputActions;

        private InputAction mKButton;

        private bool mKFrag;

        private InputAction mLButton;

        private bool mLFrag;

        private void Awake()
        {
            mInputActions = new InputSystem_Actions();
        }

        private void Start()
        {
            mCurrentFrontStage = true;
        }

        private void OnEnable()
        {
            mInputActions.Enable();
            mKButton = mInputActions.Player.StageChange1;
            mLButton = mInputActions.Player.StageChange2;
            
            mKButton.Enable();
            mLButton.Enable();


            mKButton.performed += OnKButton;
            mLButton.performed += OnLButton;
        }

        private void OnDisable()
        {
            mKButton.performed -= OnKButton;
            mLButton.performed -= OnLButton;

            mKButton.Disable();
            mLButton.Disable();
            mInputActions.Disable();
        }


        private void OnKButton(InputAction.CallbackContext context)
        {
            mKFrag = true;
            OnChangeStage();
            // 一瞬だけtrueにして、次のフレームでfalseに戻す
            StartCoroutine(ResetKButtonPress());
        }

        private System.Collections.IEnumerator ResetKButtonPress()
        {
            yield return null; // 1フレーム待つ
            mKFrag = false;
        }

        private void OnLButton(InputAction.CallbackContext context)
        {
            mLFrag = true;
            OnChangeStage();
            // 一瞬だけtrueにして、次のフレームでfalseに戻す
            StartCoroutine(ResetLButtonPress());
        }

        private System.Collections.IEnumerator ResetLButtonPress()
        {
            yield return null; // 1フレーム待つ
            mLFrag = false;
        }


        private void OnChangeStage()
        {
            if (mChangeing) { return; }
            if (mKFrag && mLFrag)
            {
                mStageA.SetActive(true);
                mStageB.SetActive(true);
                mCameraA.gameObject.SetActive(true);
                mCameraB.gameObject.SetActive(true);
                mRenderTexture.gameObject.SetActive(true);

                if (mCurrentFrontStage)
                {
                    mCutoffRadius = 1.0f;
                }
                else
                {
                    mCutoffRadius = 0.0f;
                }
                mChangeing = true;
            }
        }


        private void Update()
        {
            if (mChangeing)
            {
                Material mat = mRenderTexture.material;
                float cutoffRadius = mat.GetFloat("_Cutoff");
                if (mCurrentFrontStage)
                {
                    cutoffRadius += 1.0f * Time.deltaTime;
                    if(cutoffRadius >= mCutoffRadius)
                    {
                        cutoffRadius = mCutoffRadius;
                        mCurrentFrontStage = !mCurrentFrontStage;
                        mStageA.SetActive(false);
                        mStageB.SetActive(true);
                        mCameraA.gameObject.SetActive(false);
                        mCameraB.gameObject.SetActive(false);
                        mRenderTexture.gameObject.SetActive(false);
                        mChangeing = false;
                    }
                }
                else
                {
                    cutoffRadius -= 1.0f * Time.deltaTime;
                    if (cutoffRadius <= mCutoffRadius)
                    {
                        cutoffRadius = mCutoffRadius;
                        mCurrentFrontStage = !mCurrentFrontStage;
                        mStageA.SetActive(true);
                        mStageB.SetActive(false);
                        mCameraA.gameObject.SetActive(false);
                        mCameraB.gameObject.SetActive(false);
                        mRenderTexture.gameObject.SetActive(false);
                        mChangeing = false;
                    }
                }
                mat.SetFloat("_Cutoff", cutoffRadius);
            }
        }
    }
}
