namespace DF.GlobalPlanetaryDefense
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using System;
    using System.Collections.Generic;

    [RequiresSkill(typeof(MasonrySkill), 4)]
    public partial class CementBoneAshRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(MasonrySkill);
        private static Type ResourceTalent => typeof(MasonryLavishResourcesTalent);
        private static Type FocusedTalent => typeof(MasonryFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(MasonryParallelSpeedTalent);
        private static Type CraftingStation => typeof(CementKilnObject);

        public CementBoneAshRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "CementBoneAsh",
                displayName: Localizer.DoStr("Cement (Bone Ash)"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BoneAshItem), 1, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(SandItem), 1, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(CharcoalPowderItem), 4, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<CementItem>(2)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(70, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 1.6f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
