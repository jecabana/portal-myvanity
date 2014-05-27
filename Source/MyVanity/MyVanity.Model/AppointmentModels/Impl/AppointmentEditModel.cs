using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyVanity.Domain;
using MyVanity.Model.PlaceModels;

namespace MyVanity.Model.AppointmentModels.Impl
{
    public class AppointmentEditModel : ModelBase
    {
        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int? ProcedureId { get; set; }

        [Required(ErrorMessage = "You must select a procedure")]
        [Display(Name = "Procedure")]
        public string ProcedureIdentifier { get; set; }

        public string Status { get; set; }

        public AppointmentStatus StatusEnum { get; set; }

        public List<PlaceEditModel> Places { get; set; }

        [Display(Name = "Place")]
        public int SelectedPlace { get; set; }
    }
}
