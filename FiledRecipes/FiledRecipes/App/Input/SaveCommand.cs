using FiledRecipes.Presenters;

namespace FiledRecipes.App.Input
{
    public class SaveCommand : CommandBase
    {
        private IRecipePresenter _receiver;

        public SaveCommand(IRecipePresenter receiver)
        {
            _receiver = receiver;
        }

        public override void Execute()
        {
            _receiver.Save();
        }
    }
}
