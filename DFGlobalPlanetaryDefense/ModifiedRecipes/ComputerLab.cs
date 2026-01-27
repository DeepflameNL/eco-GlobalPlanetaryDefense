using DF.GlobalPlanetaryDefense;
using Eco.Gameplay.Items.Recipes;

namespace Eco.Mods.TechTree
{
    public partial class ComputerLabRecipe : RecipeFamily
    {
        partial void ModsPreInitialize()
        {
            this.DefaultRecipe.Ingredients.Clear();
            this.DefaultRecipe.Ingredients.Add(new IngredientElement(typeof(CentralProcessingCoreItem), 1, true));
            this.DefaultRecipe.Ingredients.Add(new IngredientElement(typeof(MagneticTapeArrayItem), 1, true));
            this.DefaultRecipe.Ingredients.Add(new IngredientElement(typeof(RadarTransceiverItem), 1, true));
            this.DefaultRecipe.Ingredients.Add(new IngredientElement(typeof(HighPerformancePowerRackItem), 1, true));
            this.DefaultRecipe.Ingredients.Add(new IngredientElement(typeof(CoolantCirculationSystemItem), 1, true));
        }
    }
}
