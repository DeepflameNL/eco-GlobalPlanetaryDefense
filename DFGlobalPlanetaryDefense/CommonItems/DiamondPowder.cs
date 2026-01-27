namespace DF.GlobalPlanetaryDefense
{
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using System;
    using System.Collections.Generic;

    [LocDisplayName("Diamond Powder"), LocDescription("Finely ground down diamonds, perfect for a high hardness polishing agent.")]
    [Serialized, MaxStackSize(50), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class DiamondPowderItem : Item { }

    [RequiresSkill(typeof(MillingSkill), 7)]
    public partial class DiamondPowderRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(MillingSkill);
        private static Type ResourceTalent => typeof(MillingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(MillingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(MillingParallelSpeedTalent);
        private static Type CraftingStation => typeof(IndustrialMillObject);

        public DiamondPowderRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "DiamondPowder",
                displayName: Localizer.DoStr("Diamond Powder"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(SyntheticDiamondItem), 2, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<DiamondPowderItem>(3)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(250, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 1.2f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

            this.ModsPreInitialize();
            this.Initialize(displayText: recipe.DisplayName, recipeType: this.GetType());
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: CraftingStation, recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
