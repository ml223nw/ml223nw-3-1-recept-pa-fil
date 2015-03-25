using FiledRecipes.Presenters;

namespace FiledRecipes.App.Input
{
    public class ViewAllCommand : CommandBase
    {
        private IRecipePresenter _receiver;

        public ViewAllCommand(IRecipePresenter receiver)
        {
            _receiver = receiver;
        }

        public override void Execute()
        {
            _receiver.ShowAll();
        }
    }
}
