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

    [LocDisplayName("Power Distribution Bus"), LocDescription("The power distribution bus gets electricity to go exactly where you need it to go.")]
    [Serialized, MaxStackSize(10), Weight(3000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class PowerDistributionBusItem : Item { }

    [RequiresSkill(typeof(IndustrySkill), 5)]
    public partial class PowerDistributionBusRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(IndustrySkill);
        private static Type FocusedTalent => typeof(IndustryFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(IndustryParallelSpeedTalent);
        private static Type CraftingStation => typeof(ElectronicsAssemblyObject);

        public PowerDistributionBusRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "PowerDistributionBus",
                displayName: Localizer.DoStr("Power Distribution Bus"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(SteelBarItem), 15, true),
                    new IngredientElement(typeof(CopperWiringItem), 50, true),
                    new IngredientElement(typeof(PlasticItem), 50, true),
                    new IngredientElement(typeof(RivetItem), 50, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<PowerDistributionBusItem>(1)
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
