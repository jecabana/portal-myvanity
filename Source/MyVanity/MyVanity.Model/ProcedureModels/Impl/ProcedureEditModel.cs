using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MyVanity.Model.ProcedureCategoryModels.Impl;
using MyVanity.Model.ProcedureTypeModels.Impl;

namespace MyVanity.Model.ProcedureModels.Impl
{
    public class ProcedureEditModel : ModelBase
    {
        [Display(Name = "Regular Price")]
        public double? RegularPrice { get; set; }

        [Display(Name = "Sale Price")]
        public double? SalePrice { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int SelectedCategoryId { get; set; }

        public ProcedureCategoryViewModel Category 
        {
            get { return Categories.SingleOrDefault(x => x.Id == SelectedCategoryId); }
        }

        public ProcedureTypeEditModel Type 
        {
            get
            {
                return Types.SingleOrDefault(x => x.Id == SelectedTypeId);
            }
        }

        [Display(Name = "Descriptive Image")]
        public string PicturePath { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int SelectedTypeId { get; set; }

        public List<ProcedureCategoryViewModel> Categories { get; set; }

        public List<ProcedureTypeEditModel> Types { get; set; }

        public string Name 
        {
            get
            {
                return string.Format("{0}, {1}", Category.Name, Type.Name);
            }
        }
    }
}
