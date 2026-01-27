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
    using System.ComponentModel;

    [Serialized, Weight(500)]
    [Ecopedia("Items", "Fertilizers", createAsSubPage: true)]
    [LocDisplayName("Bone Meal Filler"), LocDescription("Bone Meal and dirt makes a great filler for other fertilizers, rich in phosphor.")]
    [Category("Tool")]
    [Tag("Fertilizer")]
    [Tag("FertilizerFiller")]
    public partial class BoneMealFillerItem : FertilizerItem
    {
        public override FertilizerNutrients Nutrients => new FertilizerNutrients(0.2f, 0.7f, 0.1f);
    }

    [RequiresSkill(typeof(FertilizersSkill), 1)]
    [Ecopedia("Items", "Fertilizers", subPageName: "Bone Meal Filler Item")]
    public partial class BoneMealFillerRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(FertilizersSkill);
        private static Type ResourceTalent => typeof(FertilizersLavishResourcesTalent);
        private static Type FocusedTalent => typeof(FertilizersFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(FertilizersParallelSpeedTalent);
        private static Type CraftingStation => typeof(FarmersTableObject);

        public BoneMealFillerRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BoneMealFiller",
                displayName: Localizer.DoStr("Bone Meal Filler"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BoneMealItem), 15, RequiredSkill, ResourceTalent),
                    new IngredientElement(typeof(DirtItem), 1, RequiredSkill, ResourceTalent),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<BoneMealFillerItem>()
                }
            );
            this.Recipes = new List<Recipe> { recipe };

            this.LaborInCalories = CreateLaborInCaloriesValue(15, RequiredSkill);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(PulpFillerRecipe), start: 0.3f, skillType: RequiredSkill, FocusedTalent, ParallelTalent);

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Bone Meal Filler"), recipeType: this.GetType());
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: CraftingStation, recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
