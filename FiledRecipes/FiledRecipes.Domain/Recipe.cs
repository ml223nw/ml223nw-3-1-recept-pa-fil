using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FiledRecipes.Domain
{
    /// <summary>
    /// Provides members to manage a recipe.
    /// </summary>
    public class Recipe : IRecipe, IEquatable<Recipe>, IComparable, IComparable<Recipe>
    {
        /// <summary>
        /// The name of the recipe.
        /// </summary>
        private string _name;

        /// <summary>
        /// Collection of ingredients.
        /// </summary>
        private List<IIngredient> _ingredients;

        /// <summary>
        /// Collection of instructions.
        /// </summary>
        private List<string> _instructions;

        /// <summary>
        /// Gets or sets the name of the recipe.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException();
                }
                // Removes leading and traling white spaces and then replaces 
                // consecutive white spaces with a single white space.
                _name = Regex.Replace(value.Trim(), @"\s{2,}", " ");
            }
        }

        /// <summary>
        /// Gets the ingredients of a recipe.
        /// </summary>
        public IEnumerable<IIngredient> Ingredients
        {
            get { return _ingredients.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the instructions of a recipe.
        /// </summary>
        public IEnumerable<string> Instructions
        {
            get { return _instructions.AsReadOnly(); }
        }

        /// <summary>
        /// Initializes a new instance of the Recipe class.
        /// </summary>
        /// <param name="name">The name of the recipe.</param>
        public Recipe(string name)
            : this(name, new List<IIngredient>(), new List<string>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the Recipe class.
        /// </summary>
        /// <param name="name">The name of the recipe.</param>
        /// <param name="ingredients">A collection of ingredients.</param>
        /// <param name="instructions">A collection of instructions.</param>
        public Recipe(string name, IEnumerable<IIngredient> ingredients, IEnumerable<string> instructions)
        {
            Name = name;
            _ingredients = ingredients.ToList();
            _instructions = instructions.ToList();
        }

        /// <summary>
        /// Adds an instruction.
        /// </summary>
        /// <param name="instruction">The instruction to be added.</param>
        public void Add(string instruction)
        {
            _instructions.Add(instruction);
        }

        /// <summary>
        /// Adds an ingredient.
        /// </summary>
        /// <param name="ingredient">The ingredient to be added.</param>
        public void Add(IIngredient ingredient)
        {
            _ingredients.Add(ingredient);
        }

        /// <summary>
        /// Determines whether this instance and a specified object, 
        /// which must also be a Recipe object, have the same value.
        /// </summary>
        /// <param name="obj">The recipe to compare to this instance.</param>
        /// <returns>true if obj is a Recipe and its value is the same as this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            
            // If parameter cannot be cast to Point return false.
            Recipe other = obj as Recipe;
            if (other == null)
            {
                return false;
            }

            // Return true if the fields match:
            return _name.Equals(other._name) && 
                _ingredients.SequenceEqual(other._ingredients) &&
                _instructions.SequenceEqual(other._instructions);
        }

        /// <summary>
        /// Determines whether this instance and another specified Recipe object have the same value.
        /// </summary>
        /// <param name="other">The recipe to compare to this instance.</param>
        /// <returns>true if the value of the other parameter is the same as this instance; otherwise, false.</returns>
        public bool Equals(Recipe other)
        {
            if (other == null)
            {
                return false;
            }
            
            // Return true if the fields match.
            return _name.Equals(other._name, StringComparison.Ordinal) &&
                _ingredients.SequenceEqual(other._ingredients) &&
                _instructions.SequenceEqual(other._instructions);
        }

        /// <summary>
        /// Returns the hash code for this recipe.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            // http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                hash = hash * 16777619 ^ _name.GetHashCode();
                hash = hash * 16777619 ^ _ingredients.GetHashCode();
                hash = hash * 16777619 ^ _instructions.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Compares two Recipe objects for equality.
        /// </summary>
        /// <param name="recipe1"></param>
        /// <param name="recipe2"></param>
        /// <returns></returns>
        public static bool operator == (Recipe recipe1, Recipe recipe2)
        {
            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(recipe1, recipe2))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)recipe1 == null) || ((object)recipe2 == null))
            {
                return false;
            }

            // Return true if the objects match; otherwise false.
            return recipe1.Equals(recipe2);
        }

        /// <summary>
        /// Compares two Recipe objects for unequality.
        /// </summary>
        /// <param name="recipe1"></param>
        /// <param name="recipe2"></param>
        /// <returns></returns>
        public static bool operator != (Recipe recipe1, Recipe recipe2)
        {
            return !(recipe1 == recipe2);
        }

        #region IComparable Members

        /// <summary>
        /// Compares this instance with a specified Object and indicates whether this instance precedes, 
        /// follows, or appears in the same position in the sort order as the specified Object.
        /// </summary>
        /// <param name="obj">An object that evaluates to a Recipe.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, 
        /// or appears in the same position in the sort order as the value parameter.</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is Recipe))
            {
                throw new ArgumentException("The object is not a Recipe.");
            }

            return CompareTo((Recipe)obj);
        }

        #endregion

        #region IComparable<Recipe> Members

        /// <summary>
        /// Compares this instance with a specified Recipe object and indicates whether this instance precedes, 
        /// follows, or appears in the same position in the sort order as the specified Recipe.
        /// </summary>
        /// <param name="other">The recipe to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, 
        /// or appears in the same position in the sort order as the value parameter.</returns>
        public int CompareTo(Recipe other)
        {
            if (other == null)
            {
                return 1;
            }

            return Name.CompareTo(other.Name);
        }

        #endregion

        #region IClone Members

        /// <summary>
        /// Creates a new instance of this object, and copies the data from this object to the new object.
        /// </summary>
        /// <returns>The newly created copy of this object.</returns>
        public object Clone()
        {
            // Deep copy (the constructor creates copies).
            return new Recipe(_name, _ingredients, _instructions);
        }

        #endregion
    }
}
