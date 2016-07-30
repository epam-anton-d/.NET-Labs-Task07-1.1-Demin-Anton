using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualFileSystem
{
    internal interface IVirtualFilesystem
    {
        int Create(string locationPath, List<Files> fileList, List<Folders> folderList);
        int Copy(string sourcePath, string destinationPath, List<Files> fileList, List<Folders> folderList);
        int Move(string sourcePath, string destinationPath, List<Files> fileList, List<Folders> folderList);
        int Delete(string sourcePath, List<Files> fileList, List<Folders> folderList);
        //int Tree();
    }
}
