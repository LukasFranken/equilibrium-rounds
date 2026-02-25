using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Equilibrium.Component.Tag
{
    class TagMarker : MonoBehaviour
    {
        private GameObject tagMarkerObject;

        private TagType tagType;
        private Vector3 storedPosition;
        private Transform target = null;
        private Vector3 targetOffset;

        public void Create(Vector3 pos)
        {
            storedPosition = pos;
            tagType = TagType.Static;

            if (tagMarkerObject == null)
                tagMarkerObject = CreateMarkerObject(storedPosition);
            else
                tagMarkerObject.transform.position = storedPosition;
        }

        public void Create(Transform targetTransform, Vector3 offset)
        {
            target = targetTransform;
            targetOffset = offset;
            tagType = TagType.Target;

            if (tagMarkerObject == null)
                tagMarkerObject = CreateMarkerObject(target.position + targetOffset);
            else
                tagMarkerObject.transform.position = target.position + targetOffset;
        }

        private GameObject CreateMarkerObject(Vector3 position)
        {
            if (tagMarkerObject != null) Reset();

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

            return marker;
        }

        public void UpdatePosition()
        {
            if (tagMarkerObject != null)
            {
                if (tagType == TagType.Target && target != null)
                {
                    tagMarkerObject.transform.position = target.position + targetOffset;
                }
                else if (tagType == TagType.Static)
                {
                    tagMarkerObject.transform.position = storedPosition;
                }
            }
        }

        public Vector3 GetPosition()
        {
            if (tagType == TagType.Target && target != null)
            {
                return target.position;
            }
            if (tagType == TagType.Static)
            {
                return storedPosition;
            }
            return Vector3.zero;
        }

        public Boolean HasObject()
        {
            return tagMarkerObject != null;
        }

        public void Reset()
        {
            if (tagMarkerObject != null)
            {
                Destroy(tagMarkerObject);
                tagMarkerObject = null;
            }
            storedPosition = Vector3.zero;
            target = null;
            targetOffset = Vector3.zero;
        }
    }
}
