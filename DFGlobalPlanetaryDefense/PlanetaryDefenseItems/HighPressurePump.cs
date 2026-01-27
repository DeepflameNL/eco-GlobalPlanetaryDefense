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

    [LocDisplayName("High-Pressure Pump"), LocDescription("This pump can push liquids at extremely high pressures, perfect for hydraulic systems or dense cooling fins.")]
    [Serialized, MaxStackSize(10), Weight(1000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class HighPressurePumpItem : Item { }

    [RequiresSkill(typeof(MechanicsSkill), 6)]
    public partial class HighPressurePumpRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(MechanicsSkill);
        private static Type FocusedTalent => typeof(MechanicsFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(MechanicsParallelSpeedTalent);
        private static Type CraftingStation => typeof(ElectronicsAssemblyObject);

        public HighPressurePumpRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HighPressurePump",
                displayName: Localizer.DoStr("High-Pressure Pump"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(ElectricMotorItem), 1, true),
                    new IngredientElement(typeof(SteelPipeItem), 5, true),
                    new IngredientElement(typeof(SyntheticRubberItem), 5, true),
                    new IngredientElement(typeof(LubricantItem), 5, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<HighPressurePumpItem>(1)
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
