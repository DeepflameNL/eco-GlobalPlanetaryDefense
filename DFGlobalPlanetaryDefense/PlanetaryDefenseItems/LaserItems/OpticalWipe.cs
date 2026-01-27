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

    [LocDisplayName("Optical Wipe"), LocDescription("A delicate piece of paper and fabric to gently clean the most delicate lenses.")]
    [Serialized, MaxStackSize(10), Weight(100)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class OpticalWipeItem : Item { }

    [RequiresSkill(typeof(PaperMillingSkill), 5)]
    public partial class OpticalWipeRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(PaperMillingSkill);
        private static Type ResourceTalent => typeof(PaperMillingLavishResourcesTalent);
        private static Type FocusedTalent => typeof(PaperMillingFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(PaperMillingParallelSpeedTalent);
        private static Type CraftingStation => typeof(SmallPaperMachineObject);

        public OpticalWipeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "OpticalWipe",
                displayName: Localizer.DoStr("Optical Wipe"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("Fabric", 1, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(PaperItem), 2, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(EthanolItem), 1, RequiredSkill, ResourceTalent)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<OpticalWipeItem>(1)
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
