namespace DF.GlobalPlanetaryDefense
{
    using Eco.Core.Items;
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [LocDisplayName("Bone"), LocDescription("The structure upon which most bodies are built. Rich in calcium and phosphor!")]
    [Serialized, MaxStackSize(30), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class BoneItem : Item { }
}
