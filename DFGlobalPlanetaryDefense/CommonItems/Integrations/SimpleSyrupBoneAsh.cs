namespace DF.GlobalPlanetaryDefense
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using System;
    using System.Collections.Generic;

    [RequiresSkill(typeof(MillingSkill), 2)]
    public partial class SimpleSyrupBoneAshRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(MillingSkill);
        private static Type ResourceTalent => typeof(MillingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(MillingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(MillingParallelSpeedTalent);
        private static Type CraftingStation => typeof(MillObject);

        public SimpleSyrupBoneAshRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "SimpleSyrupBoneAsh",
                displayName: Localizer.DoStr("Simple Syrup (Bone Ash)"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(SugarItem), 17, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(BoneAshItem), 1, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<SimpleSyrupItem>(2)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(15, RequiredSkill);
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
