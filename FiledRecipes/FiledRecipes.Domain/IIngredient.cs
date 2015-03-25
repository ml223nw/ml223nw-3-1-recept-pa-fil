using System;

namespace FiledRecipes.Domain
{
    /// <summary>
    /// Defines members describing an ingredient of a recipe.
    /// </summary>
    public interface IIngredient
    {
        string Amount { get; set; }

        string Measure { get; set; }

        string Name { get; set; }
    }
}
