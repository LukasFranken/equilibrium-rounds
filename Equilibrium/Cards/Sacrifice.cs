using Equilibrium.Component;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnityEngine;

namespace Equilibrium.Cards
{
    class Sacrifice : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<SacrificeMono>();
            if (mono == null)
            {
                mono = player.gameObject.AddComponent<SacrificeMono>();
            }
            mono.AddIncrement();
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<SacrificeMono>();
            if (mono != null)
            {
                mono.enabled = false;
                Destroy(mono);
            }
        }

        protected override string GetTitle()
        {
            return "Sacrifice";
        }
        protected override string GetDescription()
        {
            return "Block to take damage\nincreases next bullet's power";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = false,
                    stat = "50%",
                    amount = "Current HP toll",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "+30%",
                    amount = "HP to Damage",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "+20%",
                    amount = "HP to Speed",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Walls",
                    amount = "IGNORES",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }
        public override string GetModName()
        {
            return Equilibrium.ModInitials;
        }
    }
}
