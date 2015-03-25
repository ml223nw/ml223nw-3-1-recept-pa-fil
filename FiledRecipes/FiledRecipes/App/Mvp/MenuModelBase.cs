using FiledRecipes.App.Controls;
using FiledRecipes.App.Input;
using FiledRecipes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiledRecipes.App.Mvp
{
    /// <summary>
    /// Represents the base class for all menu models.
    /// </summary>
    public abstract class MenuModelBase : IMenuModel
    {
        /// <summary>
        /// Collection of commands.
        /// </summary>
        protected List<ICommand> _commands;

        /// <summary>
        /// Collections of menu items.
        /// </summary>
        protected List<IMenuItem> _menuItems;

        /// <summary>
        /// 
        /// </summary>
        protected IRecipeRepository _repository;

        /// <summary>
        /// Returns a read-only collection of menu items.
        /// </summary>
        public IEnumerable<IMenuItem> MenuItems
        {
            get { return _menuItems.AsReadOnly(); }
        }

        /// <summary>
        /// Initializes a new instance of the MenuModelBase class.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="commands"></param>
        /// <param name="languageChanged"></param>
        protected MenuModelBase(IRecipeRepository repository, IEnumerable<ICommand> commands, ILanguageChanged languageChanged)
        {
            _repository = repository;
            _repository.RecipesChangedEvent += HandleChangedEvent;

            _commands = commands.ToList();

            languageChanged.LanguageChangedEvent += HandleChangedEvent;
            
            BuildMenu();
        }

        /// <summary>
        /// Builds a menu of the menu items.
        /// </summary>
        protected abstract void BuildMenu();

        /// <summary>
        /// Handles a changed event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void HandleChangedEvent(object sender, EventArgs e)
        {
            BuildMenu();
        }
    }
}
