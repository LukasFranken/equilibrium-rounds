using UnboundLib.Cards;
using UnityEngine;

namespace Equilibrium.Cards
{
    class ProtosDamage : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.damage = 1.1f;
            cardInfo.categories = new CardCategory[] { Protos.ProtosUpgradeCategory };
            cardInfo.allowMultiple = true;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle() => "Protos: Damage";
        protected override string GetDescription() => "+10% damage";
        protected override GameObject GetCardArt() => null;
        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Common;
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Damage",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.TechWhite;
        public override string GetModName() => Equilibrium.ModInitials;
    }
}
