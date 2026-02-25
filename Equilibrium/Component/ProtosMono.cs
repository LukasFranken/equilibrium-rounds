using ModdingUtils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnboundLib.GameModes;
using UnityEngine;

namespace Equilibrium.Component
{
    class ProtosMono : MonoBehaviour
    {
        private CharacterData data;
        private bool initialized;

        private void Start()
        {
            data = GetComponent<CharacterData>();
            if (initialized)
            {
                ApplyProtosCardFilter();
            }
        }

        public void Initialize()
        {
            if (initialized)
            {
                return;
            }

            initialized = true;
            GameModeManager.AddHook(GameModeHooks.HookPointStart, OnPointStart);
            ApplyProtosCardFilter();
        }

        private IEnumerator OnPointStart(IGameModeHandler gameMode)
        {
            ApplyProtosCardFilter();
            yield break;
        }

        private void ApplyProtosCardFilter()
        {
            if (data == null || data.stats == null)
            {
                return;
            }

            object additionalData = CharacterStatModifiersExtension.GetAdditionalData(data.stats);
            Type additionalDataType = additionalData.GetType();

            CardCategory protosCategory = Cards.Protos.ProtosUpgradeCategory;

            TrySetCategoryCollection(additionalDataType.GetField("allowedCardCategories", BindingFlags.Public | BindingFlags.Instance), additionalData, protosCategory, clearFirst: true);
            TrySetCategoryCollection(additionalDataType.GetField("allowedCategories", BindingFlags.Public | BindingFlags.Instance), additionalData, protosCategory, clearFirst: true);

            var blacklistedField = additionalDataType.GetField("blacklistedCategories", BindingFlags.Public | BindingFlags.Instance);
            if (blacklistedField != null)
            {
                var blacklisted = blacklistedField.GetValue(additionalData) as IList<CardCategory>;
                if (blacklisted != null)
                {
                    for (int i = blacklisted.Count - 1; i >= 0; i--)
                    {
                        if (blacklisted[i] == protosCategory)
                        {
                            blacklisted.RemoveAt(i);
                        }
                    }
                }
            }
        }

        private static void TrySetCategoryCollection(FieldInfo fieldInfo, object additionalData, CardCategory protosCategory, bool clearFirst)
        {
            if (fieldInfo == null)
            {
                return;
            }

            var collection = fieldInfo.GetValue(additionalData) as IList<CardCategory>;
            if (collection == null)
            {
                return;
            }

            if (clearFirst)
            {
                collection.Clear();
            }

            if (!collection.Contains(protosCategory))
            {
                collection.Add(protosCategory);
            }
        }

        public void Reset()
        {
            if (!initialized)
            {
                return;
            }

            initialized = false;
            GameModeManager.RemoveHook(GameModeHooks.HookPointStart, OnPointStart);

            if (data == null || data.stats == null)
            {
                return;
            }

            object additionalData = CharacterStatModifiersExtension.GetAdditionalData(data.stats);
            Type additionalDataType = additionalData.GetType();
            CardCategory protosCategory = Cards.Protos.ProtosUpgradeCategory;

            TryRemoveFromCategoryCollection(additionalDataType.GetField("allowedCardCategories", BindingFlags.Public | BindingFlags.Instance), additionalData, protosCategory);
            TryRemoveFromCategoryCollection(additionalDataType.GetField("allowedCategories", BindingFlags.Public | BindingFlags.Instance), additionalData, protosCategory);
        }

        private static void TryRemoveFromCategoryCollection(FieldInfo fieldInfo, object additionalData, CardCategory protosCategory)
        {
            if (fieldInfo == null)
            {
                return;
            }

            var collection = fieldInfo.GetValue(additionalData) as IList<CardCategory>;
            if (collection == null)
            {
                return;
            }

            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (collection[i] == protosCategory)
                {
                    collection.RemoveAt(i);
                }
            }
        }

        private void OnDestroy()
        {
            Reset();
        }
    }
}
