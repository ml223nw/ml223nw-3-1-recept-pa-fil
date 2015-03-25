using FiledRecipes.Presenters;

namespace FiledRecipes.App.Input
{
    public class OpenCommand : CommandBase
    {
        private IRecipePresenter _receiver;

        public OpenCommand(IRecipePresenter receiver)
        {
            _receiver = receiver;
        }

        public override void Execute()
        {
            _receiver.Load();
        }
    }
}
