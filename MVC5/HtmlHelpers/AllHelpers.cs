using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MVC5.HtmlHelpers
{
    public static class AllHelpers
    {
        /// <summary>
        /// Shows disabled DropDownList if enabled is false
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="selectList"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes, bool enabled)
        {
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            
            
            if (!enabled)
            {
                attrs.Add("disabled", "disabled");
            }
            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, attrs);
        }
    }
}