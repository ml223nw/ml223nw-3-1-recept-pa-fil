using FiledRecipes.App.Input;
using System.Collections.Generic;

namespace FiledRecipes.App.Controls
{
    public interface IMenu
    {
        IEnumerable<IMenuItem> Items { get; set; }
        ICommand GetCommand();
        void Show();
    }
}
