namespace DF.GlobalPlanetaryDefense
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using System;
    using System.Collections.Generic;

    [RequiresSkill(typeof(CuttingEdgeCookingSkill), 1)]
    public partial class ExpoxyBonesRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(CuttingEdgeCookingSkill);
        private static Type ResourceTalent => typeof(CuttingEdgeCookingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(CuttingEdgeCookingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(CuttingEdgeCookingParallelSpeedTalent);
        private static Type CraftingStation => typeof(LaboratoryObject);

        public ExpoxyBonesRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BioGlue",
                displayName: Localizer.DoStr("Bio-Glue"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BoneMealItem), 3, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(GlycerolItem), 1, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<EpoxyItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1.0f;
            this.LaborInCalories = CreateLaborInCaloriesValue(180, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 1.5f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
