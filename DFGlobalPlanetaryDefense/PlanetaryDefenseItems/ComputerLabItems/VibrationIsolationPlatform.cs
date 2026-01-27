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

    [LocDisplayName("Vibration Isolation Platform"), LocDescription("A sturdy, vibration dampening platform upon which delicate moving parts can be built.")]
    [Serialized, MaxStackSize(10), Weight(3000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class VibrationIsolationPlatformItem : Item { }

    [RequiresSkill(typeof(AdvancedMasonrySkill), 5)]
    public partial class VibrationIsolationPlatformRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(AdvancedMasonrySkill);
        private static Type FocusedTalent => typeof(AdvancedMasonryFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(AdvancedMasonryParallelSpeedTalent);
        private static Type CraftingStation => typeof(AdvancedMasonryTableObject);

        public VibrationIsolationPlatformRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "VibrationIsolationPlatform",
                displayName: Localizer.DoStr("Vibration Isolation Platform"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(GraniteItem), 10, true),
                    new IngredientElement(typeof(PlasticItem), 30, true),
                    new IngredientElement(typeof(SyntheticRubberItem), 30, true),
                    new IngredientElement(typeof(EpoxyItem), 30, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<VibrationIsolationPlatformItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(1500, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 90f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
