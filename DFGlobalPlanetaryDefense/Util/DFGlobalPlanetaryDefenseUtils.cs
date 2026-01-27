using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Shared.Utils;

namespace DF.GlobalPlanetaryDefense
{
    // DF Utils class, rev 2
    public static class DFGlobalPlanetaryDefenseUtils
    {
        // Adds a new ingredient to the Default Recipe.
        // Allows recipe editing without requiring to override recipes in their entirety.
        public static void AddIngredient(RecipeFamily recipe, Item item, IngredientElement newIngredient)
        {
            recipe.DefaultRecipe.Ingredients.Add(newIngredient);
        }

        // Tries to replace a specific ingredient in a recipe with another ingredient.
        // Allows recipe editing without requiring to override recipes in their entirety.
        public static bool TryReplaceIngredient(RecipeFamily recipe, Item item, IngredientElement newIngredient)
        {
            int index = recipe.DefaultRecipe.Ingredients.IndexOf(ingredientElement => ingredientElement.Item == item);
            if (index != -1)
            {
                recipe.Ingredients[index] = newIngredient;
                return true;
            }
            return true;
        }

        // Tries to replace a tag ingredient in a recipe with another ingredient.
        // Allows recipe editing without requiring to override recipes in their entirety.
        public static bool TryReplaceIngredient(RecipeFamily recipe, string tag, IngredientElement newIngredient)
        {
            int index = recipe.DefaultRecipe.Ingredients.IndexOf(ingredientElement => ingredientElement.Tag.Name == tag);
            if (index != -1)
            {
                recipe.Ingredients[index] = newIngredient;
                return true;
            }
            return false;
        }

        // Tries to remove a specific ingredient.
        // Allows recipe editing without requiring to override recipes in their entirety.
        public static bool TryRemoveIngredient(RecipeFamily recipeFamily, Item item)
        {
            foreach (var recipe in recipeFamily.Recipes)
            {
                int index = recipe.Ingredients.IndexOf(ingredientElement => ingredientElement.Item == item);
                if (index != -1)
                {
                    recipe.Ingredients.RemoveAt(index);
                    return true;
                }
            }
            return false;
        }

        // Tries to remove a tag ingredient.
        // Allows recipe editing without requiring to override recipes in their entirety.
        public static bool TryRemoveIngredient(RecipeFamily recipeFamily, string tag)
        {
            foreach (var recipe in recipeFamily.Recipes)
            {
                int index = recipe.Ingredients.IndexOf(ingredientElement => ingredientElement.Tag.Name == tag);
                if (index != -1)
                {
                    recipe.Ingredients.RemoveAt(index);
                    return true;
                }
            }
            return false;
        }

        // Adds a new product to the Default Recipe.
        // Allows recipe editing without requiring to override recipes in their entirety.
        public static void AddProduct(RecipeFamily recipe, CraftingElement newProduct)
        {
            recipe.DefaultRecipe.Products.Add(newProduct);
        }
    }
}
