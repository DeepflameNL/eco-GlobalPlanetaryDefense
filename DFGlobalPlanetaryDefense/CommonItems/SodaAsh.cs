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

    [LocDisplayName("Soda Ash"), LocDescription("Ash made of sodium rich minerals, useful as a source of sodium in chemical processes, or as a flux to reduce temperature in a chemical reaction.")]
    [Tag("Alkaline"), Tag("Flux")]
    [Serialized, MaxStackSize(50), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class SodaAshItem : Item { }

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 5)]
    public partial class SodaAshRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(AdvancedSmeltingSkill);
        private static Type ResourceTalent => typeof(AdvancedSmeltingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(AdvancedSmeltingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(AdvancedSmeltingParallelSpeedTalent);
        private static Type CraftingStation => typeof(BlastFurnaceObject);

        public SodaAshRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "SodaAsh",
                displayName: Localizer.DoStr("Soda Ash"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CrushedBasaltItem), 2, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(CrushedLimestoneItem), 2, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(CharcoalPowderItem), 3, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<SodaAshItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(50, RequiredSkill);
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
