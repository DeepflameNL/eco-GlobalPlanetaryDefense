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

    [LocDisplayName("Seismic Stabilization Platform"), LocDescription("Big, heavy, stable, and flat. Perfect to mount a laser onto.")]
    [Serialized, MaxStackSize(10), Weight(1000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class SeismicStabilizationPlatformItem : Item { }

    [RequiresSkill(typeof(AdvancedMasonrySkill), 5)]
    public partial class SeismicStabilizationPlatformRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(AdvancedMasonrySkill);
        private static Type FocusedTalent => typeof(AdvancedMasonryFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(AdvancedMasonryParallelSpeedTalent);
        private static Type CraftingStation => typeof(AdvancedMasonryTableObject);

        public SeismicStabilizationPlatformRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "SeismicStabilizationPlatform",
                displayName: Localizer.DoStr("Seismic Stabilization Platform"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(ReinforcedConcreteItem), 250, true),
                    new IngredientElement(typeof(TimberPileItem), 12, true),
                    new IngredientElement(typeof(MortarItem), 200, true),
                    new IngredientElement(typeof(CrushedSlagItem), 200, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<SeismicStabilizationPlatformItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(6000, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 360f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
