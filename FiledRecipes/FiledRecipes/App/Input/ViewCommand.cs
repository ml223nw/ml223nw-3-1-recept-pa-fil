using FiledRecipes.Domain;
using FiledRecipes.Presenters;

namespace FiledRecipes.App.Input
{
    public class ViewCommand : CommandBase
    {
        private IRecipePresenter _receiver;

        public ViewCommand(IRecipePresenter receiver)
        {
            _receiver = receiver;
        }

        public override void Execute()
        {
            _receiver.Show((Recipe)Argument);
        }
    }
}
