using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace BookStore
{
    internal class PhysicalPathProvider : IFileProvider
    {
        private string v;

        public PhysicalPathProvider(string v)
        {
            this.v = v;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new System.NotImplementedException();
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            throw new System.NotImplementedException();
        }

        public IChangeToken Watch(string filter)
        {
            throw new System.NotImplementedException();
        }
    }
}