using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LivroCaixa.Filters
{
    public class ValidateAntiModelInjectionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The name of the property we are generating a hash for.
        /// </summary>
        private readonly string[] _properties;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName">The name of the property from the form to validate against the hidden encrypted form version.</param>
        public ValidateAntiModelInjectionAttribute(params string[] properties)
        {
            foreach (var property in properties) {
                if (string.IsNullOrEmpty(property))
                {
                    throw new ArgumentException("O valor propertyName deve ser uma string não vazia.");
                }
            }

            _properties = properties;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //The hidden form field that contains our hash - for ex. CustomerId is rendered as a hidden input  id="_CustomerIdToken"
            foreach (var _propertyName in _properties)
            {
                string encryptedPropertyName = string.Format("_{0}Token", _propertyName);

                //grab the token
                string hashToken = filterContext.HttpContext.Request.Form[encryptedPropertyName];

                //The encrypted form data MUST be there. We do not allow empty strings otherwise this could give
                //an attack vector in our filter as a means to bypass checks by simply passing in an empty validation token.
                if (string.IsNullOrEmpty(hashToken))
                {
                    throw new MissingFieldException(string.Format("O campo de formulário oculto nomeado valor {0} estava faltando. Isto é criado pelos métodos Html.AntiModelInjection. Verifique se o nome usado em seu [ValidateAntiModelInjectionAttribute (\"!AQUI!\")] Corresponde ao nome do campo utilizado no método Html.AntiModelInjection. Se este atributo é utilizado em um método de controlador que se entende por HttpGet, então o valor forma que ainda não existe. Este atributo é para ser utilizado em métodos do controlador acessados via HttpPost.", encryptedPropertyName));
                }


                //Get the plain text value
                string formValue = filterContext.HttpContext.Request.Form[_propertyName];

                //Plain text must be available to compare.
                if (string.IsNullOrEmpty(formValue))
                {
                    throw new MissingFieldException(string.Format("O valor de {0} estava faltando. Se este atributo é utilizado em um método de controlador que se entende por HttpGet, então o valor forma que ainda não existe. Este atributo é para ser utilizado em métodos do controlador acessados via HttpPost.", _propertyName));
                }

                //Now hash the 'plain text' version so we can compare to the hash originally created by Html.AntiModelInjectionFor
                string hashedFormValue = FormsAuthentication.HashPasswordForStoringInConfigFile(formValue, "SHA1");

                //And compare
                if (string.Compare(hashedFormValue, hashToken, false, CultureInfo.InvariantCulture) != 0)
                {
                    throw new HttpAntiModelInjectionException(string.Format("Validação de segurança falhou para {0}. É possível que os dados foram alterados como o valor original utilizado para criar o campo de formulário não coincide com o valor da propriedade corrente para este campo.", _propertyName));
                }

                filterContext.HttpContext.Trace.Write("(Logging Filter)Action Executing: " +
                filterContext.ActionDescriptor.ActionName);

                base.OnActionExecuting(filterContext);
            }
        }
    }

    public class HttpAntiModelInjectionException: Exception
    {
        public HttpAntiModelInjectionException(string message)
        {

        }
    }
}