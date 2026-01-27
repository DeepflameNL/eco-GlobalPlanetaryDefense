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

    [LocDisplayName("Coolant Circulation System"), LocDescription("The system responsible for providing coolant to the parts that really need cooling.")]
    [Serialized, MaxStackSize(10), Weight(4000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    public partial class CoolantCirculationSystemItem : Item { }

    [RequiresSkill(typeof(IndustrySkill), 5)]
    public partial class CoolantCirculationSystemRecipe : RecipeFamily
    {
        // Easy to find constants
        private static Type RequiredSkill => typeof(IndustrySkill);
        private static Type FocusedTalent => typeof(IndustryFocusedSpeedTalent);
        private static Type ParallelTalent => typeof(IndustryParallelSpeedTalent);
        private static Type CraftingStation => typeof(ElectricMachinistTableObject);

        public CoolantCirculationSystemRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "CoolantCirculationSystem",
                displayName: Localizer.DoStr("Coolant Circulation System"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CryoInsulatedDuctItem), 20, true),
                    new IngredientElement(typeof(HighPressurePumpItem), 2, true),
                    new IngredientElement(typeof(ThermalInterfacePasteItem), 10, true),
                    new IngredientElement(typeof(RadiatorItem), 4, true)
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<CoolantCirculationSystemItem>(1)
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
