namespace DF.GlobalPlanetaryDefense
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using System;
    using System.Collections.Generic;

    [RequiresSkill(typeof(PaperMillingSkill), 5)]
    public partial class PaperStarchRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(PaperMillingSkill);
        private static Type ResourceTalent => typeof(PaperMillingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(PaperMillingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(PaperMillingParallelSpeedTalent);
        private static Type CraftingStation => typeof(SmallPaperMachineObject);

        public PaperStarchRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "PaperStarch",
                displayName: Localizer.DoStr("Paper (Starch)"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CelluloseFiberItem), 2, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(StarchItem), 2, RequiredSkill, ResourceTalent),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<PaperItem>(4)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(20, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 0.2f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
