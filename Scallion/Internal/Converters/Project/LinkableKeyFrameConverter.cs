using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.Internal.Converters.Project
{
    internal static class LinkableKeyFrameConverter
    {
        public static LinkedList<T> Extract<T>(this List<T> src, T initial) where T : Raw.Components.Project.ILinkableKeyFrame
        {
            var dic = new Dictionary<int, T>(src.Count + 1);
            foreach (var item in src)
                dic.Add(item.DataIndex, item);

            var list = new LinkedList<T>();
            list.AddLast(initial);

            var current = initial; // treat as DataIndex = 0
            while (current.NextDataIndex != 0)
            {
                current = dic[current.NextDataIndex];
                list.AddLast(current);
            }

            return list;
        }

        public static List<T> Pack<T>(this List<T> src, int initIndex) where T : Raw.Components.Project.ILinkableKeyFrame
        {
            return src.Pack(initIndex, 0);
        }

        public static List<T> Pack<T>(this List<T> src, int initIndex, int prevIndex) where T : Raw.Components.Project.ILinkableKeyFrame
        {
            for (int i = 0; i < src.Count; i++)
            {
                src[i].DataIndex = initIndex + i;
                src[i].NextDataIndex = i == src.Count - 1 ? 0 : initIndex + i + 1;
                src[i].PreviousDataIndex = i == 0 ? prevIndex : initIndex + i - 1;
            }
            return src;
        }
    }
}
