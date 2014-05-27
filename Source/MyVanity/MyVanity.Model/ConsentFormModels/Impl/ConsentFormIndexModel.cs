using System.Collections.Generic;

namespace MyVanity.Model.ConsentFormModels.Impl
{
    public class ConsentFormIndexModel
    {
        public IEnumerable<ConsentFormEditModel> Consents { get; set; }

        public ConsentFormIndexModel(IEnumerable<ConsentFormEditModel> consentForms)
        {
            Consents = consentForms;
        }
    }
}