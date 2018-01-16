using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab_1
{
    class FileAndDirectoryVersion
    {
        const string PathZapisiFailov = "\\FileZapisiFailov.txt";

        public string Name { get; set; }

        public string Size { get; set; }

        public string Created {get; set;}

        public string Modified { get; set; }

        public string Note { get; set; }

        public string Directory { get; set; }

        public static string PathToActiveDirectory { get; set; } // Путь к активному каталогу.
        public static List<FileAndDirectoryVersion> katalogList = new List<FileAndDirectoryVersion>();
        public static List<FileAndDirectoryVersion> fileList = new List<FileAndDirectoryVersion>(); // Список проинициализированных файлов.
        public static List<FileAndDirectoryVersion> fileListTemp = new List<FileAndDirectoryVersion>(); // Временный список.

        // Запись в файл.
        //
        private void WriteInFile(FileAndDirectoryVersion p)
        {
            FileStream file = new FileStream(PathZapisiFailov, FileMode.Append);
            StreamWriter Writer = new StreamWriter(file);

            Writer.WriteLine("{0}", p.Name);
            Writer.WriteLine("{0}", p.Size);
            Writer.WriteLine("{0}", p.Created);
            Writer.WriteLine("{0}", p.Modified);
            Writer.WriteLine("{0}", p.Note);
            Writer.WriteLine("{0}", p.Directory);

            Writer.Close();
        }

        // Запись информации о директории и входящих в неё данных в ФАЙЛ и в СПИСОК "fileList".
        // Для команды "init".
        public void InitDirectory()
        {

            DirectoryInfo d = new DirectoryInfo(FileAndDirectoryVersion.PathToActiveDirectory);

            foreach (FileInfo f in d.GetFiles("*.*"))
            {
                FileAndDirectoryVersion p;
                p = new FileAndDirectoryVersion();

                // Узнаем информацию о файле.
                p.Name = f.Name;
                p.Size = Convert.ToString(f.Length);
                p.Created = Convert.ToString(f.CreationTime);
                p.Modified = Convert.ToString(f.LastWriteTime);
                p.Note = "0";
                p.Directory = Convert.ToString(f.DirectoryName);

                // Запись в файл.
                WriteInFile(p);
            }
        }

        // Перезапись информации из СПИСКА "fileList" и из АКТИВНОЙ директории.
        // Для команды "apply".
        public void ApplyFileZapisi()
        {
            int flagZapisiNew = 1;

            // Очистка файла.
            FileStream file = new FileStream(PathZapisiFailov, FileMode.Create);
            StreamWriter Writer = new StreamWriter(file);
            Writer.Close();

            // Очистка меток временного списка для АКТИВНОЙ директории
            for (int i = 0; i < FileAndDirectoryVersion.fileListTemp.Count; i++)
            {
                if (FileAndDirectoryVersion.fileListTemp[i].Directory == FileAndDirectoryVersion.PathToActiveDirectory)
                {
                    if (FileAndDirectoryVersion.fileListTemp[i].Note != "remove")
                        FileAndDirectoryVersion.fileListTemp[i].Note = "0";
                    else
                        FileAndDirectoryVersion.fileListTemp[i].Note = "new";
                }
            }

            // Берем список "fileList".
            // Записываем НОВУЮ информацию о ТЕКУЩЕМ каталоге.
            // Записываем СТАРУЮ информацию об остальных ПРОИНИЦИАЛИЗИРОВАННЫХ каталогах из списка "fileList".
            DirectoryInfo d = new DirectoryInfo(FileAndDirectoryVersion.PathToActiveDirectory);

            for (int i = 0; i < FileAndDirectoryVersion.fileList.Count; i++)
            {

                FileAndDirectoryVersion pOld;
                pOld = new FileAndDirectoryVersion();

                // Узнаем информацию о файле.
                pOld.Name = FileAndDirectoryVersion.fileList[i].Name;
                pOld.Size = FileAndDirectoryVersion.fileList[i].Size;
                pOld.Created = FileAndDirectoryVersion.fileList[i].Created;
                pOld.Modified = FileAndDirectoryVersion.fileList[i].Modified;
                pOld.Note = FileAndDirectoryVersion.fileList[i].Note;
                pOld.Directory = FileAndDirectoryVersion.fileList[i].Directory;

                // Записываем НОВУЮ информацию о ТЕКУЩЕМ каталоге.
                if (flagZapisiNew == 1)
                {
                    foreach (FileInfo f in d.GetFiles())
                    {

                        FileAndDirectoryVersion pNew;
                        pNew = new FileAndDirectoryVersion();

                        // Узнаем НОВУЮ информацию о файле в текущей директории.
                        pNew.Name = f.Name;
                        pNew.Size = Convert.ToString(f.Length);
                        pNew.Created = Convert.ToString(f.CreationTime);
                        pNew.Modified = Convert.ToString(f.LastWriteTime);
                        pNew.Note = "0";
                        pNew.Directory = Convert.ToString(f.DirectoryName);

                        // Если директории совпадают, перезаписываем НОВУЮ информацию.
                        if (pOld.Directory == FileAndDirectoryVersion.PathToActiveDirectory)
                        {
                            // Запись в файл НОВОЙ инфы о директории.
                            WriteInFile(pNew);

                            flagZapisiNew = 0;
                        }
                        else
                            break;
                    }
                }
                // Записываем СТАРУЮ информацию об остальных ПРОИНИЦИАЛИЗИРОВАННЫХ каталогах.
                if (pOld.Directory != FileAndDirectoryVersion.PathToActiveDirectory)
                    WriteInFile(pOld);
            }
            // Очистка списка.
            fileList.Clear();
        }

        // Инициализация списка "katalogList".
        // Заполнение списка всеми проинициализированными каталогами.
        public void InitKatalogList()
        {
            // Очистка списка.
            katalogList.Clear();

            for (int i = 0; i < FileAndDirectoryVersion.fileList.Count; i++)
            {
                int flagZapisi = 1;
                FileAndDirectoryVersion p;
                p = new FileAndDirectoryVersion();

                p.Directory = FileAndDirectoryVersion.fileList[i].Directory;

                for (int j = 0; j < FileAndDirectoryVersion.katalogList.Count; j++)
                {
                    if (p.Directory == katalogList[j].Directory)
                    {
                        flagZapisi = 0;
                        break;
                    }                
                }
                // Добавление в список зхаписи происходит в том случае, если не было найдено директории.
                if (flagZapisi == 1)
                    katalogList.Add(p);
            }
        }

        // Инициализация списка "fileList".
        // Происходит для того чтобы заполнить список и узнать адрес последней проинициализированной дирректории.
        public void InitFileList()
        {
            // Очистка списка.
            fileList.Clear();

            // Обход файла для записи в список проинициализированных файлов.
            FileStream File = new FileStream(PathZapisiFailov, FileMode.Open);
            StreamReader Reader = new StreamReader(File);

            while (!Reader.EndOfStream) // Начальное состояние.
            {
                FileAndDirectoryVersion p;
                p = new FileAndDirectoryVersion();

                // Достаём строки и вынимаем характеристики(Name, Size, Created, Modified, Directory).
                string strokaName = Reader.ReadLine();
                string strokatSize = Reader.ReadLine();
                string strokaCreated = Reader.ReadLine();
                string strokaModified = Reader.ReadLine();
                string strokaNote = Reader.ReadLine();
                string strokaDirectory = Reader.ReadLine();

                p.Name = strokaName;
                p.Size = strokatSize;
                p.Created = strokaCreated;
                p.Modified = strokaModified;
                p.Note = strokaNote;
                p.Directory = strokaDirectory;

                // Заполнение списка.
                fileList.Add(p);
            }
            Reader.Close();
        }
    }
}
