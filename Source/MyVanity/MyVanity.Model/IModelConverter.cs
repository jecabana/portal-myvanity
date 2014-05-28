using MyVanity.Common.Autofac;

namespace MyVanity.Model
{
    public interface IModelConverter<TEntity, TModel> : IEntityConverter<TEntity, TModel>, IViewModelConverter<TEntity, TModel> where TModel : ModelBase
    {
    }

    public interface IEntityConverter<in TEntity, out TModel> : IPerRequestDependency
    {
        TModel ConvertToModel(TEntity entity);
    }

    public interface IViewModelConverter<out TEntity, in TModel> : IPerRequestDependency
    {
        TEntity ConvertToSource(TModel model);
    }

    public interface IViewModelBuilder<TModel> : IPerRequestDependency
    {
        TModel BuildModel(TModel model);
    }
}
