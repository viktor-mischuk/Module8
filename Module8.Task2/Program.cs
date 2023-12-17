using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool inputBool = true;
            string path = null;
            DirectoryInfo directoryInfo = null;
            while (inputBool)
            {
                Console.Write("Введите путь к папке: ");
                path = Console.ReadLine();
                if (path is null)
                {
                    Console.WriteLine("Не указан путь"); continue;
                }
                try
                {
                    directoryInfo = new DirectoryInfo(path);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Не верно указан путь"); continue;
                }

                if (!directoryInfo.Exists)
                {
                    Console.WriteLine("Папка не существует");
                    Console.Write("Выйти из программы? Y/N :");
                    var answer = Console.ReadKey().Key;
                    if (answer == ConsoleKey.Y) inputBool = false;
                    Console.WriteLine(); 
                }
                var size = DefSize(directoryInfo);
                Console.WriteLine($"Размер папки {size} байт");
                Console.ReadKey(); inputBool = false;
            }
        }
        /// <summary>
        /// Подсчет размера папки в байтах
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        private static long DefSize(DirectoryInfo directoryInfo)
        {
            long size = 0;

            var folders = directoryInfo.GetDirectories();

            var files = directoryInfo.GetFiles();
                foreach(var file in files)
                {
                    size += file.Length;
                }
                foreach(var folder in folders) size += DefSize(folder);
   
            return size;
            
        }
    }
}
