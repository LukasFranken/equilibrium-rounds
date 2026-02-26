using System.Collections;
using UnityEngine;

namespace Equilibrium.Component
{
    public class HealthStackMono : MonoBehaviour
    {
        public CharacterData? data;
        public HealthHandler? health;

        public float baseFactor = 0.25f;
        public float increment = 0f;
        public float currentMultiplier;

        public bool resetPerRound = true;

        private float lastHealth;

        void Start()
        {
            data = GetComponent<CharacterData>();
            StartCoroutine(WaitForHealth());
        }

        private IEnumerator WaitForHealth()
        {
            while (data == null || data.healthHandler == null)
                yield return null;

            health = data.healthHandler;
        }

        void Update()
        {
            if (health == null) return;
            if (data == null) return;

            if (data.health != lastHealth)
            {
                if (data.health < lastHealth)
                {
                    OnDamageTaken(lastHealth - data.health);
                }
                lastHealth = data.health;
            }
        }

        private void OnDamageTaken(float damage)
        {
            if (data == null) return;
            data.maxHealth += baseFactor * increment * damage;
        }

        public void AddIncrement()
        {
            increment += 1f;
        }

        void OnDestroy()
        {
            increment = 0f;
        }
    }
}
