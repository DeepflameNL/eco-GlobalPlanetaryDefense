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

    [LocDisplayName("Cryo-Insulated Duct"), LocDescription("A special duct with a thermal lining to resist cryogenic temperatures.")]
    [Serialized, MaxStackSize(10), Weight(2000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class CryoInsulatedDuctItem : Item { }

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 6)]
    public partial class CryoInsulatedDuctRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(AdvancedSmeltingSkill);
        private static Type FocusedTalent => typeof(AdvancedSmeltingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(AdvancedSmeltingParallelSpeedTalent);
        private static Type CraftingStation => typeof(ElectricStampingPressObject);

        public CryoInsulatedDuctRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "CryoInsulatedDuct",
                displayName: Localizer.DoStr("Cryo-Insulated Duct"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(VitrifiedCarbonFoamItem), 1, true),
                    new IngredientElement(typeof(SteelPipeItem), 1, true),
                    new IngredientElement(typeof(EpoxyItem), 10, true),
                    new IngredientElement(typeof(LeatherHideItem), 10, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<CryoInsulatedDuctItem>(1)
                }
            );
            Recipes = new List<Recipe> { recipe };

            ExperienceOnCraft = 1;
            LaborInCalories = CreateLaborInCaloriesValue(150, RequiredSkill);
            CraftMinutes = CreateCraftTimeValue(beneficiary: GetType(), start: 9f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

            ModsPreInitialize();
            Initialize(displayText: recipe.DisplayName, recipeType: GetType());
            ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: CraftingStation, recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
