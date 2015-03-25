using System;

namespace FiledRecipes.App.Input
{
    public abstract class CommandBase : ICommand, ICloneable
    {
        public object Argument { get; set; }

        public abstract void Execute();

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
