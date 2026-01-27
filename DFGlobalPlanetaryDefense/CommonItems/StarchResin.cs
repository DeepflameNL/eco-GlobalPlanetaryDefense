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

    [LocDisplayName("Starch Resin"), LocDescription("A resin made out of starch, useful to build temperature stable substrates.")]
    [Serialized, MaxStackSize(50), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class StarchResinItem : Item { }

    [RequiresSkill(typeof(AdvancedBakingSkill), 5)]
    public partial class StarchResinRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(AdvancedBakingSkill);
        private static Type ResourceTalent => typeof(AdvancedBakingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(AdvancedBakingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(AdvancedBakingParallelSpeedTalent);
        private static Type CraftingStation => typeof(LaboratoryObject);

        public StarchResinRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "StarchResin",
                displayName: Localizer.DoStr("Starch Resin"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(GlassContainerItem), 1, true),
                    new IngredientElement(typeof(StarchItem), 20, RequiredSkill, ResourceTalent),
                    new IngredientElement("Oil", 10, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(EthanolItem), 2, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<StarchResinItem>(1)
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
