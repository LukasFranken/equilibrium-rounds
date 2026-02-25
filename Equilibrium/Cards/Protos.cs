using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using Equilibrium.Component;
using UnboundLib.Cards;
using UnityEngine;

namespace Equilibrium.Cards
{
    class Protos : CustomCard
    {
        public static readonly CardCategory ProtosUpgradeCategory = CustomCardCategories.instance.CardCategory("EQ_PROTOS_UPGRADE");

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<ProtosMono>();
            if (mono == null)
            {
                mono = player.gameObject.AddComponent<ProtosMono>();
            }

            mono.Initialize();
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
            return "Only Protos upgrades appear for you from now on";
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
                    stat = "Card Choices",
                    amount = "Only Protos",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Per Round",
                    amount = "+10% damage / speed / health",
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
