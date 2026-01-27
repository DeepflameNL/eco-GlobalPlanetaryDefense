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

    [LocDisplayName("Synthetic Diamond"), LocDescription("Just as hard and shiny as the real thing, just cheaper to make!")]
    [Serialized, MaxStackSize(50), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class SyntheticDiamondItem : Item { }

    [RequiresSkill(typeof(MiningSkill), 7)]
    public partial class SyntheticDiamondRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(MiningSkill);
        private static Type CraftingStation => typeof(ElectricStampingPressObject);

        public SyntheticDiamondRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "SyntheticDiamond",
                displayName: Localizer.DoStr("Synthetic Diamond"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CrushedCoalItem), 15, RequiredSkill)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<SyntheticDiamondItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(250, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 1.2f, skillType: RequiredSkill);

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
