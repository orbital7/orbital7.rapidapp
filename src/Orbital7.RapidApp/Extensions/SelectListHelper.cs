﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class SelectListHelper
    {
        public static SelectList EnumToSelectList<T>(
            bool sort = true, 
            bool useDisplayString = true)
        {
            return EnumToSelectList(typeof(T), sort, useDisplayString);
        }

        public static SelectList EnumToSelectList(
            Type enumType, 
            bool sort = true, 
            bool useDisplayString = true)
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

        public static SelectList CreateTempSelectList(
            Guid? selectedId)
        {
            var list = new List<SerializableTuple<Guid, string>>();
            if (selectedId.HasValue)
                list.Add(new SerializableTuple<Guid, string>(selectedId.Value, null));

            return list.ToSelectList();
        }

        public static SelectList CreateTempSelectList(
            DateTime? selectedDate)
        {
            var list = new List<SerializableTuple<DateTime, string>>();
            if (selectedDate.HasValue)
                list.Add(new SerializableTuple<DateTime, string>(selectedDate.Value, null));

            return list.ToSelectList();
        }

        public static SelectList CreateTempSelectList(
            double? selectedId)
        {
            var list = new List<SerializableTuple<double, string>>();
            if (selectedId.HasValue)
                list.Add(new SerializableTuple<double, string>(selectedId.Value, null));

            return list.ToSelectList();
        }

        public static SelectList CreateHourSelectList(
            int startHour = 0,
            int endHour = 23)
        {
            var list = new List<SerializableTuple<int, string>>();

            for (var i = startHour; i <= endHour; i++)
                list.Add(new SerializableTuple<int, string>(i, i.ToHour()));

            return list.ToSelectList();
        }

        public static SelectList CreateQuarterHourSelectList(
            int startHour = 0,
            int endHour = 23)
        {
            var list = new List<SerializableTuple<double, string>>();

            for (int i = startHour; i <= endHour; i++)
            {
                for (double j = 0; j <= 0.75; j += 0.25)
                {
                    var time = i + j;
                    list.Add(new SerializableTuple<double, string>(time, time.ToTime()));
                }
            }

            return list.ToSelectList();
        }
    }
}
