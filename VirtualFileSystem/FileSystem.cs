using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualFileSystem
{
    internal abstract class FileSystem : IVirtualFilesystem
    {
        public string Name { set; get; }
        public string LocationFolder { set; get; }

        public abstract int Create(string locationPath, List<Files> fileList, List<Folders> folderList);
        public abstract int Copy(string sourcePath, string destinationPath, List<Files> fileList, List<Folders> folderList);
        public abstract int Move(string sourcePath, string destinationPath, List<Files> fileList, List<Folders> folderList);
        public abstract int Delete(string sourcePath, List<Files> fileList, List<Folders> folderList);
        //public abstract int Dir();
    }
}
