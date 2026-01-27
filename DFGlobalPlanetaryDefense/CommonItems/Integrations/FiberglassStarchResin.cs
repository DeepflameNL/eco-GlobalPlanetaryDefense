namespace DF.GlobalPlanetaryDefense
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using System;
    using System.Collections.Generic;

    [RequiresSkill(typeof(GlassworkingSkill), 2)]
    public partial class FiberglassStarchResinRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(GlassworkingSkill);
        private static Type ResourceTalent => typeof(GlassworkingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(GlassworkingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(GlassworkingParallelSpeedTalent);
        private static Type CraftingStation => typeof(ElectronicsAssemblyObject);

        public FiberglassStarchResinRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "FiberglassStarchResin",
                displayName: Localizer.DoStr("Fiberglass (Starch Resin)"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(StarchResinItem), 1, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(GlycerolItem), 2, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(GlassItem), 2, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<FiberglassItem>(2)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(120, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 3f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
