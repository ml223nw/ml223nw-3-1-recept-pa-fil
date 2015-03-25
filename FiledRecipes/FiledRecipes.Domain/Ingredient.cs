using System;
using System.Text.RegularExpressions;

namespace FiledRecipes.Domain
{
    /// <summary>
    /// Describes the amount, measure and name of an ingredient.
    /// </summary>
    public struct Ingredient : IIngredient, IEquatable<Ingredient>, IComparable, IComparable<Ingredient>
    {
        /// <summary>
        /// Gets or sets the amount of the ingredient.
        /// </summary>
        public string Amount { get; set; }
        
        /// <summary>
        /// Gets or sets the measure of the ingredient.
        /// </summary>
        public string Measure { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the ingredient.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns>true if value is an instance of Ingredient and equals the value of this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Ingredient))
            {
                return false;
            }

            return Equals((Ingredient)obj);
        }

        /// <summary>
        /// Returns a value indicating whether the value of this instance is 
        /// equal to the value of the specified Ingredient instance.
        /// </summary>
        /// <param name="other">The object to compare to this instance.</param>
        /// <returns>true if the value parameter equals the value of this instance; otherwise, false.</returns>
        public bool Equals(Ingredient other)
        {
            return Name.Equals(other.Name) &&
                Amount.Equals(other.Amount) &&
                Measure.Equals(other.Measure);
        }

        /// <summary>
        /// Returns the hash code for this ingredient.
        /// </summary>
        /// <returns>A hash code for the current value.</returns>
        public override int GetHashCode()
        {
            // http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                hash = hash * 16777619 ^ Name.GetHashCode();
                hash = hash * 16777619 ^ Amount.GetHashCode();
                hash = hash * 16777619 ^ Measure.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Compares the value of this instance to a specified object that contains a specified 
        /// Ingredient value, and returns an integer that indicates whether this instance is 
        /// earlier than, the same as, or later than the specified DateTime value.
        /// </summary>
        /// <param name="value">A boxed object to compare, or null.</param>
        /// <returns>A signed number indicating the relative values of this instance and the value parameter.</returns>
        public int CompareTo(object value)
        {
            if (value == null)
            {
                return 1;
            }

            if (!(value is Ingredient))
            {
                throw new ArgumentException("The value is not an Ingredient.");
            }

            return CompareTo((Ingredient)value);
        }

        /// <summary>
        /// Compares the value of this instance to a specified Ingredient value and returns an 
        /// integer that indicates whether this instance is earlier than, the same as, or later 
        /// than the specified Ingredient value.
        /// </summary>
        /// <param name="other">The object to compare to the current instance.</param>
        /// <returns>A signed number indicating the relative values of this instance and the value parameter.</returns>
        public int CompareTo(Ingredient other)
        {
            int result = Name.CompareTo(other.Name);
            if (result != 0)
            {
                return result;
            }
            result = Amount.CompareTo(other.Amount);
            if (result != 0)
            {
                return result;
            }
            return Measure.CompareTo(other.Measure);
        }

        /// <summary>
        /// Returns a string that represents the current instance.
        /// </summary>
        /// <returns>A string representation of the ingredient.</returns>
        public override string ToString()
        {
            // Removes leading and traling white spaces and then replaces consecutive white spaces with a single white space.
            return Regex.Replace(String.Format("{0} {1} {2}", Amount, Measure, Name).Trim(), @"\s{2,}", " ");
        }

    }
}
