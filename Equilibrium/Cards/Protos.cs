using Equilibrium.Component;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnityEngine;

namespace Equilibrium.Cards
{
    class Protos : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {

        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<ProtosMono>();
            if (mono == null)
            {
                mono = player.gameObject.AddComponent<ProtosMono>();
            }
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<ProtosMono>();
            if (mono != null)
            {
                mono.Reset();
                mono.enabled = false;
                Destroy(mono);
            }
        }

        protected override string GetTitle()
        {
            return "Protos";
        }
        protected override string GetDescription()
        {
            return "Collect gears to power up";
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
                    stat = "Effect",
                    amount = "No",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
        }
        public override string GetModName()
        {
            return Equilibrium.ModInitials;
        }
    }
}
