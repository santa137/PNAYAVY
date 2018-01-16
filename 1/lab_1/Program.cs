using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab_1
{
    class Program
    {
        private static string komanda;
        const string PathZapisiFailov = "\\FileZapisiFailov.txt";

        static void Main(string[] args)
        {
            Program pr;
            pr = new Program();

            FileAndDirectoryVersion p;
            p = new FileAndDirectoryVersion();

            // Инициализация списков "fileList" и "katalogList".
            p.InitFileList();
            p.InitKatalogList();

            // Получение адреса последнего проинициализированного каталога.
            if (FileAndDirectoryVersion.fileList.Count != 0)
                FileAndDirectoryVersion.PathToActiveDirectory = FileAndDirectoryVersion.fileList[FileAndDirectoryVersion.fileList.Count - 1].Directory;
            else
                FileAndDirectoryVersion.PathToActiveDirectory = null;

            // Ожидание ввода команды.
            while (true)
            {
                komanda = Console.ReadLine();

                pr.OpredelenieKomand(komanda);
            }
        }

        ///////////////////////////////////////////////
        // Установка метки New и подкрашивание текста.
        //
        private void MetkaNewAddedICvet()
        {
            FileAndDirectoryVersion p;
            p = new FileAndDirectoryVersion();

            DirectoryInfo d = new DirectoryInfo(FileAndDirectoryVersion.PathToActiveDirectory); // Инициализация дирректории для отслеживания.

            foreach (FileInfo f in d.GetFiles("*.*"))
            {
                int FlagFileNew = 1;
                int flagFound2 = 0;// 0 - не удалось найти файл "remove", 1 - поиск удачно завершился.

                // Узнаем Имя, Размер, Время создания и изменения файла в АКТИВНОЙ дирктории.
                p.Name = f.Name;
                p.Size = Convert.ToString(f.Length);
                p.Created = Convert.ToString(f.CreationTime);
                p.Modified = Convert.ToString(f.LastWriteTime);

                // Ищем во временном списке "fileListTemp" есть ли файл с меткой "remove".
                for (int i = 0; i < FileAndDirectoryVersion.fileListTemp.Count; i++)
                {
                    if ((FileAndDirectoryVersion.fileListTemp[i].Name == p.Name) && (FileAndDirectoryVersion.fileListTemp[i].Note != "0") && (FileAndDirectoryVersion.fileListTemp[i].Directory == FileAndDirectoryVersion.PathToActiveDirectory))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen; // Устанавливаем цвет.

                        if(FileAndDirectoryVersion.fileListTemp[i].Note == "remove")
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("file: {0}<<-- {1}ed\n", p.Name, FileAndDirectoryVersion.fileListTemp[i].Note);
                        Console.Write("size: {0} byte\n", p.Size);
                        Console.Write("created: {0}\n", p.Created);
                        Console.Write("modified: {0}\n\n", p.Modified);
                        Console.ResetColor();

                        flagFound2 = 1;
                        break;
                    }
                }       
                if(flagFound2 == 1)
                {
                    // Переходим на следующую итерацию.
                    continue;
                }

                for (int i = 0; i < FileAndDirectoryVersion.fileList.Count; i++)
                {
                    // Заходим только в активную директорию в списке.
                    if (FileAndDirectoryVersion.fileList[i].Directory == FileAndDirectoryVersion.PathToActiveDirectory)
                    {
                        // Проверка на то что файл есть. 
                        // FlagFileNew в положение "0" и выход из цикла.
                        if (p.Name == FileAndDirectoryVersion.fileList[i].Name)
                        {
                            // Если изменился Size И Modified.
                            if ((p.Size != FileAndDirectoryVersion.fileList[i].Size) && (p.Modified != FileAndDirectoryVersion.fileList[i].Modified))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed; // Устанавливаем цвет.
                                Console.Write("file: {0}\n", p.Name);
                                Console.Write("size: {0} byte<<-- {1} byte\n", FileAndDirectoryVersion.fileList[i].Size, p.Size);
                                Console.Write("created: {0}\n", p.Created);
                                Console.Write("modified: {0}<<-- {1}\n\n", FileAndDirectoryVersion.fileList[i].Modified, p.Modified);
                            }
                            // Если изменилась ТОЛЬКО Modified.
                            else if (p.Modified != FileAndDirectoryVersion.fileList[i].Modified)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed; // Устанавливаем цвет.
                                Console.Write("file: {0}\n", p.Name);
                                Console.Write("size: {0} byte\n", p.Size);
                                Console.Write("created: {0}\n", p.Created);
                                Console.Write("modified: {0}<<-- {1}\n\n", FileAndDirectoryVersion.fileList[i].Modified, p.Modified);
                            }
                            // Если изменился ТОЛЬКО Size.
                            else if (p.Size != FileAndDirectoryVersion.fileList[i].Size)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed; // Устанавливаем цвет.
                                Console.Write("file: {0}\n", p.Name);
                                Console.Write("size: {0} byte<<-- {1} byte\n", FileAndDirectoryVersion.fileList[i].Size, p.Size);
                                Console.Write("created: {0}\n", p.Created);
                                Console.Write("modified: {0}\n\n", p.Modified);
                            }
                            else if ((p.Size == FileAndDirectoryVersion.fileList[i].Size) && (p.Modified == FileAndDirectoryVersion.fileList[i].Modified))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("file: {0}\n", FileAndDirectoryVersion.fileList[i].Name);
                                Console.Write("size: {0} byte\n", FileAndDirectoryVersion.fileList[i].Size);
                                Console.Write("created: {0}\n", FileAndDirectoryVersion.fileList[i].Created);
                                Console.Write("modified: {0}\n\n", FileAndDirectoryVersion.fileList[i].Modified);
                            }
                            Console.ResetColor();

                            FlagFileNew = 0; // Установка флага в положение что файл существует.
                            break;
                        }
                    }
                }
                if (FlagFileNew == 1)
                {
                    int flagFound = 0;// 0 - не удалось найти файл "added", 1 - поиск удачно завершился.

                    // Ищем во временном списке "fileListTemp" есть ли файл с меткой "add".
                    for (int i = 0; i < FileAndDirectoryVersion.fileListTemp.Count; i++)
                    {
                        if((FileAndDirectoryVersion.fileListTemp[i].Name == p.Name) && (FileAndDirectoryVersion.fileListTemp[i].Note == "add") && (FileAndDirectoryVersion.fileListTemp[i].Directory == FileAndDirectoryVersion.PathToActiveDirectory))
                        {
                            flagFound = 1;
                            break;
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    if (flagFound == 1)
                    {
                        Console.Write("file: {0}<<-- added\n", p.Name);
                        Console.Write("size: {0} byte\n", p.Size);
                        Console.Write("created: {0}\n", p.Created);
                        Console.Write("modified: {0}\n\n", p.Modified);
                    }
                    else
                    {
                        Console.Write("file: {0}<<-- new\n", p.Name);
                        Console.Write("size: {0} byte\n", p.Size);
                        Console.Write("created: {0}\n", p.Created);
                        Console.Write("modified: {0}\n\n", p.Modified);
                    }
                    Console.ResetColor();
                }
            }
        }

        //////////////////////////////////////////////
        // Установка метки Deleted.
        //
        private void MetkaDeleted()
        {
            FileAndDirectoryVersion p;
            p = new FileAndDirectoryVersion();

            DirectoryInfo d = new DirectoryInfo(FileAndDirectoryVersion.PathToActiveDirectory); // Инициализация дирректории для отслеживания.

            for (int i = 0; i < FileAndDirectoryVersion.fileList.Count; i++)
            {
                int FlagFileDeleted = 1;

                // Заходим только в активную директорию в списке.
                if (FileAndDirectoryVersion.fileList[i].Directory == FileAndDirectoryVersion.PathToActiveDirectory)
                {
                    foreach (FileInfo f in d.GetFiles("*.*"))
                    {
                        int flagFound = 0;// 0 - не удалось найти файл removed, 1 - поиск удачно завершился.

                        // Узнаем Имя, Размер, Время создания и изменения файла.
                        p.Name = f.Name;
                        p.Size = Convert.ToString(f.Length);
                        p.Created = Convert.ToString(f.CreationTime);
                        p.Modified = Convert.ToString(f.LastWriteTime);

                        // Проверка на то что файл есть. 
                        // FlagFileNew в положение "0" и выход из цикла.
                        if (FileAndDirectoryVersion.fileList[i].Name == p.Name)
                        {
                            FlagFileDeleted = 0; // Установка флага в положение что файл удалён.
                            break;
                        }
                    }
                    if (FlagFileDeleted == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("file: {0}<<-- deleted\n", FileAndDirectoryVersion.fileList[i].Name);
                        Console.Write("size: {0} byte\n", FileAndDirectoryVersion.fileList[i].Size);
                        Console.Write("created: {0}\n", FileAndDirectoryVersion.fileList[i].Created);
                        Console.Write("modified: {0}\n\n", FileAndDirectoryVersion.fileList[i].Modified);    
                        Console.ResetColor();
                    }
                }
            }
        }

        ////////////////////////////////////////////////
        // Работа с командами.

        //////////////////////////////////////////////
        // Определение команды.
        //
        public void OpredelenieKomand(string komanda)
        {
            String[] Podkomanda = komanda.Split(new[] { ' ' }, 2); // Разделяем строку на подкоманды.

            switch (Podkomanda[0])
            {
                case "init":
                    if (Podkomanda.Length == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("init <--Ошибка. Не введён путь к каталогу.\n");
                        Console.ResetColor();
                    }
                    else if (Podkomanda.Length == 2)
                    {
                        if (!Directory.Exists(Podkomanda[1]))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("init <--Ошибка. Каталог не существует.\n");
                            Console.ResetColor();
                        }
                        else
                        {
                            int flagZapisi = 1;
                            // Запоминаем путь к активному каталогу.
                            FileAndDirectoryVersion.PathToActiveDirectory = Podkomanda[1];

                            // Проверка директорий на инициализацию.
                            for (int i = 0; i < FileAndDirectoryVersion.katalogList.Count; i++)
                            {
                                if (FileAndDirectoryVersion.fileList[i].Directory == FileAndDirectoryVersion.PathToActiveDirectory)
                                    flagZapisi = 0;
                            }
                            if (flagZapisi == 1)
                            {
                                FileAndDirectoryVersion p;
                                p = new FileAndDirectoryVersion();

                                init();

                                Console.WriteLine("init <--Каталог: {0} проинициализирован!\n", FileAndDirectoryVersion.PathToActiveDirectory);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("init <--Ошибка. Каталог: {0} уже был проинициализирован!\n", FileAndDirectoryVersion.PathToActiveDirectory);
                                Console.ResetColor();
                            }
                        }
                    }
                    break;

                case "status":
                    if (Podkomanda.Length == 1)
                    {
                        if (FileAndDirectoryVersion.PathToActiveDirectory == null)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("status <--Ошибка. Ни один из каталогов не проинициализирован!\n");
                            Console.ResetColor();
                        }
                        else
                            status();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("status <--Ошибка. Неверный формат команды!\n");
                        Console.ResetColor();
                    }
                    break;

                case "add":
                    if (Podkomanda.Length == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("add <--Ошибка. Не введёно имя файла.\n");
                        Console.ResetColor();
                    }

                    else if (Podkomanda.Length == 2)
                    {
                        if (!File.Exists(FileAndDirectoryVersion.PathToActiveDirectory+"\\"+Podkomanda[1]))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("add <--Ошибка. Файл не существует.\n");
                            Console.ResetColor();

                        }
                        else
                        {
                            add(Podkomanda[1]);
                        }
                    }
                    break;

                case "remove":
                    if (Podkomanda.Length == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("remove <--Ошибка. Не введёно имя файла.\n");
                        Console.ResetColor();
                    }

                    else if (Podkomanda.Length == 2)
                    {
                        remove(Podkomanda[1]);
                    }
                    break;

                case "apply":
                    if (Podkomanda.Length == 1)
                    {
                        if (FileAndDirectoryVersion.PathToActiveDirectory == null)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("apply <--Ошибка. Ни один из каталогов не проинициализирован!\n");
                            Console.ResetColor();
                        }
                        else
                        {
                            apply();
                            Console.WriteLine("apply <--Все изменения сохранены в каталоге {0}!\n", FileAndDirectoryVersion.PathToActiveDirectory);
                        }
                    }
                    else if (Podkomanda.Length == 2)
                    {
                        if (!Directory.Exists(Podkomanda[1]))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("apply <--Ошибка. Каталог не существует.\n");
                            Console.ResetColor();

                        }
                        else
                        {
                            // Запоминаем путь к активному каталогу.
                            FileAndDirectoryVersion.PathToActiveDirectory = Podkomanda[1];

                            apply();
                            Console.WriteLine("apply <--Все изменения сохранены в каталоге {0}!\n", FileAndDirectoryVersion.PathToActiveDirectory);

                        }
                    }
                    break;

                case "listbranch":
                    if (Podkomanda.Length == 1)
                    {
                        if (FileAndDirectoryVersion.PathToActiveDirectory == null)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("listbranch <--Ошибка. Ни один каталог не проинициализирован!\n");
                            Console.ResetColor();
                        }
                        else
                            listbranch();
                    }
                    else
                        Console.WriteLine("listbranch <--Ошибка. Неверный формат команды!\n");
                    break;

                case "checkout":
                    if (Podkomanda.Length == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("checkout <--Ошибка. Не введён путь к каталогу или номер каталога.\n");
                        Console.ResetColor();
                    }
                    else if (Podkomanda.Length == 2)
                    {
                        try
                        {
                            checkout(Convert.ToInt32(Podkomanda[1]) - 1); // Пытаемся передать число.
                        }
                        catch // Не получается - передаём строку.
                        {
                            checkout(Podkomanda[1]);
                        }
                    }
                    break;

                case "exit":
                    Console.WriteLine("exit <--GoodBye!");
                    Console.Read();
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("<--oshibka!");
                    break;
            }
        }

        //////////////////////////////////////////////
        // Команда "status".
        //
        private void status()
        {
            // Вызов методов для проверки меток New(Проверки изменений в файлах) и Deleted.
            MetkaNewAddedICvet();
            MetkaDeleted();
        }

        //////////////////////////////////////////////
        // Команда "init".
        //
        private void init()
        {
            FileAndDirectoryVersion p = new FileAndDirectoryVersion();

            // Запись информации в файл о новой директории.
            p.InitDirectory();

            // Заполнение списков НОВЫМИ данными.
            p.InitFileList();
            p.InitKatalogList();
        }

        //////////////////////////////////////////////
        // Команда "apply".
        //
        private void apply()
        {
            FileAndDirectoryVersion p = new FileAndDirectoryVersion();

            // Запись НОВОЙ информации в файл о выбранной директории.
            p.ApplyFileZapisi();

            // Заполнение списков НОВЫМИ данными.
            p.InitFileList();
            p.InitKatalogList();
        }

        //////////////////////////////////////////////
        // Команда "listbranch".
        //
        private void listbranch()
        {
            FileAndDirectoryVersion p = new FileAndDirectoryVersion();

            // По завершению работы будет проинициализированный список "katalogList".
            p.InitKatalogList();

            Console.WriteLine("Список проинициализированных каталогов:");
            // Вывод проинициализированных каталогов из списка "katalogList".
            for (int i = 0; i < FileAndDirectoryVersion.katalogList.Count; i++)
            {
                Console.WriteLine("> {0}", FileAndDirectoryVersion.katalogList[i].Directory);
            }
            Console.WriteLine("");
        }

        //////////////////////////////////////////////
        // Команда "checkout" dir_path.
        //
        private void checkout(string dir_path)
        {
            int flagFound = 0; // 0 - не удалось найти, 1 - поиск удачно завершился.

            // Поиск по ИМЕНИ в указанной директории в списке проинициализированных каталогов.
            for (int i = 0; i < FileAndDirectoryVersion.katalogList.Count; i++)
            {
                if (FileAndDirectoryVersion.katalogList[i].Directory == @dir_path)
                {
                    FileAndDirectoryVersion.PathToActiveDirectory = @dir_path;
                    flagFound = 1;
                    break;
                }
            }
            if (flagFound == 1)
                Console.WriteLine("checkout <--Установлен активный каталог: {0}!\n", FileAndDirectoryVersion.PathToActiveDirectory);
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("checkout <--Ошибка. Каталог: {0} не найден среди проинициализированных!\n", dir_path);
                Console.ResetColor();
            }
        }

        //////////////////////////////////////////////
        // Команда "checkout" dir_num.
        //
        private void checkout(int dir_num)
        {
            int flagFound = 0; // 0 - не удалось найти, 1 - поиск удачно завершился.

            // Поиск по НОМЕРУ в указанной директории в списке "katalogList" проинициализированных каталогов.
            for (int i = 0; i < FileAndDirectoryVersion.katalogList.Count; i++)
            {
                if (i == dir_num)
                {
                    FileAndDirectoryVersion.PathToActiveDirectory = FileAndDirectoryVersion.katalogList[dir_num].Directory;
                    flagFound = 1;
                    break;
                }
            }
            if (flagFound == 1)
                Console.WriteLine("checkout <--Установлен активный каталог: {0}!\n", FileAndDirectoryVersion.PathToActiveDirectory);
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("checkout <--Ошибка. Каталог: {0} не найден среди проинициализированных!\n", dir_num + 1);
                Console.ResetColor();
            }
        }

        //////////////////////////////////////////////
        // Команда "add" file_path.
        //
        private void add(string file_name)
        {
            int flag = 0;
            // Поиск файла по ИМЕНИ в списке проинициализированных файлов.
            for (int i = 0; i < FileAndDirectoryVersion.fileListTemp.Count; i++)
            {
                if ((FileAndDirectoryVersion.fileListTemp[i].Name == file_name) && (FileAndDirectoryVersion.fileListTemp[i].Directory == FileAndDirectoryVersion.PathToActiveDirectory))
                {
                    FileAndDirectoryVersion.fileListTemp[i].Note = "add";
                    Console.WriteLine("add <--На файл: {0} добавлена метка add!\n", file_name);
                    flag = 1;
                    break;
                }
            }
            if (flag == 0)
            {
                DirectoryInfo d = new DirectoryInfo(FileAndDirectoryVersion.PathToActiveDirectory);
                foreach (FileInfo f in d.GetFiles(file_name))
                {
                    FileAndDirectoryVersion p;
                    p = new FileAndDirectoryVersion();

                    // Узнаем информацию о файле.
                    p.Name = f.Name;
                    p.Size = Convert.ToString(f.Length);
                    p.Created = Convert.ToString(f.CreationTime);
                    p.Modified = Convert.ToString(f.LastWriteTime);
                    p.Note = "add";
                    p.Directory = Convert.ToString(f.DirectoryName);

                    // Добавляем во временный список, который будет хранить метку add.
                    FileAndDirectoryVersion.fileListTemp.Add(p);
                    Console.WriteLine("add <--На файл: {0} добавлена метка add!\n", p.Name);
                }
            }
        }

        //////////////////////////////////////////////
        // Команда "remove" file_path.
        //
        private void remove(string file_name)
        {
            int flag = 0;
            // Поиск файла по ИМЕНИ в списке проинициализированных файлов.
            for (int i = 0; i < FileAndDirectoryVersion.fileListTemp.Count; i++)
            {
                if ((FileAndDirectoryVersion.fileListTemp[i].Name == file_name) && (FileAndDirectoryVersion.fileListTemp[i].Directory == FileAndDirectoryVersion.PathToActiveDirectory))
                {
                    FileAndDirectoryVersion.fileListTemp[i].Note = "remove";
                    Console.WriteLine("remove <--На файл: {0} добавлена метка remove!\n", file_name);
                    flag = 1;
                    break;
                }
            }

            if (flag == 0)
            {
                DirectoryInfo d = new DirectoryInfo(FileAndDirectoryVersion.PathToActiveDirectory);
                foreach (FileInfo f in d.GetFiles(file_name))
                {
                    FileAndDirectoryVersion p;
                    p = new FileAndDirectoryVersion();

                    // Узнаем информацию о файле.
                    p.Name = f.Name;
                    p.Size = Convert.ToString(f.Length);
                    p.Created = Convert.ToString(f.CreationTime);
                    p.Modified = Convert.ToString(f.LastWriteTime);
                    p.Note = "remove";
                    p.Directory = Convert.ToString(f.DirectoryName);

                    Console.WriteLine("remove <--На файл: {0} добавлена метка remove!\n", p.Name);

                    // Добавляем во временный список, который будет хранить метку remove.
                    FileAndDirectoryVersion.fileListTemp.Add(p);
                }
            }
        }
    }
}

