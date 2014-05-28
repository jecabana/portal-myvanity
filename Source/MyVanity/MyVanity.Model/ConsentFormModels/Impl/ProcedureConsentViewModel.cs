namespace MyVanity.Model.ConsentFormModels.Impl
{
    public class ProcedureConsentViewModel : ModelBase
    {
        public int ConsentId { get; set; }

        public bool Signed { get; set; }

        public int ProcedureId { get; set; }

        public string ConsentTitle { get; set; }

        public string Body { get; set; }
    }
}
