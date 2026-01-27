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

    [LocDisplayName("Diamond Infused Polishing Paste"), LocDescription("Excellent polishing paste for creating the highest quality optics.")]
    [Serialized, MaxStackSize(10), Weight(1000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class DiamondInfusedPolishingPasteItem : Item { }

    [RequiresSkill(typeof(OilDrillingSkill), 5)]
    public partial class DiamondInfusedPolishingPasteRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(OilDrillingSkill);
        private static Type ResourceTalent => typeof(OilDrillingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(OilDrillingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(OilDrillingParallelSpeedTalent);
        private static Type CraftingStation => typeof(OilRefineryObject);

        public DiamondInfusedPolishingPasteRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "DiamondInfusedPolishingPaste",
                displayName: Localizer.DoStr("Diamond Infused Polishing Paste"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(DiamondPowderItem), 13, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(PolishingPasteItem), 1, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<DiamondInfusedPolishingPasteItem>(1)
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
