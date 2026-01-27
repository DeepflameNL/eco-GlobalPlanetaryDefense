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

    [LocDisplayName("Ceramic Insulator"), LocDescription("A ceramic insulator keeps electricity from going places you don't want it to go.")]
    [Serialized, MaxStackSize(10), Weight(1000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class CeramicInsulatorItem : Item { }

    [RequiresSkill(typeof(AdvancedMasonrySkill), 5)]
    public partial class CeramicInsulatorRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(AdvancedMasonrySkill);
        private static Type FocusedTalent => typeof(AdvancedMasonryFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(AdvancedMasonryParallelSpeedTalent);
        private static Type CraftingStation => typeof(CementKilnObject);

        public CeramicInsulatorRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "CeramicInsulator",
                displayName: Localizer.DoStr("Ceramic Insulator"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CrushedSlagItem), 20, true),
                    new IngredientElement(typeof(CrushedSulfurItem), 20, true),
                    new IngredientElement(typeof(DiamondInfusedPolishingPasteItem), 4, true),
                    new IngredientElement(typeof(ClayItem), 20, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<CeramicInsulatorItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(460, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 27.5f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
