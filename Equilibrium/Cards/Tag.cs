using Equilibrium.Component.Tag;
using UnboundLib.Cards;
using UnityEngine;

namespace Equilibrium.Cards
{
    class Tag : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.projectileSpeed = 1.5f;
            statModifiers.health = 0.8f;
            block.cdAdd = -0.25f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<TagMono>();
            if (mono == null)
            {
                mono = player.gameObject.AddComponent<TagMono>();
            }
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<TagMono>();
            if (mono != null)
            {
                mono.ResetTag();
                mono.enabled = false;
                Destroy(mono);
            }
        }

        protected override string GetTitle()
        {
            return "Tag";
        }
        protected override string GetDescription()
        {
            return "Block to throw a Tag\nBlock again to teleport to the Tag";
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
                    stat = "Projectile Speed",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Block Cooldown",
                    amount = "-25%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Health",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }
        public override string GetModName()
        {
            return Equilibrium.ModInitials;
        }
    }
}
