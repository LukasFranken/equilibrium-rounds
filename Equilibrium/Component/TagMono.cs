using System.Collections;
using UnboundLib.GameModes;
using UnityEngine;

namespace Equilibrium.Component
{
    class TagMono : MonoBehaviour
    {
        private CharacterData data;
        private Block block;
        private Gun gun;

        private TagType tagType = TagType.None;
        private Vector3 storedPosition;
        private Transform target = null;

        private bool spawnTagNextShot = false;

        private float originalCooldown;

        private GameObject tagMarker;
        public GameObject tagMarkerPrefab;
        private Vector3 targetOffset;

        private float tagDuration = 5f;
        private float lastTagTime = 0f;

        void Start()
        {
            data = GetComponent<CharacterData>();
            block = GetComponent<Block>();

            block.BlockAction += OnBlock;

            StartCoroutine(WaitForGun());
        }

        IEnumerator WaitForGun()
        {
            while (data.weaponHandler == null || data.weaponHandler.gun == null)
                yield return null;

            gun = data.weaponHandler.gun;
            gun.ShootPojectileAction += OnBulletSpawned;
        }

        void OnDestroy()
        {
            if (block != null)
                block.BlockAction -= OnBlock;

            if (gun != null)
                gun.ShootPojectileAction -= OnBulletSpawned;
        }

        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            if (gun == null) return;

            if (tagType == TagType.None)
            {
                spawnTagNextShot = true;
                gun.Attack(0f, true, 0f, 0f, false);
                if (originalCooldown == 0f)
                {
                    originalCooldown = block.cooldown;
                    block.cooldown *= 0.5f;
                }
            }
            else
            {
                Teleport();
                block.cooldown = originalCooldown;
                originalCooldown = 0f;
            }
        }

        private void OnBulletSpawned(GameObject bullet)
        {
            if (!spawnTagNextShot) return;
            spawnTagNextShot = false;

            TagProjectile tagProjectile = bullet.AddComponent<TagProjectile>();
            tagProjectile.owner = this;

            bullet.tag = "TagBullet";
        }

        void Update()
        {
            if (tagMarker != null)
            {
                if (lastTagTime + tagDuration < Time.time)
                {
                    ResetTag();
                    return;
                }
                if (tagType == TagType.Target && target != null)
                {
                    tagMarker.transform.position = target.position + targetOffset;
                }
                else if (tagType == TagType.Static)
                {
                    tagMarker.transform.position = storedPosition;
                }
            }
        }

        public void SetTag(Vector3 pos)
        {
            storedPosition = pos;
            tagType = TagType.Static;

            if (tagMarker == null)
                tagMarker = CreateMarker(storedPosition);
            else
                tagMarker.transform.position = storedPosition;
        }

        public void SetTarget(Transform targetTransform, Vector3 offset)
        {
            target = targetTransform;
            targetOffset = offset;
            tagType = TagType.Target;

            if (tagMarker == null)
                tagMarker = CreateMarker(target.position + targetOffset);
            else
                tagMarker.transform.position = target.position + targetOffset;
        }

        private GameObject CreateMarker(Vector3 position)
        {
            GameObject marker = new GameObject("TagMarker");

            marker.transform.position = position;
            marker.transform.localScale = Vector3.one * 1f;

            var renderer = marker.AddComponent<SpriteRenderer>();

            Texture2D tex = new Texture2D(32, 32);
            Color[] colors = new Color[32 * 32];
            for (int i = 0; i < colors.Length; i++) colors[i] = Color.clear;
            int radius = 15;
            for (int y = 0; y < 32; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    int dx = x - 16;
                    int dy = y - 16;
                    if (dx * dx + dy * dy <= radius * radius)
                        colors[y * 32 + x] = Color.red;
                }
            }
            tex.SetPixels(colors);
            tex.Apply();

            renderer.sprite = Sprite.Create(tex, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));
            renderer.sortingOrder = 10;

            lastTagTime = Time.time;
            return marker;
        }

        public void Teleport()
        {
            if (tagType == TagType.Target && target != null)
            {
                data.transform.position = target.position;
            }
            if (tagType == TagType.Static)
            {
                data.transform.position = storedPosition;
            }

            if (tagMarker != null)
            {
                Destroy(tagMarker);
                tagMarker = null;
            }

            target = null;
            storedPosition = Vector3.zero;
            tagType = TagType.None;
        }

        public void ResetTag()
        {
            if (tagMarker != null)
            {
                Destroy(tagMarker);
                tagMarker = null;
            }
            tagType = TagType.None;
            storedPosition = Vector3.zero;
            target = null;
            targetOffset = Vector3.zero;
        }
    }

    enum TagType
    {
        Static,
        Target,
        None
    }
}
