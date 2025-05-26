using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace Kusume
{
    public class LandingChecker : MonoBehaviour
    {
        [SerializeField]
        private Vector3 mOriginOffset;

        [SerializeField]
        private float mRayLength;
        [SerializeField]
        private Vector3 mRayDirection;
        [SerializeField]
        private float mSphereRadius;

        [SerializeField]
        private LayerMask mMask;

        private bool mCurrentLanding;

        private bool mPastLanding;

        public bool GLanding => mCurrentLanding;
        public bool GPastLanding => mPastLanding;

        // Update is called once per frame
        private void Update()
        {
            mPastLanding = mCurrentLanding;
            if (HitCheck())
            {
                mCurrentLanding = true;
            }
            else
            {
                mCurrentLanding = false;
            }
        }

        private bool HitCheck()
        {
            RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position + mOriginOffset, mSphereRadius, mRayDirection, mRayLength);
            
            for(int i = 0; i < hit.Length;i++)
            {
                if (hit[i].collider.gameObject.tag == "Ground")
                {
                    DebugDrawCircleCast(transform.position + mOriginOffset, mSphereRadius, mRayDirection, mRayLength, Color.red);
                    return true;
                }
            }
            return false;
        }

        void DebugDrawCircleCast(Vector2 origin, float radius, Vector2 direction, float distance, Color color)
        {
            Vector2 end = origin + direction.normalized * distance;

            // ü‚ð•`‚­
            Debug.DrawLine(origin + Vector2.Perpendicular(direction.normalized) * radius, end + Vector2.Perpendicular(direction.normalized) * radius, color);
            Debug.DrawLine(origin - Vector2.Perpendicular(direction.normalized) * radius, end - Vector2.Perpendicular(direction.normalized) * radius, color);
            Debug.DrawLine(origin + Vector2.Perpendicular(direction.normalized) * radius, origin - Vector2.Perpendicular(direction.normalized) * radius, color);
            Debug.DrawLine(end + Vector2.Perpendicular(direction.normalized) * radius, end - Vector2.Perpendicular(direction.normalized) * radius, color);

            // Žn“_‚ÆI“_‚Ì‰~‚ð•`‚­
            DrawCircle(origin, radius, color);
            DrawCircle(end, radius, color);
        }

        void DrawCircle(Vector2 center, float radius, Color color, int segments = 16)
        {
            float angleStep = 360f / segments;
            Vector2 prevPoint = center + new Vector2(Mathf.Cos(0), Mathf.Sin(0)) * radius;

            for (int i = 1; i <= segments; i++)
            {
                float rad = Mathf.Deg2Rad * angleStep * i;
                Vector2 nextPoint = center + new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;
                Debug.DrawLine(prevPoint, nextPoint, color);
                prevPoint = nextPoint;
            }
        }
    }
}
