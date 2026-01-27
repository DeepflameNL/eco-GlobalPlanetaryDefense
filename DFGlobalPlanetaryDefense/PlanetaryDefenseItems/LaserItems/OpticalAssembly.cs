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

    [LocDisplayName("Optical Assembly"), LocDescription("The fully assembled optical bench, ready to be aimed at a meteorite and turned on.")]
    [Serialized, MaxStackSize(10), Weight(2000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class OpticalAssemblyItem : Item { }

    [RequiresSkill(typeof(GlassworkingSkill), 7)]
    public partial class OpticalAssemblyRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(GlassworkingSkill);
        private static Type FocusedTalent => typeof(GlassworkingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(GlassworkingParallelSpeedTalent);
        private static Type CraftingStation => typeof(ElectricMachinistTableObject);

        public OpticalAssemblyRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "OpticalAssembly",
                displayName: Localizer.DoStr("Optical Assembly"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(FocalLensItem), 4, true),
                    new IngredientElement(typeof(OpticalTableItem), 1, true),
                    new IngredientElement(typeof(LaminatedLensHousingItem), 4, true),
                    new IngredientElement(typeof(ServoItem), 4, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OpticalAssemblyItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(6000, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 360f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
