using System.Web;
using RazorEngine.Templating;

namespace MyVanity.Services.MailServices
{
    public class MailTemplate<T> : HtmlTemplateBase<T>
    {
        public string MailFooter 
        {
            get
            {
                var imgSrc = HttpContext.Current.Server.MapPath("~/Content/images/logo.png");
                var imgTag = string.Format("<img src='{0}' />", imgSrc);

                const string address = @"<address>
                                    <strong>MyVanity</strong> <br/>
                                    Office hours M-F 9-5 EST <br/>
                                    Phone: 1.800.340.4095 <br/>
                                    In case of any issue, please visit us here http://vanitymiami.com/survey 
                                </address>";

                return string.Format("{0} <br/> {1}", imgTag, address);
            }
        }
    }
}
