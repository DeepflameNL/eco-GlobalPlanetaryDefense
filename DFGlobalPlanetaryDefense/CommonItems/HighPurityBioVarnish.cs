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

    [LocDisplayName("High-Purity Bio-Varnish"), LocDescription("A very clean varnish made out of biological ingredients.")]
    [Serialized, MaxStackSize(50), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class HighPurityBioVarnishItem : Item { }

    [RequiresSkill(typeof(PaintingSkill), 5)]
    public partial class HighPurityBioVarnishRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(PaintingSkill);
        private static Type ResourceTalent => typeof(PaintingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(PaintingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(PaintingParallelSpeedTalent);
        private static Type CraftingStation => typeof(PaintMixerObject);

        public HighPurityBioVarnishRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HighPurityBioVarnish",
                displayName: Localizer.DoStr("High-Purity Bio-Varnish"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(StarchResinItem), 1, true),
                    new IngredientElement(typeof(EthanolItem), 4, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(GlycerolItem), 5, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<HighPurityBioVarnishItem>(1)
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
