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

    [LocDisplayName("EM Shielding Jacket"), LocDescription("A special jacket built around the laser to protect it and everything around it from electromagnetic interference.")]
    [Serialized, MaxStackSize(10), Weight(1000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class EMShieldingJacketItem : Item { }

    [RequiresSkill(typeof(TailoringSkill), 7)]
    public partial class EMShieldingJacketRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(TailoringSkill);
        private static Type FocusedTalent => typeof(TailoringFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(TailoringParallelSpeedTalent);
        private static Type CraftingStation => typeof(AdvancedTailoringTableObject);

        public EMShieldingJacketRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "EMShieldingJacket",
                displayName: Localizer.DoStr("EM Shielding Jacket"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CopperWeaveFabricItem), 10, true),
                    new IngredientElement(typeof(HydraulicTensioningLatticeItem), 3, true),
                    new IngredientElement(typeof(PlasticItem), 100, true),
                    new IngredientElement(typeof(LeatherHideItem), 120, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<EMShieldingJacketItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(6000, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 360f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
