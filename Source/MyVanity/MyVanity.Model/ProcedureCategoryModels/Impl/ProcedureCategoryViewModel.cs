using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace MyVanity.Model.ProcedureCategoryModels.Impl
{
    public class ProcedureCategoryViewModel : ModelBase
    {
        [Required]
        public string Name { get; set; }
    }

    public class ProcedureCategoryIndexModel
    {
        public ProcedureCategoryIndexModel(IEnumerable<ProcedureCategoryViewModel> categories)
        {
            Categories = categories;
        }

        public IEnumerable<ProcedureCategoryViewModel> Categories { get; set; }
    }
}
