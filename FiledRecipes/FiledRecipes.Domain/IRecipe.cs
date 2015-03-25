using System;
using System.Collections.Generic;

namespace FiledRecipes.Domain
{
    /// <summary>
    /// Defines members describing a recipe.
    /// </summary>
    public interface IRecipe : ICloneable
    {
        string Name { get; set; }

        IEnumerable<IIngredient> Ingredients { get; }

        IEnumerable<string> Instructions { get; }

        void Add(IIngredient ingredient);

        void Add(string instruction);
    }
}
