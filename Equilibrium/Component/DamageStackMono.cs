using System.Collections;
using UnityEngine;

namespace Equilibrium.Component
{
    public class DamageStackMono : MonoBehaviour
    {
        public CharacterData? data;
        public Gun? gun;

        public float baseMultiplier = 0.2f;
        public float increment = 0.03f;
        public float incrementIncrement = 0f;
        public float currentMultiplier;

        public bool resetPerRound = true;

        void Start()
        {
            ResetMultiplier();
            data = GetComponent<CharacterData>();
            StartCoroutine(WaitForGun());
        }

        private IEnumerator WaitForGun()
        {
            while (data == null || data.weaponHandler == null || data.weaponHandler.gun == null)
                yield return null;

            gun = data.weaponHandler.gun;
            gun.ShootPojectileAction += OnShoot;
        }

        private void OnShoot(GameObject bullet)
        {
            var proj = bullet.GetComponent<ProjectileHit>();
            if (proj != null)
            {
                proj.damage *= currentMultiplier;
            }
            currentMultiplier += (increment * incrementIncrement);
        }

        public void ResetMultiplier()
        {
            currentMultiplier = baseMultiplier;
        }

        public void AddIncrement()
        {
            incrementIncrement += 1f;
        }

        void OnDestroy()
        {
            if (gun != null)
                gun.ShootPojectileAction -= OnShoot;
        }
    }

}
