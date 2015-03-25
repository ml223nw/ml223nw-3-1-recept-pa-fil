using FiledRecipes.Domain;
using FiledRecipes.Presenters;

namespace FiledRecipes.App.Input
{
    public class DeleteCommand : CommandBase
    {
        private IRecipePresenter _receiver;

        public DeleteCommand(IRecipePresenter receiver)
        {
            _receiver = receiver;
        }

        public override void Execute()
        {
            _receiver.Delete((Recipe)Argument);
        }
    }
}
