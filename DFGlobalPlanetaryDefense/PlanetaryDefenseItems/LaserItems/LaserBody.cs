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

    [LocDisplayName("Laser Body"), LocDescription("The main body that will house all of the components for the laser.")]
    [Serialized, MaxStackSize(10), Weight(5000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class LaserBodyItem : Item { }

    [RequiresSkill(typeof(ElectronicsSkill), 5)]
    public partial class LaserBodyRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(ElectronicsSkill);
        private static Type FocusedTalent => typeof(ElectronicsFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(ElectronicsParallelSpeedTalent);
        private static Type CraftingStation => typeof(RoboticAssemblyLineObject);

        public LaserBodyRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "LaserBody",
                displayName: Localizer.DoStr("Laser Body"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(PowerDistributionBusItem), 1, true),
                    new IngredientElement(typeof(CeramicInsulatorItem), 4, true),
                    new IngredientElement(typeof(ElectricMotorItem), 2, true),
                    new IngredientElement(typeof(SteelBarItem), 50, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<LaserBodyItem>(1)
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
