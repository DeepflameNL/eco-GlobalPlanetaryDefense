using DF.GlobalPlanetaryDefense;
using Eco.Gameplay.Items.Recipes;

namespace Eco.Mods.TechTree
{
    public partial class LaserRecipe : RecipeFamily
    {
        partial void ModsPreInitialize()
        {
            this.Recipe[0].Ingredients.Clear();
            this.Recipe[0].Ingredients.Add(new IngredientElement(typeof(SeismicStabilizationPlatformItem), 1, true));
            this.Recipe[0].Ingredients.Add(new IngredientElement(typeof(LaserBodyItem), 1, true));
            this.Recipe[0].Ingredients.Add(new IngredientElement(typeof(OpticalAssemblyItem), 1, true));
            this.Recipe[0].Ingredients.Add(new IngredientElement(typeof(EMShieldingJacketItem), 1, true));
            this.Recipe[0].Ingredients.Add(new IngredientElement(typeof(CoolantCirculationSystemItem), 1, true));
            this.Recipe[0].Ingredients.Add(new IngredientElement(typeof(HighCapacityBatteryItem), 10, true));
        }
    }
}
