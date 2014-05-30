using System.IO;
using MyVanity.Common.Autofac;

namespace MyVanity.Services.DocumentParser
{
    public interface IDocumentParser : IPerRequestDependency
    {
        string ParseDocument(Stream stream);
    }
}
