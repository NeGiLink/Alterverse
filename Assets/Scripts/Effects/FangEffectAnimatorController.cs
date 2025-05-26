using UnityEngine;

namespace Kusume
{
    public class FangEffectAnimatorController : MonoBehaviour
    {
        private Animator mAnimator;

        private void Awake()
        {
            mAnimator = GetComponentInChildren<Animator>();
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            mAnimator.StopPlayback();
            mAnimator.gameObject.SetActive(false);
        }

        public void EffectStart()
        {
            mAnimator.gameObject.SetActive(true);
            mAnimator.StartPlayback();
            mAnimator.Play("Attack",0, 0);
            mAnimator.speed = 1f;
        }

        public void EffectEnd()
        {
            mAnimator.speed = 0f;
            mAnimator.gameObject.SetActive(false);
        }

        public void EffectTimeEnd()
        {
            if(mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                mAnimator.speed = 0f;
                mAnimator.gameObject.SetActive(false);
            }
        }
    }
}
