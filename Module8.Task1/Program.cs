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
                    Console.WriteLine("Папка не существует"); continue;
                }
                if (DefTimeLastUse(directoryInfo))
                {
                    DeleteDir(directoryInfo);
                    Console.WriteLine("Папка по укзанному пути удалена"); inputBool = false;
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Со времени последнего доступа прошло менее 30 мин");
                    Console.Write("Выйти из программы? Y/N :");
                    var answer = Console.ReadKey().Key;
                    if(answer == ConsoleKey.Y) inputBool = false;
                    Console.WriteLine();
                }
            }
        }
        /// <summary>
        /// Определение количества времени, прошедшее с последней активности
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        private static bool DefTimeLastUse(DirectoryInfo directoryInfo)
        {
            return DateTime.Now.Subtract(TimeSpan.FromMinutes(30)) > directoryInfo.LastAccessTime;
        }
        /// <summary>
        /// Удаление папки со всем содержимым
        /// </summary>
        /// <param name="directoryInfo"></param>
        private static void DeleteDir(DirectoryInfo directoryInfo)
        {
            try
            {
                directoryInfo.Delete(true);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Отсутствует доступ");
            }
            catch (Exception)
            {
                Console.WriteLine("Произошла ошибка");
            }
        }
    }
}
