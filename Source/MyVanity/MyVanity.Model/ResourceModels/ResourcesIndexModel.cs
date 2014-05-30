using System.Collections.Generic;
using MyVanity.Model.FileModels.Impl;

namespace MyVanity.Model.ResourceModels
{
    public class ResourcesIndexModel
    {
        public IEnumerable<FileEditModel> Files { get; set; }

        public ResourcesIndexModel(IEnumerable<FileEditModel> files)
        {
            Files = files;
        }
    }
}
