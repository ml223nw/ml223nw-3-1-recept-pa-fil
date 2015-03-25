using FiledRecipes.Domain;
using FiledRecipes.App.Mvp;
using FiledRecipes.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiledRecipes.Views
{
    /// <summary>
    /// 
    /// </summary>
    public class RecipeView : ViewBase, IRecipeView
    {
        public void Show(IRecipe recipe)
        {
            Header = recipe.Name;
            ShowHeaderPanel();

            Console.WriteLine("\nIngredienser:\n");
            foreach (IIngredient ingredient in recipe.Ingredients)
            {
                Console.WriteLine(ingredient);
            }

            Console.WriteLine("\nInstruktioner:\n");
            int line = 1;
            foreach (string instruction in recipe.Instructions)
            {
                Console.WriteLine("{0}: {1}", line, instruction);
                line++;
            }
        }

        public void Show(IEnumerable<IRecipe> recipes)
        {
            foreach (IRecipe recipe in recipes)
            {
                Show(recipe);
                ContinueOnKeyPressed();
            }
        }
    }
}