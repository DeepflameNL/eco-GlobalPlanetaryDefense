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

    [LocDisplayName("Starch"), LocDescription("Ground down grains, with the starch extracted.")]
    [Serialized, MaxStackSize(100), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class StarchItem : Item
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Starch");
    }

    [RequiresSkill(typeof(CookingSkill), 5)]
    public partial class StarchRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(CookingSkill);
        private static Type ResourceTalent => typeof(CookingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(CookingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(CookingParallelSpeedTalent);
        private static Type CraftingStation => typeof(KitchenObject);

        public StarchRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Starch",
                displayName: Localizer.DoStr("Starch"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("Grain", 20, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<StarchItem>(5)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(30, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 0.5f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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

    [RequiresSkill(typeof(CookingSkill), 5)]
    public partial class StarchCornRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(CookingSkill);
        private static Type ResourceTalent => typeof(CookingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(CookingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(CookingParallelSpeedTalent);
        private static Type CraftingStation => typeof(KitchenObject);

        public StarchCornRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Starch (Corn)",
                displayName: Localizer.DoStr("Starch (Corn)"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CornItem), 20, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<StarchItem>(5)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(30, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 0.5f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
