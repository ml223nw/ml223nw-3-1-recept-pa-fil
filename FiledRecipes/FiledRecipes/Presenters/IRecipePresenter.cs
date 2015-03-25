using FiledRecipes.Domain;

namespace FiledRecipes.Presenters
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRecipePresenter
    {
        void Delete(IRecipe recipe);

        void Load();

        void Save();

        void Show(IRecipe recipe);

        void ShowAll();
    }
}
