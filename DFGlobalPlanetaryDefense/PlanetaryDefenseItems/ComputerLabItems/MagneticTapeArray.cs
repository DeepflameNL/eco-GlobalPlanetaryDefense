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

    [LocDisplayName("Magnetic Tape Array"), LocDescription("This device provides the Computer Lab with its data storage options.")]
    [Serialized, MaxStackSize(10), Weight(2000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class MagneticTapeArrayItem : Item { }

    [RequiresSkill(typeof(MechanicsSkill), 6)]
    public partial class MagneticTapeArrayRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(MechanicsSkill);
        private static Type FocusedTalent => typeof(MechanicsFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(MechanicsParallelSpeedTalent);
        private static Type CraftingStation => typeof(ElectronicsAssemblyObject);

        public MagneticTapeArrayRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "MagneticTapeArray",
                displayName: Localizer.DoStr("Magnetic Tape Array"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(VibrationIsolationPlatformItem), 2, true),
                    new IngredientElement(typeof(TapeDriveMechanismItem), 2, true),
                    new IngredientElement(typeof(ServoItem), 15, true),
                    new IngredientElement(typeof(DataTapeItem), 6, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<MagneticTapeArrayItem>(1)
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
