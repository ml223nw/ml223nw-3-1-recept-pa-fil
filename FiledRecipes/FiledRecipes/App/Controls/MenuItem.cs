using FiledRecipes.App.Input;
using System;

namespace FiledRecipes.App.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuItem : IMenuItem, ICloneable
    {
        /// <summary>
        /// 
        /// </summary>
        public ICommand Command { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSeparator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MenuItem()
        {
            Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }

        #region IClone Members

        /// <summary>
        /// Creates a new instance of this object, and copies the data from this object to the new object.
        /// </summary>
        /// <returns>The newly created copy of this object.</returns>
        public object Clone()
        {
            // No members are mutable why a shallow copy is good enough.
            return MemberwiseClone();
        }

        #endregion
    }
}
