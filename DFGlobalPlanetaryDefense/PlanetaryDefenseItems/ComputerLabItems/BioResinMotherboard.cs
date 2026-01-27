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

    [LocDisplayName("Bio-Resin Motherboard"), LocDescription("A motherboard made out of bio-resin, extremely temperature stable, non-conductive material that is easy to work with.")]
    [Serialized, MaxStackSize(50), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class BioResinMotherboardItem : Item { }

    [RequiresSkill(typeof(AdvancedBakingSkill), 5)]
    public partial class BioResinMotherboardRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(AdvancedBakingSkill);
        private static Type FocusedTalent => typeof(AdvancedBakingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(AdvancedBakingParallelSpeedTalent);
        private static Type CraftingStation => typeof(BakeryOvenObject);

        public BioResinMotherboardRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BioResinMotherboard",
                displayName: Localizer.DoStr("Bio-Resin Motherboard"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CompositeLumberItem), 10, true),
                    new IngredientElement(typeof(StarchResinItem), 10, true),
                    new IngredientElement(typeof(CopperWiringItem), 10, true),
                    new IngredientElement(typeof(SodaAshItem), 10, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<BioResinMotherboardItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(1000, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 60f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
