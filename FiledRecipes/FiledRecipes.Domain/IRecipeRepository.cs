using System;
using System.Collections.Generic;

namespace FiledRecipes.Domain
{
    /// <summary>
    /// Defines members representing a holder for recipes.
    /// </summary>
    public interface IRecipeRepository
    {
        event EventHandler RecipesChangedEvent;

        bool IsModified { get; }

        void Delete(IRecipe recipe);

        void Delete(int index);

        IRecipe GetAt(int index);

        IEnumerable<IRecipe> GetAll();

        void Load();

        void Save();
    }
}
