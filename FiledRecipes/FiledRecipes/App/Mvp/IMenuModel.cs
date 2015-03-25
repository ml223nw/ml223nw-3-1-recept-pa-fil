using FiledRecipes.App.Controls;
using System.Collections.Generic;

namespace FiledRecipes.App.Mvp
{
    /// <summary>
    /// Defines the members required for a menu model.
    /// </summary>
    public interface IMenuModel
    {
        IEnumerable<IMenuItem> MenuItems { get; }
    }
}
