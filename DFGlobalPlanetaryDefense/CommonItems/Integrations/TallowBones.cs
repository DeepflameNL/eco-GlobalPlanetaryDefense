namespace DF.GlobalPlanetaryDefense
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using System;
    using System.Collections.Generic;

    [RequiresSkill(typeof(CampfireCookingSkill), 1)]
    public partial class TallowBonesRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(CampfireCookingSkill);
        private static Type ResourceTalent => typeof(CampfireCookingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(CampfireCookingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(CampfireCookingParallelSpeedTalent);
        private static Type CraftingStation => typeof(KilnObject);

        public TallowBonesRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Extract Marrow",
                displayName: Localizer.DoStr("Extract Marrow"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BoneItem), 10, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<TallowItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 0.5f;
            this.LaborInCalories = CreateLaborInCaloriesValue(20, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 0.8f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
