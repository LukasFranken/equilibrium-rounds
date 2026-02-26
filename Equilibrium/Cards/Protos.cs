using Equilibrium.Component;
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
            var mono = player.gameObject.GetComponent<HealthStackMono>();
            if (mono == null)
            {
                mono = player.gameObject.AddComponent<HealthStackMono>();
            }
            mono.AddIncrement();
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<HealthStackMono>();
            if (mono != null)
            {
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
            return "Convert damage to max health";
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
                    stat = "Conversion",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }

        public override string GetModName()
        {
            return Equilibrium.ModInitials;
        }
    }
}
