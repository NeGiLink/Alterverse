using UnityEngine;

namespace Kusume
{
    /*
     * �ړ������N���X�̃C���^�[�t�F�[�X
     */
    public interface IMovement
    {
        void Stop();
        void Move(float maxSpeed);
    }
    /*
     * �L�����N�^�[�̈ړ��������s���N���X
     */
    [System.Serializable]
    public class Movement : IMovement, ICharacterComponent<ICharacterSetup>
    {
        private Transform thisTransform;

        private IVelocityComponent velocity;

        public void DoSetup(ICharacterSetup chara)
        {
            thisTransform = chara.gameObject.transform;
            velocity = chara.Velocity;
        }

        public void Stop()
        {
            velocity.CurrentVelocity = Vector2.zero;
            velocity.Rigidbody2D.linearVelocity = Vector2.zero;
        }

        public void Move(float maxSpeed)
        {
            var moveVelocity = velocity.CurrentVelocity;
            moveVelocity = moveVelocity * maxSpeed;
            velocity.Rigidbody2D.linearVelocity = new Vector2(moveVelocity.x, velocity.Rigidbody2D.linearVelocity.y);
        }
    }
}
