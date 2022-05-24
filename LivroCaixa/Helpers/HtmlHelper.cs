using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LivroCaixa.Helpers
{
    public static class SecuredValueHtmlHelper
    {
        public static string SecuredHiddenField(this HtmlHelper htmlHelper, string name, object value)
        {
            var html = new StringBuilder();
            html.Append(htmlHelper.Hidden(name, value));
            html.Append(GetHashFieldHtml(htmlHelper, name, GetValueAsString(value)));
            return html.ToString();
        }

        public static MvcHtmlString HashField(this HtmlHelper htmlHelper, string name, object value)
        {
            return GetHashFieldHtml(htmlHelper, name, GetValueAsString(value));
        }

        public static MvcHtmlString MultipleFieldHashField(this HtmlHelper htmlHelper, string name, IEnumerable values)
        {
            var valueToHash = new StringBuilder();
            foreach (var v in values)
            {
                valueToHash.Append(v);
            }

            return HashField(htmlHelper, name, valueToHash);
        }

        private static string GetValueAsString(object value)
        {
            return Convert.ToString(value, CultureInfo.CurrentCulture);
        }

        private static MvcHtmlString GetHashFieldHtml(HtmlHelper htmlHelper, string name, string value)
        {
            return htmlHelper.Hidden(SecuredValueFieldNameComputer.GetSecuredValueFieldName(name),
                                     SecuredValueHashComputer.GetHash(value));
        }
    }

    public static class SecuredValueFieldNameComputer
    {
        private const string NameSuffix = "_sha1";

        public static string GetSecuredValueFieldName(string name)
        {
            return name + NameSuffix;
        }
    }

    public interface IHashComputer
    {
        string GetBase64HashString(string value, string secret);
    }

    public class SHA1HashComputer : IHashComputer
    {
        public string GetBase64HashString(string value, string secret)
        {
            // create an array of bytes that contains the value and the secret
            var bytes = Encoding.UTF8.GetBytes(value + secret);
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var hash = sha1.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    public static class SecuredValueHashComputer
    {
        public static string Secret { get; set; }
        public static IHashComputer HashComputer { get; set; }

        static SecuredValueHashComputer()
        {
            Secret = "zomg!";
            HashComputer = new SHA1HashComputer();
        }

        public static string GetHash(string value)
        {
            return HashComputer.GetBase64HashString(value, Secret);
        }
    }

    public static class SecuredValueValidator
    {
        public static void ValidateValue(string value, string hash)
        {
            var computedHash = SecuredValueHashComputer.GetHash(value);
            if (computedHash != hash)
            {
                throw new SecurityException("Field tampering detected.");
            }
        }

        public static void ValidateValue(FormCollection formValues, string name)
        {
            var value = formValues[name];
            var hash = formValues[SecuredValueFieldNameComputer.GetSecuredValueFieldName(name)];
            ValidateValue(value, hash);
        }

        public static void ValidateMultipleValues(FormCollection formValues, string name, IEnumerable<string> names)
        {
            var valueToHash = new StringBuilder();
            foreach (var n in names)
            {
                valueToHash.Append(formValues[n]);
            }
            ValidateValue(valueToHash.ToString(),
                          formValues[SecuredValueFieldNameComputer.GetSecuredValueFieldName(name)]);
        }
    }
}