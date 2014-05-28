using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using MyVanity.Domain;
using MyVanity.Model.DocumentCategoryModels.Impl;

namespace MyVanity.Model.FileModels.Impl
{
    public class FileEditModelSync : ModelBase
    {
        public string Description { get; set; }

        public string Path { get; set; }

        [Display(Name = "Category")]
        public int SelectedCategory { get; set; }

        public List<CategoryEditModel> Categories { get; set; }

        public CategoryEditModel Category { get; set; }

        [Display(Name = "Subcategory")]
        public int? SelectedSubcategory { get; set; }

        public List<CategoryEditModel> Subcategories { get; set; }

        public CategoryEditModel Subcategory { get; set; }

        public bool Censured { get; set; }

        [Required]
        public string RealName { get; set; }

        [Required]
        public string Name { get; set; }

        public ContentType? ContentType { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}
