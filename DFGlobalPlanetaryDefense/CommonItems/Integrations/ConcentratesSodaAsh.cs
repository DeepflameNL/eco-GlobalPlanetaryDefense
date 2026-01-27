namespace DF.GlobalPlanetaryDefense
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using System;
    using System.Collections.Generic;

    [RequiresSkill(typeof(MiningSkill), 5)]
    public partial class ConcentrateIronSodaAshRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(MiningSkill);
        private static Type CraftingStation => typeof(FrothFloatationCellObject);

        public ConcentrateIronSodaAshRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ConcentrateIronSodaAsh",
                displayName: Localizer.DoStr("Concentrate Iron (Soda Ash)"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CrushedIronOreItem), 10,typeof(MiningSkill)),
                    new IngredientElement(typeof(SodaAshItem), 2, RequiredSkill),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<IronConcentrateItem>(4), // Byproducts should not be affected by Lavish Workspace talent
                    new CraftingElement<WetTailingsItem>(typeof(MiningSkill), 1), // Byproducts should not be affected by Lavish Workspace talent
                    new CraftingElement<CrushedMixedRockItem>(typeof(MiningSkill), 1), // Byproducts should not be affected by Lavish Workspace talent
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(270, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 1.2f, skillType: RequiredSkill);

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

    [RequiresSkill(typeof(MiningSkill), 5)]
    public partial class ConcentrateCopperSodaAshRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(MiningSkill);
        private static Type CraftingStation => typeof(FrothFloatationCellObject);

        public ConcentrateCopperSodaAshRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ConcentrateCopperSodaAsh",
                displayName: Localizer.DoStr("Concentrate Copper (Soda Ash)"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CrushedCopperOreItem), 7,typeof(MiningSkill)),
                    new IngredientElement(typeof(SodaAshItem), 2, RequiredSkill)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<CopperConcentrateItem>(2), // Byproducts should not be affected by Lavish Workspace talent
                    new CraftingElement<WetTailingsItem>(typeof(MiningSkill), 1), // Byproducts should not be affected by Lavish Workspace talent
                    new CraftingElement<CrushedMixedRockItem>(typeof(MiningSkill), 1), // Byproducts should not be affected by Lavish Workspace talent
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(180, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 0.8f, skillType: RequiredSkill);

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

    [RequiresSkill(typeof(MiningSkill), 5)]
    public partial class ConcentrateGoldSodaAshRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(MiningSkill);
        private static Type CraftingStation => typeof(FrothFloatationCellObject);

        public ConcentrateGoldSodaAshRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ConcentrateGoldSodaAsh",
                displayName: Localizer.DoStr("Concentrate Gold (Soda Ash)"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CrushedGoldOreItem), 10,typeof(MiningSkill)),
                    new IngredientElement(typeof(SodaAshItem), 2, RequiredSkill),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<GoldConcentrateItem>(2), // Byproducts should not be affected by Lavish Workspace talent
                    new CraftingElement<WetTailingsItem>(typeof(MiningSkill), 2), // Byproducts should not be affected by Lavish Workspace talent
                    new CraftingElement<CrushedMixedRockItem>(typeof(MiningSkill), 1), // Byproducts should not be affected by Lavish Workspace talent
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(180, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 0.8f, skillType: RequiredSkill);

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
