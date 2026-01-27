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

    [LocDisplayName("Radar Transceiver"), LocDescription("The eyes and ears of the computer lab, specially designed to track down large rocks hurling through space.")]
    [Serialized, MaxStackSize(10), Weight(2000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class RadarTransceiverItem : Item { }

    [RequiresSkill(typeof(ElectronicsSkill), 5)]
    public partial class RadarTransceiverRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(ElectronicsSkill);
        private static Type FocusedTalent => typeof(ElectronicsFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(ElectronicsParallelSpeedTalent);
        private static Type CraftingStation => typeof(ElectronicsAssemblyObject);

        public RadarTransceiverRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "RadarTransceiver",
                displayName: Localizer.DoStr("Radar Transceiver"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HighFrequencyDataBusItem), 2, true),
                    new IngredientElement(typeof(VacuumTubeArrayItem), 2, true),
                    new IngredientElement(typeof(CompositeLumberItem), 20, true),
                    new IngredientElement(typeof(EpoxyItem), 20, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<RadarTransceiverItem>(1)
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
