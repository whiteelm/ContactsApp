using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ContactsApp.UnitTests
{
    [TestFixture]
    class ProjectManagerTest
    {
        [Test]
        public void SaveToFile_CorrectProject_FileSavedCorrectly()
        {
            //Setup
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

            var location = Assembly.GetExecutingAssembly().Location;
            var testDataFolder = Path.GetDirectoryName(location) + @"\TestData";
            var actualFileName = testDataFolder + @"\actualProject.json";
            var expectedFileName = testDataFolder + @"\expectedProject.json";

            //Act
            ProjectManager.SaveToFile(sourceProject, actualFileName, testDataFolder);

            //Assert
            var actualFileContent = File.ReadAllText(actualFileName);
            var expectedFileContent = File.ReadAllText(expectedFileName);
            NUnit.Framework.Assert.AreEqual(expectedFileContent, actualFileContent);
        }

        //[Test]
        //public void SaveToFile_EmptyProject_FileSavedCorrectly()
        //{
        //    //Setup
        //    var sourceProject = new Project();

        //    var location = Assembly.GetExecutingAssembly().Location;
        //    var testDataFolder = Path.GetDirectoryName(location) + @"\TestEmptyData";
        //    var actualFileName = testDataFolder + @"\actualProject.json";
        //    var expectedFileName = testDataFolder + @"\expectedProject.json";

        //    //Act
        //    ProjectManager.SaveToFile(sourceProject, actualFileName, testDataFolder);

        //    //Assert
        //    var actualFileContent = File.ReadAllText(actualFileName);
        //    var expectedFileContent = File.ReadAllText(expectedFileName);
        //    Assert.AreEqual(expectedFileContent, actualFileContent);
        //}

        //[Test]
        //public void SaveToFile_DirectoryExist_CreateNewDirectory()
        //{
        //    //Setup
        //    var sourceProject = new Project();
        //    var location = Assembly.GetExecutingAssembly().Location;
        //    var testDataFolder = Path.GetDirectoryName(location) + @"\DirectoryExistTest";
        //    var actualFileName = testDataFolder + @"\actualProject.json";

        //    //Act
        //    ProjectManager.SaveToFile(sourceProject, actualFileName, testDataFolder);

        //    //Assert
        //    Assert.IsTrue(Directory.Exists(testDataFolder));
        //}

        [Test]
        public void LoadFromFile_CorrectProject_FileLoadedCorrectly()
        {
            //Setup
            var expectedProject = new Project();
            var phoneNumber = new PhoneNumber
            {
                Number = 79996665544
            };
            expectedProject.Contacts.Add(new Contact()
            {
                Name = "BName",
                Surname = "BSurname",
                BirthDate = DateTime.Parse("03-06-2012"),
                IdVk = "B434234",
                Email = "Bhogger@bk.com",
                PhoneNumber = phoneNumber
            });
            var location = Assembly.GetExecutingAssembly().Location;
            var testDataFolder = Path.GetDirectoryName(location) + @"\TestData";
            var testFileName = testDataFolder + @"\actualProject.json";
            ProjectManager.SaveToFile(expectedProject, testFileName, testDataFolder);

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
            Assert.IsTrue(actualProject.Contacts.Count == 0);
        }

        [Test]
        public void LoadFromFile_UnCorrectFile_ReturnEmptyProject()
        {
            //Setup
            var location = Assembly.GetExecutingAssembly().Location;
            var testDataFolder = Path.GetDirectoryName(location) + @"\TestData";
            var testFileName = testDataFolder + @"\Wrong.json";

            //Act
            var actualProject = ProjectManager.LoadFromFile(testFileName);
      
            //Assert
            Assert.IsTrue(actualProject.Contacts.Count == 0);
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
        public void DirectoryPath_DirectoryPath_ReturnSameDirectory()
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
