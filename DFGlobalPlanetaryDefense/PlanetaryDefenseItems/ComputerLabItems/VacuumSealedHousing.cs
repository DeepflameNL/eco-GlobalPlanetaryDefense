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

    [LocDisplayName("Vacuum Sealed Housing"), LocDescription("A computer case that can be vacuum sealed, to keep unwanted particles from messing up the laser calibration.")]
    [Serialized, MaxStackSize(10), Weight(2000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class VacuumSealedHousingItem : Item { }

    [RequiresSkill(typeof(CompositesSkill), 5)]
    public partial class VacuumSealedHousingRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(CompositesSkill);
        private static Type FocusedTalent => typeof(CompositesFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(CompositesParallelSpeedTalent);
        private static Type CraftingStation => typeof(AdvancedCarpentryTableObject);

        public VacuumSealedHousingRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "VacuumSealedHousing",
                displayName: Localizer.DoStr("Vacuum Sealed Housing"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CompositeLumberItem), 10, true),
                    new IngredientElement(typeof(HighPressurePumpItem), 2, true),
                    new IngredientElement(typeof(CopperWeaveFabricItem), 10, true),
                    new IngredientElement(typeof(SyntheticRubberItem), 10, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<VacuumSealedHousingItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(3000, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 180f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
