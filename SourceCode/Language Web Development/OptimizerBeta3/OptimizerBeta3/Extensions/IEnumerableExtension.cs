using Microsoft.AspNetCore.Mvc.Rendering;
using OptimizerBeta3.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, int selectedvalue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("Description"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedvalue.ToString())
                   };
        }

        public static IEnumerable<SelectListItem> ToSelectListItemByList<T>(this IEnumerable<T> items, int selectedvalue, string fkLookUpCategory)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("Description"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedvalue.ToString()) && item.GetPropertyValue("FKLookUpCategory").Equals(fkLookUpCategory.ToString())
                   };
        }

        public static IEnumerable<SelectListItem> ToSelectListName<T>(this IEnumerable<T> items, int selectedvalue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("Name"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedvalue.ToString())
                   };
        }

        public static IEnumerable<SelectListItem> ToSelectListColourName<T>(this IEnumerable<T> items, int selectedvalue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("ColourName"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedvalue.ToString())
                   };
        }

        public static IEnumerable<SelectListItem> ToSelectListCompanyName<T>(this IEnumerable<T> items, int selectedvalue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("CompanyName"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedvalue.ToString())
                   };
        }

        public static IEnumerable<SelectListItem> ToSelectListPersonName<T>(this IEnumerable<T> items, int selectedvalue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("PersonName"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedvalue.ToString())
                   };
        }

        public static IEnumerable<SelectListItem> ToSelectListEmployeeName<T>(this IEnumerable<T> items, int selectedvalue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("FullName"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedvalue.ToString())
                   };
        }

        public static IEnumerable<SelectListItem> ToSelectListArticleName<T>(this IEnumerable<T> items, int selectedvalue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("ArticleName"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedvalue.ToString())
                   };
        }

        public static IEnumerable<SelectListItem> ToSelectListHSNCode<T>(this IEnumerable<T> items, int selectedvalue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("HSNCode") + " - " + item.GetPropertyValue("Category") + " - " + item.GetPropertyValue("GSTPercentage"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedvalue.ToString())
                   };
        }

        public static IEnumerable<SelectListItem> ToSelectListStateName<T>(this IEnumerable<T> items, int selectedvalue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("StateName") + " - " + item.GetPropertyValue("StateCode"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedvalue.ToString())
                   };
        }

        public static IEnumerable<SelectListItem> ToSelectListLocationName<T>(this IEnumerable<T> items, int selectedvalue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("LocationName"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedvalue.ToString())
                   };
        }
    }
}
