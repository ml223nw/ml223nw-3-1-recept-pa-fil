using FiledRecipes.Domain;
using FiledRecipes.App.Controls;
using FiledRecipes.App.Input;
using FiledRecipes.App.Mvp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiledRecipes.Presenters;
using FiledRecipes.Properties;

namespace FiledRecipes.Models
{
    class RecipeMenuModel : MenuModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="commands"></param>
        public RecipeMenuModel(IRecipeRepository repository, IEnumerable<ICommand> commands, IToolsPresenter presenter)
            :base(repository, commands, presenter)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void BuildMenu()
        {
            IEnumerable<IRecipe> recipes = _repository.GetAll();
            _menuItems = new List<IMenuItem>(recipes.Count() + 3);
            int index = 0;

            _menuItems.Add(new MenuItem { Text = Strings.MenuFile, IsSeparator = true });
            _menuItems.Add(new MenuItem { Text = Strings.Cancel, Command = _commands[0], Index = index });
            _menuItems.Add(new MenuItem { Text = _commands[1] is ViewCommand ? 
                Strings.MenuView : Strings.MenuDelete, IsSeparator = true });

            ICommand command = _commands[1];

            foreach (Recipe recipe in recipes)
            {
                ICommand itemCommand = (ICommand)(((ICloneable)command).Clone());
                itemCommand.Argument = recipe;
                _menuItems.Add(new MenuItem { Text = recipe.Name, Command = itemCommand, Index = ++index });
            }
        }
    }
}
