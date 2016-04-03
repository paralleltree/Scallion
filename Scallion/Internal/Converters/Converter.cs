using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.Internal.Converters
{
    internal interface IConverter<TSource, TDest>
    {
        TDest Convert(TSource src);
        TSource ConvertBack(TDest src);
    }

    internal interface IInstanceConverter<TSource, TDest>
    {
        TDest Convert(TSource src, TDest obj);
        TSource ConvertBack(TDest src, TSource obj);
    }
}
