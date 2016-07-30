using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace VirtualFileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Folders> folderList = new List<Folders>();
            List<Files> fileList = new List<Files>();
            
            string read;
            string patternMkdir = "mkdir";
            string patternPathFolder = @"\sc\:(\\[A-Za-z\-_0-9^\.]+)+";
            string patternPathFile = @"\sc\:(\\[A-Za-z\-_0-9]+)+\.[a-z0-9]+";
            string patternCreate = "create";
            string patternCopy = "copy";
            string patternDelete = "delete";
            string patternMove = "move";
            string patternExit = "exit";
            string semicolon = ";";
            int error;
            string[] commands;

            char[] space = new char[] { ' ' };
            try
            {
                // Чтение папок из файла Folders.txt.
                StreamReader fl1 = File.OpenText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Folders.txt"));
                string readFoldersTxt = fl1.ReadToEnd();
                fl1.Close();
                // Заполнение коллекции папок.
                String[] folderElements = Regex.Split(readFoldersTxt, semicolon);
                for (int i = 0; i < folderElements.Length; i++)
                {
                    if ((i + 1) % 2 == 0)
                    {
                        folderList.Add(new Folders(folderElements[i - 1], folderElements[i]));
                    }
                }
            }
            catch
            {
                Console.WriteLine("Fileread exeption");
            }

            try
            {
                // Чтение файлов из файла Files.txt.
                StreamReader fl2 = File.OpenText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files.txt"));
                string readFilesTxt = fl2.ReadToEnd();
                fl2.Close();

                // Заполнение коллекции файлов.
                String[] fileElements = Regex.Split(readFilesTxt, semicolon);
                for (int i = 0; i < fileElements.Length; i++)
                {
                    if ((i + 1) % 2 == 0)
                    {
                        fileList.Add(new Files(fileElements[i - 1], fileElements[i]));
                    }
                }
            }
            catch
            {
                Console.WriteLine("Fileread exeption");
            }

            do
            {
                Console.Write((folderList[0].Name) + "\\>");
                read = Console.ReadLine();
                read = read.Trim();

                if (Regex.IsMatch(read, patternMkdir + patternPathFolder))
                {
                    commands = read.Split(space);
                    error = folderList[0].Create(commands[1], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternCreate + patternPathFile))
                {
                    commands = read.Split(space);
                    error = fileList[0].Create(commands[1], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternCopy + patternPathFile + patternPathFolder))
                {
                    commands = read.Split(space);
                    error = fileList[0].Copy(commands[1], commands[2], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternCopy + patternPathFolder + patternPathFolder))
                {
                    commands = read.Split(space);
                    error = folderList[0].Copy(commands[1], commands[2], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternDelete + patternPathFile))
                {
                    commands = read.Split(space);
                    error = fileList[0].Delete(commands[1], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternDelete + patternPathFolder))
                {
                    commands = read.Split(space);
                    error = folderList[0].Delete(commands[1], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }                
                else if (Regex.IsMatch(read, patternMove + patternPathFile + patternPathFolder))
                {
                    commands = read.Split(space);
                    error = fileList[0].Move(commands[1], commands[2], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternMove + patternPathFolder + patternPathFolder))
                {
                    commands = read.Split(space);
                    error = folderList[0].Move(commands[1], commands[2], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (read == patternExit)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\"{0}\" не является внутренней или внешней\nкомандой, исполняемой программой или внешним файлом", read);
                }
            } while (read != patternExit);

            // Запись в Folders.txt.
            FileInfo f3 = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Folders.txt"));
            StreamWriter w1 = f3.CreateText();
            foreach (Folders item in folderList)
            {
                w1.Write("{0};", item.Name);
                w1.Write("{0};", item.LocationFolder);
            }
            w1.Close();

            // Запись в Files.txt.
            FileInfo f4 = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files.txt"));
            StreamWriter w2 = f4.CreateText();
            foreach (Files item in fileList)
            {
                w2.Write("{0};", item.Name);
                w2.Write("{0};", item.LocationFolder);
            }
            w2.Close();
        }
    }
}
