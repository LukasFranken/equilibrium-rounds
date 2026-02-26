using System.Collections;
using UnboundLib.GameModes;
using UnityEngine;

namespace Equilibrium.Component.Tag
{
    class TagMono : MonoBehaviour
    {
        private CharacterData? data;
        private Block? block;
        private Gun? gun;

        private bool spawnTagNextShot = false;

        private TagMarker tagMarker = new TagMarker();

        private float tagTeleportCooldownMultiplier = 0.2f;
        private float originalCooldown;

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
            if (tagMarker.HasObject())
            {
                tagMarker.Reset();
            }
            yield break;
        }

        IEnumerator WaitForGun()
        {
            while (data == null || data.weaponHandler == null || data.weaponHandler.gun == null)
                yield return null;

            gun = data.weaponHandler.gun;
            gun.ShootPojectileAction += OnBulletSpawned;
        }

        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            if (gun == null) return;
            if (block == null) return;

            if (tagMarker.HasObject())
            {
                Teleport();
                if (originalCooldown != 0f)
                {
                    block.cooldown = originalCooldown;
                }
                originalCooldown = 0f;
            }
            else
            {
                spawnTagNextShot = true;
                gun.Attack(0f, true, 0f, 0f, false);
                if (originalCooldown == 0f)
                {
                    originalCooldown = block.cooldown;
                    block.cooldown *= tagTeleportCooldownMultiplier;
                }
            }
        }
        
        private void OnBulletSpawned(GameObject bullet)
        {
            if (!spawnTagNextShot) return;
            spawnTagNextShot = false;

            TagProjectile tagProjectile = bullet.AddComponent<TagProjectile>();
            tagProjectile.owner = this;
            MakeBulletRed(bullet);
        }

        private void MakeBulletRed(GameObject bullet)
        {
            var renderers = bullet.GetComponentsInChildren<Renderer>();

            foreach (var r in renderers)
            {
                foreach (var mat in r.materials)
                {
                    if (mat.HasProperty("_Color"))
                        mat.color = Color.red;

                    if (mat.HasProperty("_EmissionColor"))
                        mat.SetColor("_EmissionColor", Color.red * 2f);
                }
            }
        }

        void Update()
        {
            tagMarker.UpdatePosition();
        }
        
        public void SetTag(Vector3 pos)
        {
            tagMarker.Create(pos);
        }

        public void SetTag(Transform targetTransform, Vector3 offset)
        {
            tagMarker.Create(targetTransform, offset);
        }

        public void Teleport()
        {
            if (data == null) return;
            data.transform.position = tagMarker.GetPosition();
            tagMarker.Reset();
        }

        public void ResetTag()
        {
            tagMarker.Reset();
        }

        void OnDestroy()
        {
            if (block != null)
                block.BlockAction -= OnBlock;

            if (gun != null)
                gun.ShootPojectileAction -= OnBulletSpawned;
        }
    }
}
