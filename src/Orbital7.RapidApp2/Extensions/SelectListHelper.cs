using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class SelectListHelper
    {
        public static SelectList EnumToSelectList<T>(bool sort = true, bool useDisplayString = true)
        {
            return EnumToSelectList(typeof(T), sort, useDisplayString);
        }

        public static SelectList EnumToSelectList(Type enumType, bool sort = true, bool useDisplayString = true)
        {
            var list = new List<SerializableTuple<string, string>>();

            foreach (Enum value in Enum.GetValues(enumType))
                list.Add(new SerializableTuple<string, string>(value.ToString(), useDisplayString ? value.ToDisplayString() : value.ToString()));

            if (sort)
                list = list.OrderBy(x => x.Item2).ToList();

            return list.ToSelectList();
        }

        public static SelectList CreateEmptySelectList()
        {
            return new List<SerializableTuple<Guid, string>>().ToSelectList();
        }
    }
}
