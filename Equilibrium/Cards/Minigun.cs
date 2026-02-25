using Equilibrium.Component;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnityEngine;

namespace Equilibrium.Cards
{
    class Minigun : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.dontAllowAutoFire = false;
            gun.damage = 0.3f;
            gun.reloadTime = 3f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.automaticReload = false;
            gunAmmo.maxAmmo += 30;
            gun.spread = 0.15f;
            
            var mono = player.gameObject.GetComponent<AttackSpeedStackMono>();
            if (mono == null)
            {
                mono = player.gameObject.AddComponent<AttackSpeedStackMono>();
            }
            mono.AddIncrement();
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            UnityEngine.Debug.Log("Removing Minigun");
            var mono = player.gameObject.GetComponent<AttackSpeedStackMono>();
            if (mono != null)
            {
                mono.enabled = false;
                Destroy(mono);
            }
        }

        protected override string GetTitle()
        {
            return "Minigun";
        }
        protected override string GetDescription()
        {
            return "Attack Speed accelerates while firing";
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
                    stat = "Bullets",
                    amount = "+30",
                    simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-70%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload Speed",
                    amount = "-70%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Auto Reload",
                    amount = "NO",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Attack Speed",
                    amount = "FIXED",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }
        public override string GetModName()
        {
            return Equilibrium.ModInitials;
        }
    }
}
