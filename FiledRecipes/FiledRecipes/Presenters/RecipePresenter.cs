using FiledRecipes.App.Controls;
using FiledRecipes.Domain;
using FiledRecipes.Properties;
using FiledRecipes.Views;
using System;
using System.IO;

namespace FiledRecipes.Presenters
{
    /// <summary>
    /// 
    /// </summary>
    public class RecipePresenter : IRecipePresenter
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRecipeView _view;

        /// <summary>
        /// 
        /// </summary>
        private readonly IRecipeRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        public RecipePresenter(IRecipeRepository repository, IRecipeView view)
        {
            _repository = repository;
            _view = view;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipe"></param>
        public void Delete(IRecipe recipe)
        {
            _repository.Delete(recipe);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Load()
        {
            try
            {
                _repository.Load();
                _view.ShowPanel(Strings.RecipesLoaded, MessagePanelOptions.Success);
            }
            catch (FileFormatException)
            {
                _view.ShowPanel(Strings.ErrorReadingRecipes, MessagePanelOptions.Alert);
            }
            catch (Exception ex)
            {
                _view.ShowPanel(ex.Message, MessagePanelOptions.Alert);
            }

            _view.ContinueOnKeyPressed();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            try
            {
                _repository.Save();
                _view.ShowPanel(Strings.RecipesSaved, MessagePanelOptions.Success);
            }
            catch (Exception ex)
            {
                _view.ShowPanel(ex.Message, MessagePanelOptions.Alert);
            }

            _view.ContinueOnKeyPressed();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipe"></param>
        public void Show(IRecipe recipe)
        {
            _view.Show(recipe);
            _view.ContinueOnKeyPressed();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowAll()
        {
            _view.Show(_repository.GetAll());
        }
    }
}
