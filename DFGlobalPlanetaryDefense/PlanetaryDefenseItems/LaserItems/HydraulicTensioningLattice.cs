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

    [LocDisplayName("Hydraulic Tensioning Lattice"), LocDescription("A lattice that ensures the EM Shielding Jacket always tightly fits around the laser's optics, even as heat causes the metal to expand and contract.")]
    [Serialized, MaxStackSize(10), Weight(1000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class HydraulicTensioningLatticeItem : Item { }

    [RequiresSkill(typeof(MechanicsSkill), 6)]
    public partial class HydraulicTensioningLatticeRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(MechanicsSkill);
        private static Type FocusedTalent => typeof(MechanicsFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(MechanicsParallelSpeedTalent);
        private static Type CraftingStation => typeof(ElectricMachinistTableObject);

        public HydraulicTensioningLatticeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HydraulicTensioningLattice",
                displayName: Localizer.DoStr("Hydraulic Tensioning Lattice"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(PistonItem), 30, true),
                    new IngredientElement(typeof(SteelPipeItem), 30, true),
                    new IngredientElement(typeof(SyntheticRubberItem), 30, true),
                    new IngredientElement(typeof(LubricantItem), 30, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<HydraulicTensioningLatticeItem>(1)
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
