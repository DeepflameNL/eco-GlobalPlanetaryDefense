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

    [LocDisplayName("Tempered Phosphate Glass"), LocDescription("Extremely temperature resistant glass, ideal for the optics of high powered lasers.")]
    [Serialized, MaxStackSize(10), Weight(1000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class TemperedPhospateGlassItem : Item { }

    [RequiresSkill(typeof(GlassworkingSkill), 6)]
    public partial class TemperedPhospateGlassRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(GlassworkingSkill);
        private static Type ResourceTalent => typeof(GlassworkingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(GlassworkingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(GlassworkingParallelSpeedTalent);
        private static Type CraftingStation => typeof(GlassworksObject);

        public TemperedPhospateGlassRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "TemperedPhosphateGlass",
                displayName: Localizer.DoStr("Tempered Phosphate Glass"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(SodaAshItem), 3, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(BoneAshItem), 3, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(QuicklimeItem), 2, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(SandItem), 3, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<TemperedPhospateGlassItem>(1)
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
