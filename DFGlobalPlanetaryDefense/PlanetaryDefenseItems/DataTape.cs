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

    [LocDisplayName("Data Tape"), LocDescription("A reel of magnetic tape, used for storing large amounts of data.")]
    [Serialized, MaxStackSize(50), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class DataTapeItem : Item { }

    [RequiresSkill(typeof(OilDrillingSkill), 5)]
    public partial class DataTapeRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(OilDrillingSkill);
        private static Type FocusedTalent => typeof(OilDrillingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(OilDrillingParallelSpeedTalent);
        private static Type CraftingStation => typeof(SpinMelterObject);

        public DataTapeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "DataTape",
                displayName: Localizer.DoStr("Data Tape"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(IronConcentrateItem), 2, true),
                    new IngredientElement(typeof(PlasticItem), 2, true),
                    new IngredientElement(typeof(EpoxyItem), 2, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<DataTapeItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(250, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 1.2f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
