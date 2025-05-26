using UnityEngine;

namespace Kusume
{
    /*
     * 移動処理クラスのインターフェース
     */
    public interface IMovement
    {
        void Stop();
        void Move(float maxSpeed);
    }
    /*
     * キャラクターの移動処理を行うクラス
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
