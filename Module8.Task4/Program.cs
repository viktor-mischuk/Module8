using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    public class Program
    {
        private static FileInfo fi;
        private static string folderPath;
        static void Main(string[] args)
        {
            //Рабочий стол текущего ПК
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            //Создание па РС
            folderPath = Path.Combine(desktopPath, "Students");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            Console.Write("Введите путь к файлу данных: ");
            var dataFilePath = Console.ReadLine();
            var studentsData = ReadData(folderPath, dataFilePath);
            if (studentsData == null)
            {
                Console.WriteLine("Данные не получены. Выход - любая клавиша");
                Console.ReadKey();
                return;
            }

            WriteDataToFile(studentsData);
        }
        /// <summary>
        /// Десериализация данных
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="dataFilePath"></param>
        /// <returns></returns>
        static Student[] ReadData(string folderPath, string dataFilePath)
        {

            if (dataFilePath == null || dataFilePath == String.Empty || !File.Exists(dataFilePath))
            {
                return null;
            }

            BinaryFormatter formatter = new BinaryFormatter();
            // десериализация
            using (var fs = new FileStream(dataFilePath, FileMode.OpenOrCreate))
            {
                Student[] students = (Student[])formatter.Deserialize(fs);
                return students;


            }
        }
        /// <summary>
        /// Запись студентов в файлы по группам
        /// </summary>
        /// <param name="students"></param>
        static void WriteDataToFile(Student[] students)
        {
            foreach (var student in students)
            {
                fi = new FileInfo($"{folderPath}\\{student.Group}.txt");
                if (!File.Exists(fi.FullName))
                {
                    using (StreamWriter sw = new StreamWriter(fi.FullName))
                    {
                        sw.WriteLine($"Имя: {student.Name} , Дата рождения: {student.DateOfBirth.ToShortDateString()}");
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(fi.FullName, true))
                    {
                        sw.WriteLine($"Имя: {student.Name} , Дата рождения: {student.DateOfBirth.ToShortDateString()}");
                    }
                }
            }


        }
    }
}
