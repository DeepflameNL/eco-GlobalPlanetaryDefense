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

    [LocDisplayName("Bone Ash"), LocDescription("Ash made of bones, useful as a source of calcium and phosphor in chemical processes.")]
    [Tag("Flux")]
    [Serialized, MaxStackSize(100), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class BoneAshItem : Item
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Bone Ash");
    }

    [RequiresSkill(typeof(BakingSkill), 5)]
    public partial class BoneAshRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(BakingSkill);
        private static Type ResourceTalent => typeof(BakingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(BakingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(BakingParallelSpeedTalent);
        private static Type CraftingStation => typeof(BakeryOvenObject);

        public BoneAshRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BoneAsh",
                displayName: Localizer.DoStr("Bone Ash"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BoneMealItem), 3, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(CharcoalPowderItem), 3, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<BoneAshItem>(1)
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(15, RequiredSkill);
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
