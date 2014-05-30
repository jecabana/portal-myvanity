using System.IO;
using System.Threading.Tasks;
using MyVanity.Common.Autofac;

namespace MyVanity.Services.Blobs
{
    public interface IBlobStore : IPerRequestDependency
    {
        Task<byte[]> FindAsync(string container, string path);

        Task<FileWrapper> FindFileAsync(string container, string path);

        Task<bool> SaveAsync(string container, string path, byte[] content);

        Task<bool> SaveAsync(string container, FileWrapper file);

        bool Save(string container, FileWrapper file);

        Task<bool> SaveAsync(string container, string path, Stream stream);

        Task DeleteAsync(string container, string path);
    }

    public class FileWrapper
    {
        public Stream Stream { get; set; }

        public string ContentType { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }
    }
}
