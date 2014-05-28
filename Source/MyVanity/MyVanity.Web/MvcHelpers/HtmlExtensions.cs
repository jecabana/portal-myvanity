using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MyVanity.Common.Helpers;

namespace MyVanity.Web.MvcHelpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString RequiredLabelFor<TModel, TProperty>(this HtmlHelper<TModel> sender, Expression<Func<TModel, TProperty>> property, object htmlAttributes)
        {
            var resp = sender.LabelFor(property, htmlAttributes);
            const string required = "<span class=\"requiredStar\">*</span>";

            return MvcHtmlString.Create(resp.ToHtmlString() + required);
        }

        public static MvcHtmlString GDropDownListFor<TModel, TProperty, TSourceModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property, List<TSourceModel> source, 
                                                                                      string propertyName, string valuePropertyName, object htmlAttributes = null, 
                                                                                      bool includePleaseSelect = false, bool mandatory = false)
        {
            var valueGetter = property.Compile();
            var value = valueGetter(helper.ViewData.Model);

            includePleaseSelect = includePleaseSelect || mandatory;
            var selectList = SelectListHelpers.BuilListOptions(source, Convert.ToInt32(value), valuePropertyName,
                                                               propertyName, null, includePleaseSelect);
            
            if (mandatory && selectList.Count != 0)
                selectList[0].Value = "";
            
            return helper.DropDownListFor(property, selectList, htmlAttributes);
        }

        public static MvcHtmlString DropDownListFor<TModel, TProperty, TSourceModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property, List<TSourceModel> source, string titlePropertyName, string valuePropertyName, object htmlAttributes = null, bool includePleaseSelect = false)
        {
            var resultingHtml = new StringBuilder();

            var sourceModelType = typeof (TSourceModel);
            var titleProperty = sourceModelType.GetProperties().SingleOrDefault(p => p.Name == titlePropertyName);
            var valueProperty = sourceModelType.GetProperties().SingleOrDefault(p => p.Name == valuePropertyName);

            if (titleProperty == null || valueProperty == null)
                throw new InvalidOperationException("Property names for title or value are not correct");

            var controlName = helper.NameFor(property);
            var selectBody = string.Format("<select name=\"{0}\"", controlName);

            if (htmlAttributes != null)
            {
                var props = htmlAttributes.GetType().GetProperties();

                foreach (var attribute in props)
                {
                    var val = attribute.GetValue(htmlAttributes, new object[] {});
                    var attributeText = string.Format("{0}='{1}' ", attribute.Name, val);
                    selectBody += attributeText;
                }
            }

            selectBody += " />";
            resultingHtml.AppendLine(selectBody);

            if (includePleaseSelect)
                resultingHtml.AppendFormat("\t<option value=\"{0}\">{1}</option>", "", "Please select");

            if (source != null)
            {
                foreach (var model in source)
                {
                    var title = titleProperty.GetValue(model, new object[] { }).ToString();
                    var value = valueProperty.GetValue(model, new object[] { }).ToString();

                    resultingHtml.AppendFormat("\t<option value=\"{0}\">{1}</option>", value, title);
                }
            }

            resultingHtml.Append("<select />");
            return MvcHtmlString.Create(resultingHtml.ToString());
        }

        public static MvcHtmlString RadioListForEnum<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property, bool uncheck = false)
        {
            var valueGetter = property.Compile();
            var value = valueGetter(helper.ViewData.Model);

            var propertyInfo = ReflectionExtensions.GetPropertyInfo(property);
            var resultingHtml = new StringBuilder();

            var enumTypeDefinition = propertyInfo.PropertyType;
            var allEnumValues = Enum.GetValues(enumTypeDefinition);
            
            resultingHtml.AppendLine("<ul class=\"radioList\">");

            foreach (var enumValue in allEnumValues)
            {
                resultingHtml.AppendLine("<li>");
                var description = ReflectionExtensions.GetEnumDescription((Enum) enumValue);
                var isChecked = enumValue.Equals(value) && !uncheck ? " checked=\"checked\"" : string.Empty;

                var inputId = propertyInfo.Name + "-" + enumValue;
                var inputHtml = string.Format("<input type=\"radio\"{2} value=\"{3}\" id=\"{0}\" name=\"{1}\" />", inputId, propertyInfo.Name, isChecked, enumValue);
                var labelHtml = string.Format("<label for=\"{0}\">{1}</label>", inputId, description);

                resultingHtml.AppendLine(inputHtml);
                resultingHtml.AppendLine(labelHtml);

                resultingHtml.AppendLine("</li>");
            }

            resultingHtml.AppendLine("</ul>");
            return new MvcHtmlString(resultingHtml.ToString());
        }


        public static MvcHtmlString CheckBoxListForEnum<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, IList<TProperty>>> property)
        {
            var valueGetter = property.Compile();
            
            var value = valueGetter(helper.ViewData.Model);

            var propertyInfo = ReflectionExtensions.GetPropertyInfo(property);
            var resultingHtml = new StringBuilder();
            
            var listTypeDefinition = propertyInfo.PropertyType;
            var enumTypeDefinition = listTypeDefinition.GetGenericArguments()[0];

            var allEnumValues = Enum.GetValues(enumTypeDefinition);

            resultingHtml.AppendLine("<ul class=\"checkList\">");

            foreach (var enumValue in allEnumValues)
            {
                resultingHtml.AppendLine("<li>");
                var description = ReflectionExtensions.GetEnumDescription((Enum)enumValue);
                var isChecked = value.Any(x => x.Equals(enumValue)) ? " checked=\"checked\"" : string.Empty;

                var inputId = propertyInfo.Name + "-" + enumValue;
                var inputHtml = string.Format("<input type=\"checkbox\"{2} id=\"{0}\" name=\"{1}\" value=\"{3}\" />", inputId, propertyInfo.Name, isChecked, enumValue);
                var labelHtml = string.Format("<label for=\"{0}\">{1}</label>", inputId, description);

                resultingHtml.AppendLine(inputHtml);
                resultingHtml.AppendLine(labelHtml);

                resultingHtml.AppendLine("</li>");
            }

            resultingHtml.AppendLine("</ul>");

            return new MvcHtmlString(resultingHtml.ToString());
        }

        public static MvcHtmlString ImageFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property, 
            string id = null, string placeHolderTheme = null, int width = 150, int height = 150)
        {
            return ImageFor(helper, property, id, false, placeHolderTheme, width, height);
        }

        public static MvcHtmlString ImageFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property)
        {
            return ImageFor(helper, property, null, null);
        }

        public static MvcHtmlString ImageFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property, 
            string id = null, bool circled = false, string placeHolderTheme = null, int width = 150, int height = 150)
        {
            var valueGetter = property.Compile();
            var value = valueGetter(helper.ViewData.Model);

            id = string.IsNullOrEmpty(id) ? helper.NameFor(property).ToString() : id;
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action("DownloadImage", "FileManaging", new { path = value });

            var @class = circled ? "class = 'img-circle'" : string.Empty;
            var theme = string.IsNullOrEmpty(placeHolderTheme) ? string.Empty : string.Format("/{0}", placeHolderTheme);
 
            var imgHtml = value == null ? string.Format("<img data-src='holder.js/{0}x{1}{4}' id={2} {3}>", width, height, id, @class, theme) : 
                                             string.Format("<img src='{0}' id={1} style = 'width:{2}px; height:{3}px' {4}>", url, id, width, height, @class);
            return new MvcHtmlString(imgHtml);
        }

        public static MvcHtmlString FluidImageFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property, int width, int height)
        {
            var valueGetter = property.Compile();
            var value = valueGetter(helper.ViewData.Model);

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action("DownloadImage", "FileManaging", new { path = value });

            var imgHtml = value == null ? string.Format("<img data-src='holder.js/{0}x{1}/auto/#f3e4ec:#CC3399/text:No pic available' class='img-responsive'>", width, height)
                                        : string.Format("<img src='{0}' style='width:{1}; height:{2};' class='img-responsive'>", url, width, height);

            return new MvcHtmlString(imgHtml);
        }

        public static MvcHtmlString DropDownListForEnum<TModel, TEnum>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TEnum>> property, bool includePleaseSelect)
        {
            var valueGetter = property.Compile();
            var value = valueGetter(helper.ViewData.Model) as Enum;

            var list = SelectListHelpers.BuildEnumOptions<TEnum>(value, null, includePleaseSelect);

            return helper.DropDownListFor(property, list, new { data_value = value });
        }

        public static MvcHtmlString StyledPinkSpanForString<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, string>> property)
        {
            var valueGetter = property.Compile();
            var text = valueGetter(helper.ViewData.Model);

            if (string.IsNullOrEmpty(text)) return null;

            var words = text.Split(new[] {' '});

            var greatestLengthIndex = 0;
            var tmp = 0;
            
            for (var i = 0; i < words.Length; i++)
            {
                var length = words[i].Length;

                if (length <= tmp) continue;
                
                greatestLengthIndex = i;
                tmp = length;
            }

            var match = words[greatestLengthIndex];
            var replacement = string.Format("<span class='pink'>{0}</span>", match);
            var result = text.Replace(match, replacement);

            return MvcHtmlString.Create(result);
        }
    }
}
