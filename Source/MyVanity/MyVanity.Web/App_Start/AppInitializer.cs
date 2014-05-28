using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyVanity.Domain;
using MyVanity.Domain.Repositories.Enums;
using MyVanity.Domain.UoW;
using MyVanity.Services.Membership;

namespace MyVanity.Web
{
    public static class AppInitializer
    {
        public static async void CreateAdminUser()
        {
            var membershipService = DependencyResolver.Current.GetService<IMembershipService>();

            var roles = new List<ApplicationRole>{ ApplicationRole.Admin };
            await membershipService.CreateAsync(new Admin { UserName = "jecabana" }, roles, "admin123");
            await membershipService.CreateAsync(new Admin { UserName = "danielcondemarin" }, roles, "haystack");
        }

        public static void InitializeData()
        {
            var unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>();
            
            //DEFAULTS
            var catRepository = unitOfWork.GetRepository<ProcedureCategory>();
            var typeRepository = unitOfWork.GetRepository<ProcedureType>();

            foreach (var cat in Enum.GetValues(typeof(ProcedureCategories)))
            {
                var category = cat.ToString();
                var existing = catRepository.Get(x => x.Name.Equals(category));
                
                if (existing == null || !existing.Any())
                    catRepository.Insert(new ProcedureCategory(){ Name = category });
            }

            foreach (var typ in Enum.GetValues(typeof(ProcedureTypes)))
            {
                var type = typ.ToString();
                var existing = typeRepository.Get(x => x.Name.Equals(type));

                if (existing == null || !existing.Any())
                    typeRepository.Insert(new ProcedureType() { Name = type });
            }

            //System configuration values
            var sysRepository = unitOfWork.GetRepository<SystemConfiguration>();
            
            if (!sysRepository.Get(x => x.Name == "overrideEmail").Any())
                sysRepository.Insert(new SystemConfiguration{ Name = "overrideEmail", Value = "2" });

            if (sysRepository.Get(x => x.Name == "OverrideEmailAddress").SingleOrDefault() == null)
                sysRepository.Insert(new SystemConfiguration { Name = "OverrideEmailAddress", Value = "danielconde9@gmail.com;jecabana@yahoo.es" });

            //Document Category & Subcategory Defaults
            var docCategoryRepository = unitOfWork.GetRepository<DocumentCategory>();

            if (!docCategoryRepository.Get().Any())
            {
                docCategoryRepository.Insert(new DocumentCategory { Name = "Medical" });
                docCategoryRepository.Insert(new DocumentCategory { Name = "Non Medical" });
                docCategoryRepository.Insert(new DocumentCategory { Name = "Protocol" });

                unitOfWork.SaveChanges();
            }
            
            var docSubcategoryRepository = unitOfWork.GetRepository<DocumentSubcategory>();
            
            var medicalCat = docCategoryRepository.Get(x => x.Name == "Medical").Single();
            var nonMedical = docCategoryRepository.Get(x => x.Name == "Non Medical").Single();
            //var protocol = docCategoryRepository.Get(x => x.Name == "Protocol").Single();

            if (!docSubcategoryRepository.Get().Any())
            {
                docSubcategoryRepository.Insert(new DocumentSubcategory { Name = "Lab Results", Category = medicalCat });
                docSubcategoryRepository.Insert(new DocumentSubcategory { Name = "Doctor Evaluations", Category = medicalCat });
                docSubcategoryRepository.Insert(new DocumentSubcategory { Name = "Consent for Procedures", Category = nonMedical });
                docSubcategoryRepository.Insert(new DocumentSubcategory { Name = "IDs & Driver Licences", Category = nonMedical });
                docSubcategoryRepository.Insert(new DocumentSubcategory { Name = "Insurance Card", Category = nonMedical });
                docSubcategoryRepository.Insert(new DocumentSubcategory { Name = "Follow Ups", Category = medicalCat });
                docSubcategoryRepository.Insert(new DocumentSubcategory { Name = "Receipts & Applications", Category = nonMedical });
                docSubcategoryRepository.Insert(new DocumentSubcategory { Name = "Arbitration Agreement", Category = nonMedical });
                docSubcategoryRepository.Insert(new DocumentSubcategory { Name = "Patient Information", Category = nonMedical });
                docSubcategoryRepository.Insert(new DocumentSubcategory { Name = "Cancellation Charges", Category = nonMedical });
                docSubcategoryRepository.Insert(new DocumentSubcategory { Name = "Independent Contractor Agreement", Category = nonMedical });
                docSubcategoryRepository.Insert(new DocumentSubcategory { Name = "Miscellaneous", Category = nonMedical });
                docSubcategoryRepository.Insert(new DocumentSubcategory { Name = "Patient Medical History", Category = medicalCat });
            }

            var placeRepository = unitOfWork.GetRepository<Place>();
            if (!placeRepository.Get().Any())
            {
                placeRepository.Insert(new Place{ Name = "Hialeah"});
                placeRepository.Insert(new Place { Name = "Miami" });
                placeRepository.Insert(new Place { Name = "Broward" });    
            }

            unitOfWork.SaveChangesAsync();
        }
    }
}