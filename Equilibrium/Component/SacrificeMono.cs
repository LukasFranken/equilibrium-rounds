using Equilibrium.Component.Tag;
using System;
using System.Collections;
using UnboundLib.GameModes;
using UnityEngine;

namespace Equilibrium.Component
{
    class SacrificeMono : MonoBehaviour
    {
        public CharacterData data;
        public Gun gun;
        public Block block;

        private float storedHealthSacrifice;
        private float increment;

        private float hpToDamageFactor = 0.3f;
        private float hpToProjectileSpeedFactor = 0.02f;

        void Start()
        {
            data = GetComponent<CharacterData>();
            block = GetComponent<Block>();
            block.BlockAction += OnBlock;
            StartCoroutine(WaitForGun());
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, OnPointEnd);
        }

        private IEnumerator OnPointEnd(IGameModeHandler gameMode)
        {
            storedHealthSacrifice = 0f;
            yield break;
        }

        private IEnumerator WaitForGun()
        {
            while (data.weaponHandler == null || data.weaponHandler.gun == null)
                yield return null;

            gun = data.weaponHandler.gun;
            gun.ShootPojectileAction += OnShoot;
        }

        void Update()
        {
            if (storedHealthSacrifice > 0f)
            {
                gun.ignoreWalls = true;
                gun.gravity = 0f;
            }
            else
            {
                gun.ignoreWalls = false;
                gun.gravity = 1f;
            }
        }

        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            float dmgToTake = data.health * 0.5f;
            data.healthHandler.RPCA_SendTakeDamage(new Vector2(dmgToTake, 0f), data.healthHandler.transform.position);

            storedHealthSacrifice += dmgToTake;
        }

        private void OnShoot(GameObject bullet)
        {
            if (storedHealthSacrifice <= 0f) return;

            MakeBulletRedLaser(bullet);

            var proj = bullet.GetComponent<ProjectileHit>();
            if (proj != null)
            {
                proj.unblockable = true;
                proj.damage += (storedHealthSacrifice * increment * hpToDamageFactor);
                proj.GetComponent<MoveTransform>().localForce *= 1f + (storedHealthSacrifice * increment * hpToProjectileSpeedFactor);
            }
            
            storedHealthSacrifice = 0f;
        }

        private void MakeBulletRedLaser(GameObject bullet)
        {
            var renderers = bullet.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                foreach (var mat in r.materials)
                {
                    if (mat.HasProperty("_Color"))
                        mat.color = Color.magenta;

                    if (mat.HasProperty("_EmissionColor"))
                        mat.SetColor("_EmissionColor", Color.magenta * 3f);
                }
            }
        }

        public void AddIncrement()
        {
            increment += 1f;
        }

        void OnDestroy()
        {
            if (block != null)
                block.BlockAction -= OnBlock;

            if (gun != null)
                gun.ShootPojectileAction -= OnShoot;
        }
    }
}
