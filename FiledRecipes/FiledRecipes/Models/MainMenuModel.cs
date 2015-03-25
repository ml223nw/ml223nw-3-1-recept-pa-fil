using FiledRecipes.Domain;
using FiledRecipes.App.Controls;
using FiledRecipes.App.Input;
using FiledRecipes.App.Mvp;
using FiledRecipes.Properties;
using System.Collections.Generic;
using System.Linq;
using FiledRecipes.Presenters;

namespace FiledRecipes.Models
{
    public class MainMenuModel : MenuModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="commands"></param>
        public MainMenuModel(IRecipeRepository repository, IEnumerable<ICommand> commands, IToolsPresenter presenter)
            :base(repository, commands, presenter)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void BuildMenu()
        {
            _menuItems = new List<IMenuItem>(9);
            _menuItems.Add(new MenuItem { Text = Strings.MenuFile, IsSeparator = true });
            _menuItems.Add(new MenuItem { Text = Strings.MenuFileExit, Command = _commands.Single(c => c is ExitCommand), Index = 0 });
            _menuItems.Add(new MenuItem { Text = Strings.MenuFileOpen, Command = _commands.Single(c => c is OpenCommand), Index = 1 });
            if (_repository.IsModified)
            {
                _menuItems.Add(new MenuItem { Text = Strings.MenuFileSave, Command = _commands.Single(c => c is SaveCommand), Index = 2 });
            }
            if (_repository.GetAll().Any())
            {
                _menuItems.Add(new MenuItem { Text = Strings.MenuEdit, IsSeparator = true });
                _menuItems.Add(new MenuItem { Text = Strings.MenuEditDelete, Command = _commands.Single(c => c is DeleteListCommand), Index = 3 });
                _menuItems.Add(new MenuItem { Text = Strings.MenuView, IsSeparator = true });
                _menuItems.Add(new MenuItem { Text = Strings.MenuViewRecipe, Command = _commands.Single(c => c is ViewListCommand), Index = 4 });
                _menuItems.Add(new MenuItem { Text = Strings.MenuViewAllRecipies, Command = _commands.Single(c => c is ViewAllCommand), Index = 5 });
            }
            _menuItems.Add(new MenuItem { Text = Strings.MenuTools, IsSeparator = true });
            _menuItems.Add(new MenuItem { Text = Strings.MenuToolsChangeLanguage, Command = _commands.Single(c => c is ChangeLanguageCommand), Index = 6 });
            _menuItems.TrimExcess();
        }
    }
}
