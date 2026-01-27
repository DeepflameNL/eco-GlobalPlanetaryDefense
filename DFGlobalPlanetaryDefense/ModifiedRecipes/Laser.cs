using DF.GlobalPlanetaryDefense;
using Eco.Gameplay.Items.Recipes;

namespace Eco.Mods.TechTree
{
    public partial class LaserRecipe : RecipeFamily
    {
        partial void ModsPreInitialize()
        {
            this.DefaultRecipe.Ingredients.Clear();
            this.DefaultRecipe.Ingredients.Add(new IngredientElement(typeof(SeismicStabilizationPlatformItem), 1, true));
            this.DefaultRecipe.Ingredients.Add(new IngredientElement(typeof(LaserBodyItem), 1, true));
            this.DefaultRecipe.Ingredients.Add(new IngredientElement(typeof(OpticalAssemblyItem), 1, true));
            this.DefaultRecipe.Ingredients.Add(new IngredientElement(typeof(EMShieldingJacketItem), 1, true));
            this.DefaultRecipe.Ingredients.Add(new IngredientElement(typeof(CoolantCirculationSystemItem), 1, true));
            this.DefaultRecipe.Ingredients.Add(new IngredientElement(typeof(HighCapacityBatteryItem), 10, true));
        }
    }
}
