using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FiledRecipes.Domain
{
    /// <summary>
    /// Holder for recipes.
    /// </summary>
    public class RecipeRepository : IRecipeRepository
    {
        /// <summary>
        /// Represents the recipe section.
        /// </summary>
        private const string SectionRecipe = "[Recept]";

        /// <summary>
        /// Represents the ingredients section.
        /// </summary>
        private const string SectionIngredients = "[Ingredienser]";

        /// <summary>
        /// Represents the instructions section.
        /// </summary>
        private const string SectionInstructions = "[Instruktioner]";

        /// <summary>
        /// Occurs after changes to the underlying collection of recipes.
        /// </summary>
        public event EventHandler RecipesChangedEvent;

        /// <summary>
        /// Specifies how the next line read from the file will be interpreted.
        /// </summary>
        private enum RecipeReadStatus { Indefinite, New, Ingredient, Instruction };

        /// <summary>
        /// Collection of recipes.
        /// </summary>
        private List<IRecipe> _recipes;

        /// <summary>
        /// The fully qualified path and name of the file with recipes.
        /// </summary>
        private string _path;

        /// <summary>
        /// Indicates whether the collection of recipes has been modified since it was last saved.
        /// </summary>
        public bool IsModified { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the RecipeRepository class.
        /// </summary>
        /// <param name="path">The path and name of the file with recipes.</param>
        public RecipeRepository(string path)
        {
            // Throws an exception if the path is invalid.
            _path = Path.GetFullPath(path);

            _recipes = new List<IRecipe>();
        }

        /// <summary>
        /// Returns a collection of recipes.
        /// </summary>
        /// <returns>A IEnumerable&lt;Recipe&gt; containing all the recipes.</returns>
        public virtual IEnumerable<IRecipe> GetAll()
        {
            // Deep copy the objects to avoid privacy leaks.
            return _recipes.Select(r => (IRecipe)r.Clone());
        }

        /// <summary>
        /// Returns a recipe.
        /// </summary>
        /// <param name="index">The zero-based index of the recipe to get.</param>
        /// <returns>The recipe at the specified index.</returns>
        public virtual IRecipe GetAt(int index)
        {
            // Deep copy the object to avoid privacy leak.
            return (IRecipe)_recipes[index].Clone();
        }

        /// <summary>
        /// Deletes a recipe.
        /// </summary>
        /// <param name="recipe">The recipe to delete. The value can be null.</param>
        public virtual void Delete(IRecipe recipe)
        {
            // If it's a copy of a recipe...
            if (!_recipes.Contains(recipe))
            {
                // ...try to find the original!
                recipe = _recipes.Find(r => r.Equals(recipe));
            }
            _recipes.Remove(recipe);
            IsModified = true;
            OnRecipesChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Deletes a recipe.
        /// </summary>
        /// <param name="index">The zero-based index of the recipe to delete.</param>
        public virtual void Delete(int index)
        {
            Delete(_recipes[index]);
        }

        /// <summary>
        /// Raises the RecipesChanged event.
        /// </summary>
        /// <param name="e">The EventArgs that contains the event data.</param>
        protected virtual void OnRecipesChanged(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of 
            // a race condition if the last subscriber unsubscribes 
            // immediately after the null check and before the event is raised.
            EventHandler handler = RecipesChangedEvent;

            // Event will be null if there are no subscribers. 
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, e);
            }
        }

        // Läser in recept.
        public void Load()
        {
            RecipeReadStatus recipeStatus = RecipeReadStatus.Indefinite;

            // Skapa lista som kan innehålla referenser till receptobjekt.
            List<IRecipe> Irecipes = new List<IRecipe>();

            // Öppna textfilen för läsning.
            using (StreamReader recipeReader = new StreamReader(_path))
            {
                string recipeLine = "";

                // Läs rad från textfilen tills det är slut på filen.
                while ((recipeLine = recipeReader.ReadLine()) != null)
                {
                    // Om det är en tom rad.
                    if (!String.IsNullOrEmpty(recipeLine))
                    {
                        if (recipeLine == SectionRecipe) // Om det är avdelningen för ingredienser.
                        {
                            recipeStatus = RecipeReadStatus.New;
                        }
                        else if (recipeLine == SectionIngredients)
                        {
                            recipeStatus = RecipeReadStatus.Ingredient;
                        }
                        else if (recipeLine == SectionInstructions) // Om det är avdelningen för instruktioner.
                        {
                            recipeStatus = RecipeReadStatus.Instruction;
                        }
                        else // Annars är det ett namn, en ingrediens eller en instruktion.
                        {
                            switch (recipeStatus)
                            {
                                // Om status är satt att raden ska tolkas som ett recepts namn.
                                case RecipeReadStatus.New:
                                    Irecipes.Add(new Recipe(recipeLine));
                                    break;
                                // Delar upp raden i delar.
                                case RecipeReadStatus.Ingredient:
                                    string[] ingredients = recipeLine.Split(new char[] { ';' }, StringSplitOptions.None);

                                    if (ingredients.Length != 3)
                                    {
                                        throw new FileFormatException();
                                    }
                                    // Skapa ett ingrediensobjekt och initiera det med de tre delarna för mängd, mått och namn.
                                    Ingredient ingredient = new Ingredient();

                                    ingredient.Amount = ingredients[0];
                                    ingredient.Measure = ingredients[1];
                                    ingredient.Name = ingredients[2];

                                    // Lägg till ingrediensen till receptets lista med ingredienser.
                                    Irecipes.Last().Add(ingredient);
                                    break;

                                case RecipeReadStatus.Instruction:

                                    Irecipes.Last().Add(recipeLine);
                                    break;
                                // Lägg till raden till receptets lista med instruktioner.
                                default:
                                    throw new FileFormatException("Fel format");

                            }
                        }
                    }
                }
                // Tar bort tomma platser i listan med recept
                Irecipes.TrimExcess();
                // Sorterar listan med recept med avseende på receptens namn.
                List<IRecipe> sortedRecipes = Irecipes.OrderBy(recipe => recipe.Name).ToList();
                IsModified = false;
                _recipes = sortedRecipes;
                OnRecipesChanged(EventArgs.Empty);
            }
        }

        // Spara till textfil.
        public void Save()
        {
            // Skapar objekt som recept ska skrivas till.
            using (StreamWriter recipePrint = new StreamWriter(_path))
            {
                //Loopar genom Irecipes
                foreach (IRecipe recipe in _recipes)
                {
                    recipePrint.WriteLine(SectionRecipe);
                    recipePrint.WriteLine(recipe.Name);

                    //Loopar genom ingredienser och lägger till dem.
                    recipePrint.WriteLine(SectionIngredients);
                    foreach (IIngredient ingredient in recipe.Ingredients)
                    {
                        recipePrint.WriteLine("{0};{1};{2}", ingredient.Amount, ingredient.Measure, ingredient.Name);
                    }
                    //Loopa genom och lägg till instruktioner.
                    recipePrint.WriteLine(SectionInstructions);
                    foreach (string recipeDetails in recipe.Instructions)
                    {
                        recipePrint.WriteLine(recipeDetails);
                    }
                }
                IsModified = false;

                OnRecipesChanged(EventArgs.Empty);
            }
        }
    }
}