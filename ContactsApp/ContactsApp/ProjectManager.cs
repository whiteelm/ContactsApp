using System;
using System.IO;
using Newtonsoft.Json;

namespace ContactsApp
{
    /// <summary>
    /// Класс реализует метод для сохранения объекта «Проект» в файл и метод загрузки проекта из файла.
    /// Сохранение и загрузка осуществляются в один и тот же файл.
    /// </summary>
    public static class ProjectManager
    {

        /// <summary>
        /// Путь по умолчанию по которому сохраняется файл.
        /// </summary>
        public static string PathFile()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return path + @"\ContactsApp\Contacts.json";
        }

        /// <summary>
        /// Путь по умолчанию по которому создается папка для файла.
        /// </summary>
        public static string PathDirectory()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return path + @"\ContactsApp\";
        }

        /// <summary>
        /// Метод сериализации данных.
        /// </summary>
        /// <param name="data">Данные для сериализации.</param>
        /// <param name="filepath">Путь до файла</param>
        private static void Serializer(Project data, string filepath)
        {
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter(filepath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, data);
            }
        }
        /// <summary>
        /// Метод сохранения данных в файл.
        /// </summary>
        /// <param name="data">Данные для сериализации.</param>
        /// <param name="filepath">Путь до файла</param>
        public static void SaveToFile(Project data, string filepath)
        {
            try
            {
                Serializer(data, filepath);
            }
            catch
            {
                try
                {
                    Serializer(data, PathFile());
                }
                catch
                {
                    Directory.CreateDirectory(PathDirectory());
                    Serializer(data, PathFile());
                }
            }
        }

        /// <summary>
        /// Метод загрузки данных из файла.
        /// </summary>
        /// /// <param name="filepath">Путь до файла</param>
        public static Project LoadFromFile(string filepath)
        {
            Project project;
            if (!File.Exists(filepath))
            {
                return new Project();
            }
            var serializer = new JsonSerializer();
            try
            {
                using (var sr = new StreamReader(filepath))
                using (JsonReader reader = new JsonTextReader(sr))
                    project = serializer.Deserialize<Project>(reader);
            }
            catch
            {
                return new Project();
            }
            return project;
        }
    }
}