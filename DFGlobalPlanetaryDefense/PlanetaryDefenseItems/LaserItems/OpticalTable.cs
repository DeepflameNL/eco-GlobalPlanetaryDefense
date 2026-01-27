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

    [LocDisplayName("Optical Table"), LocDescription("The Optical Table is a frame on which lenses can be positioned and precisely aligned.")]
    [Serialized, MaxStackSize(10), Weight(1000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class OpticalTableItem : Item { }

    [RequiresSkill(typeof(MechanicsSkill), 6)]
    public partial class OpticalTableRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(MechanicsSkill);
        private static Type FocusedTalent => typeof(MechanicsFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(MechanicsParallelSpeedTalent);
        private static Type CraftingStation => typeof(ElectricMachinistTableObject);

        public OpticalTableRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "OpticalTable",
                displayName: Localizer.DoStr("Optical Table"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(SteelAxleItem), 10, true),
                    new IngredientElement(typeof(RivetItem), 40, true),
                    new IngredientElement(typeof(CopperWiringItem), 40, true),
                    new IngredientElement(typeof(LubricantItem), 20, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OpticalTableItem>(1)
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
