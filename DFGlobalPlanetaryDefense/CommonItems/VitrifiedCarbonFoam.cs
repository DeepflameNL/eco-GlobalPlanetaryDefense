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

    [LocDisplayName("Vitrified Carbon Foam"), LocDescription("A very light carbon-based foam, fantastic thermal insulation.")]
    [Serialized, MaxStackSize(50), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class VitrifiedCarbonFoamItem : Item { }

    [RequiresSkill(typeof(AdvancedBakingSkill), 7)]
    public partial class VitrifiedCarbonFoamRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(AdvancedBakingSkill);
        private static Type ResourceTalent => typeof(AdvancedBakingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(AdvancedBakingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(AdvancedBakingParallelSpeedTalent);
        private static Type CraftingStation => typeof(BakeryOvenObject);

        public VitrifiedCarbonFoamRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "VitrifiedCarbonFoam",
                displayName: Localizer.DoStr("Vitrified Carbon Foam"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(FlourItem), 3, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(YeastItem), 2, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(CharcoalPowderItem), 4, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<VitrifiedCarbonFoamItem>(1)
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
