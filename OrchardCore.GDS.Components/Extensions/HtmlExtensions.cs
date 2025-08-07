using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Extensions
{
    public static class HtmlExtensions
    {
        public static string StartLabelRapperTag(this IHtmlHelper html, string tagName, string cssClass = "")
        {
            string classAttr = cssClass.Length > 0 ? @$"class=""{cssClass}""" : "";
            
            return $"<{tagName} {classAttr}>";
        }

        public static string EndLabelRapperTag(this IHtmlHelper html, string tagName)
        {
            return $"</{tagName}>";
        }

        public static string StartWithCharacterCount(this IHtmlHelper html, bool withCount, bool withCountWords, int count, string id)
        {
            if (withCount)
            {
                if (withCountWords)
                {
                    return $@"<div id=""{id}"" class=""govuk-character-count"" data-module=""govuk-character-count"" data-maxwords=""{count}"">";
                }
                else
                {
                    return $@"<div id=""{id}"" class=""govuk-character-count"" data-module=""govuk-character-count"" data-maxlength=""{count}"">";
                }
            }

            return "";
        }

        public static string EndWithCharacterCount(this IHtmlHelper html, bool withCount, bool withCountWords, int count, string fieldId)
        {
            if(withCount)
            {
                if(withCountWords)
                {
                    return $@"<div id=""{fieldId}-info"" class=""govuk-hint govuk-character-count__message"">You can enter up to {count} words</div></div>";
                }
                else
                {
                    return $@"<div id=""{fieldId}-info"" class=""govuk-hint govuk-character-count__message"">You can enter up to {count} characters</div></div>";
                }
            }

            return "";
        }

        public static string SelectOptionTag(this IHtmlHelper html, string value, string label, string selected)
        {
            return $@"<option value=""{value}"" {selected}>{label}</option>";
        }

        public static string BuildJsAttributes(this IHtmlHelper html, GdsJsPart part) 
        {
            string result = string.Empty;

            if (part != null)
            {
                if(!string.IsNullOrEmpty(part.OnChange))
                    result += part.OnChange.Length > 0 ? $@" onchange=""{part.OnChange}""" : "";

                if (!string.IsNullOrEmpty(part.OnClick))
                    result += part.OnClick.Length > 0 ? $@" onclick=""{part.OnClick}""" : "";

                if (!string.IsNullOrEmpty(part.OnMouseOver))
                    result += part.OnMouseOver.Length > 0 ? $@" onmouseover=""{part.OnMouseOver}""" : "";

                if (!string.IsNullOrEmpty(part.OnMouseOut))
                    result += part.OnMouseOut.Length > 0 ? $@" onmouseout=""{part.OnMouseOut}""" : "";

                if (!string.IsNullOrEmpty(part.OnKeyDown))
                    result += part.OnKeyDown.Length > 0 ? $@" onkeydown=""{part.OnKeyDown}""" : "";

                if (!string.IsNullOrEmpty(part.OnLoad))
                    result += part.OnLoad.Length > 0 ? $@" onload=""{part.OnLoad}""" : "";
            }

            return result;
        }

        public static string BuildCssAttributes(this IHtmlHelper html, GdsCssPart part, string additionalCssClass = "")
        {
            string result = string.Empty;

            if (part != null)
            {
                if (!string.IsNullOrEmpty(part.CssClass))
                    result += additionalCssClass.Length > 0 || part.CssClass.Length > 0 ? $@" class=""{additionalCssClass} {part.CssClass}""" : "";
                else
                    result += additionalCssClass.Length > 0 ? $@" class=""{additionalCssClass}""" : "";

                if (!string.IsNullOrEmpty(part.CssStyle))
                    result += part.CssStyle.Length > 0 ? $@" style=""{part.CssStyle}""" : "";
            }
            else
            {
                result += additionalCssClass.Length > 0 ? $@" class=""{additionalCssClass}""" : "";
            }

            return result;
        }

        public static string DisplayFileSize(this IHtmlHelper html, long fileSize)
        {
            long size = Convert.ToInt64(fileSize);
            long sizeInKB = Math.Abs(size / 1024);
            long sizeInMB = Math.Abs(sizeInKB / 1024);

            if (sizeInMB > 0)
            {
                return $"{sizeInMB} MB";
            }
            else if (sizeInKB > 0)
            {
                return $"{sizeInKB} KB";
            }
            else if (fileSize != 0)
            {
                return $"{size} bytes";
            }

            return "";
        }
    }
}
