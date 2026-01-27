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

    [LocDisplayName("Vacuum Tube Array"), LocDescription("An array of vacuum tubes and several delicate circuits, the heart of a radar.")]
    [Serialized, MaxStackSize(10), Weight(2000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class VacuumTubeArrayItem : Item { }

    [RequiresSkill(typeof(GlassworkingSkill), 7)]
    public partial class VacuumTubeArrayRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(GlassworkingSkill);
        private static Type FocusedTalent => typeof(GlassworkingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(GlassworkingParallelSpeedTalent);
        private static Type CraftingStation => typeof(ElectronicsAssemblyObject);

        public VacuumTubeArrayRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "VacuumTubeArray",
                displayName: Localizer.DoStr("Vacuum Tube Array"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(LightBulbItem), 12, true),
                    new IngredientElement(typeof(AdvancedCircuitItem), 12, true),
                    new IngredientElement(typeof(CopperWiringItem), 10, true),
                    new IngredientElement(typeof(CrushedSulfurItem), 5, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<VacuumTubeArrayItem>(1)
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
