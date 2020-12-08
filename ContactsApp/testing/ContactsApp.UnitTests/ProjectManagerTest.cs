using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ContactsApp.UnitTests
{
    [TestFixture]
    internal class ProjectManagerTest
    {
        public Project PrepareProject()
        {
            var sourceProject = new Project();
            var phoneNumber = new PhoneNumber
            {
                Number = 79996665544
            };
            sourceProject.Contacts.Add(new Contact()
            {
                Name = "BName",
                Surname = "BSurname",
                BirthDate = DateTime.Parse("03-06-2012"),
                IdVk = "B434234",
                Email = "Bhogger@bk.com",
                PhoneNumber = phoneNumber
            });
            sourceProject.Contacts.Add(new Contact()
            {
                Name = "CName",
                Surname = "CSurname",
                BirthDate = DateTime.Parse("03-06-2012"),
                IdVk = "C434234",
                Email = "Chogger@bk.com",
                PhoneNumber = phoneNumber
            });
            sourceProject.Contacts.Add(new Contact()
            {
                Name = "AName",
                Surname = "ASurname",
                BirthDate = DateTime.Parse("03-06-2012"),
                IdVk = "A434234",
                Email = "Ahogger@bk.com",
                PhoneNumber = phoneNumber
            });
            return sourceProject;
        }

        [Test]
        public void SaveToFile_CorrectProject_FileSavedCorrectly()
        {
            //Setup
            var sourceProject = PrepareProject();

            var location = Assembly.GetExecutingAssembly().Location;
            var testDataFolder = Path.GetDirectoryName(location) + @"\TestData";
            var actualFileName = testDataFolder + @"\actualProject.json"; 
            var expectedDataFolder = Path.GetDirectoryName(location) + @"\TestDataExpected";
            var expectedFileName = expectedDataFolder + @"\expectedProject.json";
            if (Directory.Exists(testDataFolder))
            {
                Directory.Delete(testDataFolder, true);
            }

            //Act
            ProjectManager.SaveToFile(sourceProject, actualFileName, testDataFolder);

            var isFileExist = File.Exists(actualFileName);
            Assert.AreEqual(true, isFileExist);

            //Assert
            var actualFileContent = File.ReadAllText(actualFileName);
            var expectedFileContent = File.ReadAllText(expectedFileName);
            Assert.AreEqual(expectedFileContent, actualFileContent);
        }

        [Test]
        public void LoadFromFile_CorrectProject_FileLoadedCorrectly()
        {
            //Setup
            var expectedProject = PrepareProject();

            var location = Assembly.GetExecutingAssembly().Location;
            var testDataFolder = Path.GetDirectoryName(location) + @"\TestDataExpected";
            var testFileName = testDataFolder + @"\expectedProject.json";

            //Act
            var actualProject = ProjectManager.LoadFromFile(testFileName);
            var expected = JsonConvert.SerializeObject(expectedProject);
            var actual = JsonConvert.SerializeObject(actualProject);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LoadFromFile_UnCorrectPath_ReturnEmptyProject()
        {
            //Setup
            var location = Assembly.GetExecutingAssembly().Location;
            var testDataFolder = Path.GetDirectoryName(location) + @"\Wrong";
            var testFileName = testDataFolder + @"\Wrong.json";

            //Act
            var actualProject = ProjectManager.LoadFromFile(testFileName);

            //Assert
            Assert.IsEmpty(actualProject.Contacts);
        }

        [Test]
        public void LoadFromFile_UnCorrectFile_ReturnEmptyProject()
        {
            //Setup
            var location = Assembly.GetExecutingAssembly().Location;
            var testDataFolder = Path.GetDirectoryName(location) + @"\TestDataExpected";
            var testFileName = testDataFolder + @"\defectiveProject.json";

            //Act
            var actualProject = ProjectManager.LoadFromFile(testFileName);

            //Assert
            Assert.IsEmpty(actualProject.Contacts);
        }

        [Test]
        public void FilePath_GoodFilePath_ReturnSamePath()
        {
            //Setup
            var expectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            expectedPath += @"\ContactsApp\Contacts.json";
            //Act
            var actualPath = ProjectManager.FilePath();

            //Assert
            Assert.AreEqual(expectedPath, actualPath);
        }

        [Test]
        public void DirectoryPath_GoodDirectoryPath_ReturnSameDirectory()
        {
            //Setup
            var expectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            expectedPath += @"\ContactsApp\";
            //Act
            var actualPath = ProjectManager.DirectoryPath();

            //Assert
            Assert.AreEqual(expectedPath, actualPath);
        }
    }
}
