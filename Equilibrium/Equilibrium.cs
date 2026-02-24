using UnboundLib;
using UnboundLib.Cards;
using Equilibrium.Cards;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using BepInEx;


namespace Equilibrium
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class Equilibrium : BaseUnityPlugin
    {
        private const string ModId = "de.instinct.equilibrium";
        private const string ModName = "Equilibrium";
        public const string Version = "0.0.2";

        public const string ModInitials = "EQ";
        public static Equilibrium instance { get; private set; }

        void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }
        void Start()
        {
            instance = this;
            CustomCard.BuildCard<Evasion>();
            CustomCard.BuildCard<Cannonball>();
            CustomCard.BuildCard<Metamorphosis>();
            CustomCard.BuildCard<Minigun>();
        }
    }
}
