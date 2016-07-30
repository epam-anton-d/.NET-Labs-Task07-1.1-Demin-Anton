using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualFileSystem
{
    internal class Files : FileSystem
    {
        public Files(string name, string locationFolder)
        {
            this.Name = name;
            this.LocationFolder = locationFolder;
        }

        //private bool IsExist(string locationPath, List<Folders> folderList)
        //{
        //    foreach (Folders item in folderList)
        //    {
        //        if (locationPath == item.LocationFolder + "\\" + item.Name || locationPath == "c:")
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        // works.
        //private bool IsExistTwice(string nameFile, string nameFolder, List<Files> fileList, List<Folders> folderList)
        //{
        //    foreach (Files item in fileList)
        //    {
        //        if (nameFolder + "\\" + nameFile == item.LocationFolder + "\\" + item.Name)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public override int Create(string locationPath, List<Files> fileList, List<Folders> folderList)
        {
            char[] slash = new char[] { '\\' };
            string[] folderDir = locationPath.Split(slash);
            string path = "";
            for (int i = 0; i < folderDir.Length - 1; i++)
            {
                if (folderDir[i] != "")
                    path += folderDir[i];
                if (i != folderDir.Length - 2)
                    path += "\\";
            }
            string endPath = folderDir[folderDir.Length - 1];

            if (!folderList.Exists(x => x.LocationFolder + "\\" + x.Name == path) && path != "c:")
	        {
	        	 return 1; // Несуществующий путь.
	        }

            //if (!IsExist(path, folderList))
            //{
            //    return 1; // Несуществующий путь.
            //}

            if (fileList.Exists(x => x.LocationFolder + "\\" + x.Name == path + "\\" + endPath))
	        {
	        	 return 2;
	        }

            //if (IsExistTwice(endPath, path, fileList, folderList))
            //{
            //    return 2; // Файл в заданной директории уже существует.
            //}

            try
            {
                fileList.Add(new Files(endPath, path));//folderDir[folderDir.Length - 2]));
                return 0;
            }
            catch
            {
                return 3; // Неизвестная ошибка.
            }
        }

        public override int Copy(string sourcePath, string destinationPath, List<Files> fileList, List<Folders> folderList)
        {
            char[] slash = new char[] { '\\' };

            string[] folderDir = sourcePath.Split(slash);
            string path = "";
            for (int i = 0; i < folderDir.Length - 1; i++)
            {
                path += folderDir[i];
                if (i != folderDir.Length - 2)
                    path += "\\";
            }
            string endPath = folderDir[folderDir.Length - 1];

            if (!fileList.Exists(x => x.LocationFolder + "\\" + x.Name == path + "\\" + endPath))
            {
                return 4; // Несуществующий путь.
            }

            //if (!IsExistTwice(endPath, path, fileList, folderList))
            //{
            //    return 4; // Несуществующий путь.
            //}

            if (!folderList.Exists(x => x.LocationFolder + "\\" + x.Name == destinationPath))
            {
                return 5; // Несуществующий путь.
            }

            //if (!IsExist(destinationPath, folderList))
            //{
            //    return 5; // Несуществующий путь.
            //}

            try
            {
                fileList.Add(new Files(endPath, destinationPath));
                //folderList.Remove((Folder){endPath,surcePath});
                //parts.Remove(new Part(){PartId=1534, PartName="cogs"})
                return 0;
            }
            catch
            {
                return 3; // Неизвестная ошибка.
            }
        }

        public override int Move(string sourcePath, string destinationPath, List<Files> fileList, List<Folders> folderList)
        {
            try
            {
                Copy(sourcePath, destinationPath, fileList, folderList);
            }
            catch
            {
                return 6; // Ошибка копирования.
            }
            try
            {
                Delete(sourcePath, fileList, folderList);
            }
            catch
            {
                return 7; // Ошибка удаления.
            }
            return 0;
        }

        // Delete.
        public override int Delete(string sourcePath, List<Files> fileList, List<Folders> folderList)
        {
            char[] slash = new char[] { '\\' };

            string[] folderDir = sourcePath.Split(slash);
            string path = "";
            for (int i = 0; i < folderDir.Length - 1; i++)
            {
                path += folderDir[i];
                if (i != folderDir.Length - 2)
                    path += "\\";
            }
            string endPath = folderDir[folderDir.Length - 1];

            if (!fileList.Exists(x => x.LocationFolder + "\\" + x.Name == path + "\\" + endPath))
            {
                return 4; // Несуществующий путь.
            }
            try
            {
                fileList.RemoveAll(x => x.LocationFolder == path && x.Name == endPath);
            }
            catch
            {
                return 5; // Не удалось удалить папку по неизвесной причине.
            }

            return 0;
        }
    }
}
