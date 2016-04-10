using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.Internal.Converters
{
    /// <summary>
    /// Provides a way to convert between two types of objects.
    /// </summary>
    /// <typeparam name="TSource">The type of the object which will be converted from</typeparam>
    /// <typeparam name="TDest">The type of the object which will be converted to</typeparam>
    internal interface IConverter<TSource, TDest>
    {
        /// <summary>
        /// Converts the specified object into <typeparamref name="TDest"/>.
        /// </summary>
        /// <param name="src">The source object to be converted</param>
        /// <returns>A converted object</returns>
        TDest Convert(TSource src);

        /// <summary>
        /// Converts back the specified object into <typeparamref name="TSource"/>.
        /// </summary>
        /// <param name="src">The source object to be converted back</param>
        /// <returns>A converted object</returns>
        TSource ConvertBack(TDest src);
    }

    /// <summary>
    /// Provides a way to convert with specified instance between two types of objects.
    /// </summary>
    /// <typeparam name="TSource">The type of the object which will be converted from</typeparam>
    /// <typeparam name="TDest">The type of the object which will be converted to</typeparam>
    internal interface IInstanceConverter<TSource, TDest>
    {
        /// <summary>
        /// Converts the specified object into <typeparamref name="TDest"/>.
        /// </summary>
        /// <param name="src">The source object to be converted</param>
        /// <param name="obj">The object that converted values assigned to</param>
        /// <returns>A converted object</returns>
        TDest Convert(TSource src, TDest obj);

        /// <summary>
        /// Converts back the specified object into <typeparamref name="TSource"/>.
        /// </summary>
        /// <param name="src">The source object to be converted back</param>
        /// <param name="obj">The object that converted values assigned to</param>
        /// <returns>A converted object</returns>
        TSource ConvertBack(TDest src, TSource obj);
    }
}
