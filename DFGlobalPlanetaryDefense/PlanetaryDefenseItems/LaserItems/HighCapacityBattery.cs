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

    [LocDisplayName("High-Capacity Battery"), LocDescription("A battery capable of storing vast quantities of energy.")]
    [Serialized, MaxStackSize(10), Weight(1000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class HighCapacityBatteryItem : Item { }

    [RequiresSkill(typeof(OilDrillingSkill), 5)]
    public partial class HighCapacityBatteryRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(OilDrillingSkill);
        private static Type FocusedTalent => typeof(OilDrillingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(OilDrillingParallelSpeedTalent);
        private static Type CraftingStation => typeof(ElectronicsAssemblyObject);

        public HighCapacityBatteryRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HighCapacityBattery",
                displayName: Localizer.DoStr("High-Capacity Battery"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BatteryHousingItem), 1, true),
                    new IngredientElement(typeof(HighDensityElectrolyteItem), 1, true),
                    new IngredientElement(typeof(GlassContainerItem), 20, true),
                    new IngredientElement(typeof(SteelBarItem), 20, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<HighCapacityBatteryItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(600, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 36f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
