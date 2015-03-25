using FiledRecipes.App.Input;
using FiledRecipes.App.Mvp;
using FiledRecipes.Properties;
using FiledRecipes.Views;

namespace FiledRecipes.Presenters
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuPresenter : IMenuPresenter
    {
        /// <summary>
        /// 
        /// </summary>
        private IMenuModel _model;

        /// <summary>
        /// 
        /// </summary>
        private IMenuView _view;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="view"></param>
        public MenuPresenter(IMenuModel model, IMenuView view)
        {
            _model = model;
            _view = view;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICommand GetCommand()
        {
            _view.Header = Strings.ApplicationName;
            _view.MenuItems = _model.MenuItems;
            return _view.GetCommand();
        }
    }
}
