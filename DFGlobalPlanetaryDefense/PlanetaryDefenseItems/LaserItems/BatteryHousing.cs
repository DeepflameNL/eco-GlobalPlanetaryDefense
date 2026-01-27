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

    [LocDisplayName("Battery Housing"), LocDescription("The housing for a high-density battery, made out of sturdy, temperature resistant composite materials.")]
    [Serialized, MaxStackSize(10), Weight(1000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class BatteryHousingItem : Item { }

    [RequiresSkill(typeof(CompositesSkill), 5)]
    public partial class BatteryHousingRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(CompositesSkill);
        private static Type FocusedTalent => typeof(CompositesFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(CompositesParallelSpeedTalent);
        private static Type CraftingStation => typeof(AdvancedCarpentryTableObject);

        public BatteryHousingRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BatteryHousing",
                displayName: Localizer.DoStr("Battery Housing"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CompositeLumberItem), 4, true),
                    new IngredientElement(typeof(EpoxyItem), 2, true),
                    new IngredientElement(typeof(CopperWiringItem), 4, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<BatteryHousingItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(300, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 18f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
