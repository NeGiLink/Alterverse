using UnityEngine;
using UnityEngine.Assertions;

namespace Kusume
{
    /*
    * 速度関係の宣言をまとめたインターフェース
    */
    public interface IVelocityComponent
    {
        Vector2 CurrentMove { get; }
        float CurrentMoveSpeed { get; }
        Vector2 CurrentVelocity { get; set; }
        Rigidbody2D Rigidbody2D { get; set; }

        void DeathCollider();
    }
    /*
     * キャラクターの速度、Rigidbody関係の処理を行うクラス
     */
    [System.Serializable]
    public class VelocityComponent : IVelocityComponent, ICharacterComponent<ICharacterSetup>
    {
        [SerializeField]
        private Rigidbody2D thisRigidbody2D;
        [SerializeField]
        private Collider2D collider2D;
        [SerializeField]
        private Vector2 currentVelocity = Vector3.zero;
        private bool needUpdateVelocity = true;

        [SerializeField]
        private bool inheritRigidbodyVelocity = true;
        public Vector2 CurrentVelocity
        {
            get
            {
                if (needUpdateVelocity && inheritRigidbodyVelocity)
                {
                    currentVelocity = thisRigidbody2D.linearVelocity;
                    needUpdateVelocity = false;
                }
                return currentVelocity;
            }
            set
            {
                needUpdateVelocity = false;
                currentVelocity = value;
            }
        }
        public Vector2 CurrentMove => new Vector2(CurrentVelocity.x, CurrentVelocity.y);
        public float CurrentMoveSpeed => CurrentMove.magnitude;

        public void DoSetup(ICharacterSetup chara)
        {
            thisRigidbody2D = chara.gameObject.GetComponent<Rigidbody2D>();
            collider2D = chara.gameObject.GetComponent<Collider2D>();
            Assert.IsNotNull(thisRigidbody2D);
        }

        public void DeathCollider()
        {
            collider2D.isTrigger = true;
        }

        public Rigidbody2D Rigidbody2D { get { return thisRigidbody2D; } set { thisRigidbody2D = value; } }
        public Collider2D Collider2D { get { return collider2D; } set { collider2D = value; } }
    }
}
