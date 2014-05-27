using Autofac;
using MyVanity.Domain;
using MyVanity.Model.AppointmentModels.Impl;
using MyVanity.Model.ConsentFormModels.Impl;
using MyVanity.Model.DoctorModels.Impl;
using MyVanity.Model.FileModels.Impl;
using MyVanity.Model.ProcedureCategoryModels.Impl;
using MyVanity.Model.ProcedureModels.Impl;
using MyVanity.Model.ProcedureTypeModels.Impl;
using MyVanity.Model.ResourceModels;
using MyVanity.Model.UserModels;
using MyVanity.Views.Repositories;
using MyVanity.Views.Repositories.UserViewsRepository;

namespace MyVanity.Views
{
    public class ViewsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ViewRepository<Doctor, DoctorEditModel>>().As<IViewRepository<DoctorEditModel>>();
            builder.RegisterType<ViewRepository<ProcedureCategory, ProcedureCategoryViewModel>>().As<IViewRepository<ProcedureCategoryViewModel>>();
            builder.RegisterType<ViewRepository<ProcedureType, ProcedureTypeEditModel>>().As<IViewRepository<ProcedureTypeEditModel>>();
            builder.RegisterType<ViewRepository<Procedure, ProcedureEditModel>>().As<IViewRepository<ProcedureEditModel>>();
            builder.RegisterType<ViewRepository<Appointment, AppointmentEditModel>>().As<IViewRepository<AppointmentEditModel>>();
            builder.RegisterType<UserViewRepository<User, UserViewModel>>().As<IUserViewRepository<UserViewModel>>();
            builder.RegisterType<ViewRepository<SharedDocument, FileEditModel>>().As<IViewRepository<FileEditModel>>();
            builder.RegisterType<ViewRepository<UserProcedurePatientDocument, UserProcedurePatientDocViewModel>>().As<IViewRepository<UserProcedurePatientDocViewModel>>();
            builder.RegisterType<ViewRepository<ConsentForm, ConsentFormEditModel>>().As<IViewRepository<ConsentFormEditModel>>();
        }
    }
}
