using System.ComponentModel.DataAnnotations;

namespace MyVanity.Model.ProcedureTypeModels.Impl
{
    public class ProcedureTypeEditModel : ModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
