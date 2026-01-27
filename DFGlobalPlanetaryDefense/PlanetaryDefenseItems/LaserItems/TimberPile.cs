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

    [LocDisplayName("Timber Pile"), LocDescription("A timber pile, driven into the soil, to provide support and stability to heavy constructions.")]
    [Serialized, MaxStackSize(10), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class TimberPileItem : Item { }

    [RequiresSkill(typeof(LoggingSkill), 7)]
    public partial class TimberPileRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(LoggingSkill);
        private static Type CraftingStation => typeof(AdvancedCarpentryTableObject);

        public TimberPileRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "TimerPile",
                displayName: Localizer.DoStr("Timber Pile"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("Wood", 10, true),
                    new IngredientElement(typeof(IronPlateItem), 4, true),
                    new IngredientElement(typeof(NailItem), 40, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<TimberPileItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(250, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 15f, skillType: RequiredSkill);

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
