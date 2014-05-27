using MyVanity.Common.Autofac;

namespace MyVanity.Services.Files
{
    public interface IFileService : IPerRequestDependency
    {
        void ChangeFileCensure(int fileId, bool censure);
    }
}
