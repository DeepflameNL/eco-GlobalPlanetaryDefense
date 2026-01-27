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

    [LocDisplayName("High-Frequency Data Bus"), LocDescription("The nerve center of any high tech system, capable of moving large amounts of data in a very short time.")]
    [Serialized, MaxStackSize(10), Weight(2000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class HighFrequencyDataBusItem : Item { }

    [RequiresSkill(typeof(ElectronicsSkill), 5)]
    public partial class HighFrequencyDataBusRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(ElectronicsSkill);
        private static Type FocusedTalent => typeof(ElectronicsFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(ElectronicsParallelSpeedTalent);
        private static Type CraftingStation => typeof(ElectronicsAssemblyObject);

        public HighFrequencyDataBusRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HighFrequencyDataBus",
                displayName: Localizer.DoStr("High-Frequency Data Bus"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(GoldWiringItem), 10, true),
                    new IngredientElement(typeof(CopperWeaveFabricItem), 12, true),
                    new IngredientElement(typeof(GoldBarItem), 6, true),
                    new IngredientElement(typeof(CopperBarItem), 10, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<HighFrequencyDataBusItem>(1)
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
