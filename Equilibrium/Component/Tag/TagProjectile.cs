using UnityEngine;

namespace Equilibrium.Component.Tag
{
    class TagProjectile : MonoBehaviour
    {
        public TagMono? owner;
        private ProjectileHit? hit;
        private bool triggered = false;

        void Start()
        {
            hit = GetComponent<ProjectileHit>();
            if (hit != null)
            {
                hit.AddHitActionWithData(OnHit);
            }
        }

        private void OnHit(HitInfo hitInfo)
        {
            if (triggered) return;
            triggered = true;

            Vector3 offset = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.transform.position.z) - hitInfo.transform.position;
            owner?.SetTag(hitInfo.transform, offset);

            Destroy(gameObject);
        }
    }
}
