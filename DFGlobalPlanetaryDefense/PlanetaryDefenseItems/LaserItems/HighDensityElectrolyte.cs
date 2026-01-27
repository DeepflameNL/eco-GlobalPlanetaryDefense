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

    [LocDisplayName("High-Density Electrolyte"), LocDescription("A very potent electrolyte, used for batteries that need to store incredible amounts of energy.")]
    [Serialized, MaxStackSize(50), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class HighDensityElectrolyteItem : Item { }

    [RequiresSkill(typeof(OilDrillingSkill), 5)]
    public partial class HighDensityElectrolyteRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(OilDrillingSkill);
        private static Type FocusedTalent => typeof(OilDrillingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(OilDrillingParallelSpeedTalent);
        private static Type CraftingStation => typeof(OilRefineryObject);

        public HighDensityElectrolyteRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HighDensityElectrolyte",
                displayName: Localizer.DoStr("High-Density Electrolyte"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(NitricAcidItem), 8, true),
                    new IngredientElement(typeof(CrushedSulfurItem), 8, true),
                    new IngredientElement(typeof(GoldBarItem), 8, true),
                    new IngredientElement(typeof(GlassContainerItem), 1, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<HighDensityElectrolyteItem>(1)
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
