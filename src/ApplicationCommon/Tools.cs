using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ApplicationCommon
{
	public static class Tools
	{
		public static string NewFileName(string file)
		{
			return Guid.NewGuid() + Path.GetExtension(file);
		}

		private const int MaxLengthSlug = 45;
		public static string EncodeUrl(this string title)
		{
			var slug = RemoveAccent(title).ToLower();
			slug = Regex.Replace(slug, @"[^a-z0-9-\u0600-\u06FF]", "-");
			slug = Regex.Replace(slug, @"\s+", "-").Trim();
			slug = Regex.Replace(slug, @"-+", "-");
			return slug.Substring(0, slug.Length <= MaxLengthSlug ? slug.Length : MaxLengthSlug).Trim();
		}

		private static string RemoveAccent(string text)
		{
			var bytes = Encoding.GetEncoding("UTF-8").GetBytes(text);
			return Encoding.UTF8.GetString(bytes);
		}

		public static string DecodeUrl(this string value)
		{
			value = value.Replace("-", " ");
			return value;
		}

		public static string ToLatinNum(this string value)
		{
			return Regex.Replace(
				value,
				@"\d+",
				m => string.Join("", m.Groups[0].Value.Select(x => Convert.ToChar(x - 1728)))
				);
		}

		public static string RemoveAllHtmlTags(this string text, int maxChar)
		{
			var withoutHtml = string.IsNullOrEmpty(text) ?
						  string.Empty :
						  Regex.Replace(text, "<(.|\\n)*?>", string.Empty);

			withoutHtml = withoutHtml.Replace("&nbsp;", " ").Replace("&zwnj;", " ").Replace("&quot;", " ").Replace("amp;", "");
			withoutHtml = withoutHtml.Replace("&laquo;", "«");
			withoutHtml = withoutHtml.Replace("&raquo;", "»");
			return withoutHtml.Length < maxChar ? withoutHtml : withoutHtml.Substring(0, maxChar);
			//return withoutHtml;
		}

		public static string ConvertToMinSec(this int second)
		{
			var min = second / 60;
			var sec = second - min * 60;
			return $"{min}:{sec}";
		}

		public static T GetObjectValue<T>(this object item, string propName)
		{
			var pi = item.GetType().GetProperty(propName);
			return (T)pi.GetValue(item, null);
		}

		public static bool HasValue(this string value)
		{
			return !string.IsNullOrEmpty(value);
		}

		public static string DescriptionAttr<T>(this T source)
		{
			if (source == null)
				return "---";

			var fi = source.GetType().GetField(source.ToString());
			var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
				typeof(DescriptionAttribute), false);
			return attributes.Length > 0 ? attributes[0].Description : source.ToString();
		}

		public static string GetLast(this string source, int length)
		{
			if (source == null || length >= source.Length)
				return source;
			return source.Substring(source.Length - length);
		}
		public static string GetFirst(this string source, int length)
		{
			if (source == null || length >= source.Length)
				return source;
			return source.Substring(0, length);
		}
	}
}