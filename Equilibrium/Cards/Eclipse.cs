using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnityEngine;

namespace Equilibrium.Cards
{
    class Eclipse : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.gravity = 0.2f;
            gun.damage = 2f;
            gun.projectileSpeed = 0.6f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }

        protected override string GetTitle()
        {
            return "Eclipse";
        }
        protected override string GetDescription()
        {
            return "By the power of the moon!";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "+100%",
                    simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Gravity",
                    amount = "-80%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },new CardInfoStat()
                {
                    positive = false,
                    stat = "Bullet Speed",
                    amount = "-40%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }
        public override string GetModName()
        {
            return Equilibrium.ModInitials;
        }
    }
}
