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

    [LocDisplayName("Laminated Lens Housing"), LocDescription("A lens made out of composite layers of fabrics and epoxy, soft yet sturdy, keeps the glass lens from direct contact with the steel frame.")]
    [Serialized, MaxStackSize(10), Weight(1000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class LaminatedLensHousingItem : Item { }

    [RequiresSkill(typeof(TailoringSkill), 7)]
    public partial class LaminatedLensHousingRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(TailoringSkill);
        private static Type FocusedTalent => typeof(TailoringFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(TailoringParallelSpeedTalent);
        private static Type CraftingStation => typeof(AdvancedTailoringTableObject);

        public LaminatedLensHousingRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "LaminatedLensHousing",
                displayName: Localizer.DoStr("Laminated Lens Housing"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(LinenFabricItem), 6, true),
                    new IngredientElement(typeof(EpoxyItem), 6, true),
                    new IngredientElement(typeof(CharcoalPowderItem), 16, true),
                    new IngredientElement(typeof(HighPurityBioVarnishItem), 3, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<LaminatedLensHousingItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(750, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: this.GetType(), start: 45f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

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
