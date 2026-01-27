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

    [LocDisplayName("Copper Weave Fabric"), LocDescription("Fabric with copper woven into it to act as a faraday cage.")]
    [Serialized, MaxStackSize(10), Weight(1000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class CopperWeaveFabricItem : Item { }

    [RequiresSkill(typeof(TailoringSkill), 7)]
    public partial class CopperWeaveFabricRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(TailoringSkill);
        private static Type FocusedTalent => typeof(TailoringFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(TailoringParallelSpeedTalent);
        private static Type CraftingStation => typeof(AutomaticLoomObject);

        public CopperWeaveFabricRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "CopperWeaveFabric",
                displayName: Localizer.DoStr("Copper Weave Fabric"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("Fabric", 4, true),
                    new IngredientElement(typeof(CopperWiringItem), 6, true),
                    new IngredientElement(typeof(GoldFlakesItem), 2, true),
                    new IngredientElement(typeof(LinenYarnItem), 10, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<CopperWeaveFabricItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(300, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 18f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
