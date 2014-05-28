using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyVanity.Common.Helpers;

namespace MyVanity.Web.MvcHelpers
{
    public class SelectListHelpers
    {
        /// <summary>
        /// Gets the Description for a given enum value
        /// </summary>
        /// <param name="value">Enum item</param>
        /// <returns>The description of the item or the enum casted to string</returns>
        public static string GetEnumDescription(Enum value)
        {
            return ReflectionExtensions.GetEnumDescription(value);
        } 

        public static List<SelectListItem> BuildEnumOptions<T>(Enum selectedItem, SelectListItem noSelect = null, bool includeSelectOption = false)
        {
            var response = new Dictionary<int, string>();
            var type = typeof(T);

            if (!type.IsEnum)
                return new List<SelectListItem>();

            var enumValue = Enum.GetValues(type);
            var selectedIndex = -1;

            if (includeSelectOption)
            {
                var defaultSelector = noSelect ?? new SelectListItem { Text = "Please select", Value = "-1", Selected = true };
                int defaultValue;
                var parsed = Int32.TryParse(defaultSelector.Value, out defaultValue);
                defaultValue = parsed ? -1 : defaultValue;

                response.Add(defaultValue, defaultSelector.Text);
            }

            foreach (var enumName in enumValue)
            {
                var enumItem = enumName;
                var enumIndex = Convert.ToInt32(enumName);

                if (enumName.Equals(selectedItem))
                    selectedIndex = enumIndex;

                response.Add(enumIndex, GetEnumDescription((Enum)enumItem));
            }

            return response.Select(item => new SelectListItem
            {
                Selected = item.Key == selectedIndex,
                Text = item.Value,
                Value = item.Key.ToString()
            }).ToList();
        }

        public static List<SelectListItem> BuilListOptions<T>(List<T> itemsList, int selectedItemId, string optKeyProp, string optTextProp, SelectListItem noSelect = null, bool includeSelectOption = false)
        {
            if (itemsList == null)
                itemsList = new List<T>();

            var response = new Dictionary<int, string>();
            var selectedIndex = -1;

            if (includeSelectOption)
            {
                var defaultSelector = noSelect ?? new SelectListItem { Text = "Please select", Value = "-1", Selected = true };
                int defaultValue;
                var parsed = Int32.TryParse(defaultSelector.Value, out defaultValue);
                defaultValue = parsed ? -1 : defaultValue;

                response.Add(defaultValue, defaultSelector.Text);
            }

            foreach (var item in itemsList)
            {
                var optText = ReflectionExtensions.GetPropValue(item, optTextProp).ToString();
                var currentItemIndex = Convert.ToInt32(ReflectionExtensions.GetPropValue(item, optKeyProp));

                if (currentItemIndex == selectedItemId)
                    selectedIndex = currentItemIndex;

                response.Add(currentItemIndex, optText);
            }

            return response.Select(item => new SelectListItem
                {
                    Selected = item.Key == selectedIndex,
                    Text = item.Value,
                    Value = item.Key.ToString()
                }).ToList();
        }

        public static List<SelectListItem> BuildHoursOptions(List<TimeSpan> timeSpans, int selectedHour, string format)
        {
            var response = new Dictionary<int, string>();
            int selectedIndex = -1;

            foreach (var timeSpan in timeSpans)
            {
                var value = timeSpan.ToString(format);
                var index = timeSpan.Hours;

                if (index == selectedHour)
                    selectedIndex = index;

                response.Add(index, value);
            }

            return response.Select(x => new SelectListItem
                {
                    Selected = x.Key == selectedIndex,
                    Text = x.Value,
                    Value = x.Key.ToString()
                }).ToList();

        }
    }
}
