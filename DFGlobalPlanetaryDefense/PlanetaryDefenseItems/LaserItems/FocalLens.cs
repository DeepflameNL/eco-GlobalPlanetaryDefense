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

    [LocDisplayName("Focal Lens"), LocDescription("Highly polished, finely tuned focal lens, used to create a highly focused beam of light all the way to pesky meteorites.")]
    [Serialized, MaxStackSize(10), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class FocalLensItem : Item { }

    [RequiresSkill(typeof(GlassworkingSkill), 7)]
    public partial class FocalLensRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(GlassworkingSkill);
        private static Type FocusedTalent => typeof(GlassworkingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(GlassworkingParallelSpeedTalent);
        private static Type CraftingStation => typeof(GlassworksObject);

        public FocalLensRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "FocalLens",
                displayName: Localizer.DoStr("Focal Lens"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(TemperedPhospateGlassItem), 2, true),
                    new IngredientElement(typeof(DiamondInfusedPolishingPasteItem), 2, true),
                    new IngredientElement(typeof(OpticalWipeItem), 6, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<FocalLensItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(750, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 45f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
