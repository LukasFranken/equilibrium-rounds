using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Equilibrium.Component
{
    class AttackSpeedStackMono : MonoBehaviour
    {
        public CharacterData data;
        public Gun gun;

        public float baseValue = 0.25f;
        public float increment = 0.01f;
        public float maxReduction = 0.2f;

        public float incrementIncrement = 0f;
        public float currentReduction = 0f;

        void Start()
        {
            data = GetComponent<CharacterData>();
            StartCoroutine(WaitForGun());
        }

        private IEnumerator WaitForGun()
        {
            while (data.weaponHandler == null || data.weaponHandler.gun == null)
                yield return null;

            gun = data.weaponHandler.gun;
            gun.ShootPojectileAction += OnShoot;
            gun.attackSpeed = baseValue;
        }

        private void OnShoot(GameObject bullet)
        {
            currentReduction += (increment * incrementIncrement);
            gun.attackSpeed = Mathf.Clamp(baseValue - currentReduction, baseValue - maxReduction, baseValue);
        }

        public void Update()
        {
            if (!data.playerActions.Fire.IsPressed || gun.isReloading)
            {
                if (currentReduction > 0f)
                {
                    currentReduction = 0f;
                    gun.attackSpeed = baseValue;
                }
            }
        }

        public void AddIncrement()
        {
            incrementIncrement += 1f;
        }
    }
}
