using System;

namespace Analyzer.Library
{
    /// <summary>
    /// A client side view of the data model
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Update all data from the server
        /// </summary>
        void Pull();

        /// <summary>
        /// Get specific piece of data
        /// </summary>
        /// <param name="property"></param>
        /// <returns>Whether the value is immediately available</returns>
        bool TryGet(string name, out object value);

        /// <summary>
        /// Set specific variable
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        void Set(string name, object value);

        /// <summary>
        /// Set specific variable and return whether variable was changed
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns>Whether the value was changed</returns>
        bool TryChange(string name, object value);
    }
}