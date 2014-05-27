using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model.ProcedureModels;
using MyVanity.Model.ProcedureModels.Impl;

namespace MyVanity.Views.Repositories.ProceduresViewsRepository.Impl
{
    public class ProcedureViewRepository : ViewRepository<Procedure,ProcedureEditModel>
    {
        public ProcedureViewRepository(IProcedureModelConverter modelConverter, IUnitOfWork unitOfWork) : base(modelConverter, unitOfWork)
        {
        }
    }
}
