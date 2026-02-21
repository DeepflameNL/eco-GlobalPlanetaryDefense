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

    [LocDisplayName("Glycerol"), LocDescription("A syrupy mass that can be used in several chemical reactions.")]
    [Tag("Plasticizer")]
    [Serialized, MaxStackSize(50), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class GlycerolItem : Item
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Glycerol");
    }

    [RequiresSkill(typeof(CuttingEdgeCookingSkill), 1)]
    public partial class GlycerolTallowRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(CuttingEdgeCookingSkill);
        private static Type ResourceTalent => typeof(CuttingEdgeCookingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(CuttingEdgeCookingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(CuttingEdgeCookingParallelSpeedTalent);
        private static Type CraftingStation => typeof(LaboratoryObject);

        public GlycerolTallowRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "GlycerolTallow",
                displayName: Localizer.DoStr("Glycerol (Tallow)"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(TallowItem), 5, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<GlycerolItem>(1)
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

    [RequiresSkill(typeof(CuttingEdgeCookingSkill), 1)]
    public partial class GlycerolBeansRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(CuttingEdgeCookingSkill);
        private static Type ResourceTalent => typeof(CuttingEdgeCookingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(CuttingEdgeCookingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(CuttingEdgeCookingParallelSpeedTalent);
        private static Type CraftingStation => typeof(LaboratoryObject);

        public GlycerolBeansRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "GlycerolBeans",
                displayName: Localizer.DoStr("Glycerol (Beans)"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BeansItem), 20, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<GlycerolItem>(1)
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

    [RequiresSkill(typeof(CuttingEdgeCookingSkill), 1)]
    public partial class GlycerolPalmRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(CuttingEdgeCookingSkill);
        private static Type ResourceTalent => typeof(CuttingEdgeCookingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(CuttingEdgeCookingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(CuttingEdgeCookingParallelSpeedTalent);
        private static Type CraftingStation => typeof(LaboratoryObject);

        public GlycerolPalmRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "GlycerolBeans",
                displayName: Localizer.DoStr("Glycerol (Palm)"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HeartOfPalmItem), 10, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<GlycerolItem>(1)
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
