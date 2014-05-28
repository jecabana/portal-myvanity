using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MyVanity.Model.AgentModels.Impl;
using MyVanity.Model.ConsentFormModels.Impl;
using MyVanity.Model.DoctorModels.Impl;
using MyVanity.Model.FileModels.Impl;
using MyVanity.Model.PlaceModels;
using MyVanity.Model.ProcedureModels.Impl;

namespace MyVanity.Model.PatientProcedureModels.Impl
{
    public class PatientProcedureEditModel : ModelBase
    {
        [Required]
        public int? PatientId { get; set; }

        [Display(Name = "Patient Name")]
        [Required]
        public string PatientName { get; set; }

        public List<ProcedureConsentViewModel> SelectedConsents { get; set; }

        public List<ConsentFormEditModel> Consents { get; set; }

        [Display(Name = "Consent")]
        public int? SelectedConsent { get; set; }

        [Display(Name = "Doctor")]
        public int? SelectedDoctor { get; set; }

        [Required(ErrorMessage = "You must at least one doctor")]
        [Display(Name = "Added Doctors")]
        public List<int> SelectedDoctors { get; set; }

        public List<DoctorEditModel> Doctors { get; set; }

        public List<DoctorEditModel> AddedDoctors
        {
            get
            {
                return SelectedDoctors == null ?
                                          null : SelectedDoctors.Select(doctor => Doctors.SingleOrDefault(x => x.Id == doctor)).ToList();
            }
        }

        public string FormattedAddedDoctors
        {
            get { return string.Join(" | ", AddedDoctors.Select(x => x.FullName)); }
        }

        [Display(Name = "Procedure")]
        public int SelectedProcedure { get; set; }

        public List<ProcedureEditModel> Procedures { get; set; }

        public ProcedureEditModel Procedure
        {
            get { return Procedures.SingleOrDefault(x => x.Id == SelectedProcedure); }
        }        

        public string Detail
        {
            get { return string.Format("{0} {1}", Procedure.Category.Name, Procedure.Type.Name); }
        }

        /* Procedure Agents */
        public List<AgentEditModel> FinancialCoordinators { get; set; }

        [Display(Name = "Financial Coordinator")]
        public int SelectedFinancialCoordinator { get; set; }

        public AgentEditModel FinancialCoordinator
        {
            get
            {
                return FinancialCoordinators.SingleOrDefault(x => x.Id == SelectedFinancialCoordinator);
            }
        }

        public List<AgentEditModel> HomeAwayAssistants { get; set; }

        [Display(Name = "Home Away Coordinator")]
        public int? SelectedHomeAwayAssistant { get; set; }

        public AgentEditModel HomeAwayAssistant 
        {
            get
            {
                return SelectedHomeAwayAssistant == null
                    ? null
                    : HomeAwayAssistants.SingleOrDefault(x => x.Id == SelectedHomeAwayAssistant.Value);
            }
        }

        public List<AgentEditModel> MedicalAssistants { get; set; }

        [Display(Name = "Medical Assistant")]
        public int SelectedMedicalAssistant { get; set; }

        public AgentEditModel MedicalAssistant
        {
            get
            {
                return MedicalAssistants.SingleOrDefault(x => x.Id == SelectedMedicalAssistant);
            }
        }

        public AgentEditModel SurgicalCoordinator { get; set; }

        [Display(Name = "File")]
        public List<int> SelectedFiles { get; set; }

        public List<FileEditModel> AddedFiles { get; set; }

        public List<FileEditModel> PatientDocuments { get; set; }

        public List<PlaceEditModel> Places { get; set; }

        [Display(Name = "Place")]
        public int SelectedPlace { get; set; }

        public PlaceEditModel Place 
        {
            get { return Places.SingleOrDefault(x => x.Id == SelectedPlace); }
        }

        public override string ToString()
        {
            return string.Format("{0} ({1}, {2})", PatientName, Procedure.Category.Name, Procedure.Type.Name);
        }
    }
}




