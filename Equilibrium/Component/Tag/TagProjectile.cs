using UnityEngine;

namespace Equilibrium.Component.Tag
{
    class TagProjectile : MonoBehaviour
    {
        public TagMono owner;
        private ProjectileHit hit;
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

            var health = hitInfo.transform?.GetComponent<HealthHandler>();
            if (health != null)
            {
                Vector3 offset = new Vector3(hitInfo.point.x, hitInfo.point.y, health.transform.position.z) - health.transform.position;
                owner?.SetTag(health.transform, offset);
            }
            else
            {
                owner?.SetTag(hitInfo.point);
            }

            Destroy(gameObject);
        }
    }
}
