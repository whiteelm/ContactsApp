using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ContactsApp.UnitTests
{
    [TestFixture]
    public class ProjectTest
    {
        public List<Contact> PrepareList()
        {
            var sourceProject = new List<Contact>();
            var phoneNumber = new PhoneNumber
            {
                Number = 79996665544
            };
            sourceProject.Add(new Contact
            {
                Name = "BName",
                Surname = "BSurname",
                BirthDate = DateTime.Now,
                IdVk = "B434234",
                Email = "Bhogger@bk.com",
                PhoneNumber = phoneNumber
            });
            sourceProject.Add(new Contact
            {
                Name = "CName",
                Surname = "CSurname",
                BirthDate = DateTime.Now,
                IdVk = "C434234",
                Email = "Chogger@bk.com",
                PhoneNumber = phoneNumber
            });
            sourceProject.Add(new Contact
            {
                Name = "AName",
                Surname = "ASurname",
                BirthDate = DateTime.Now,
                IdVk = "A434234",
                Email = "Ahogger@bk.com",
                PhoneNumber = phoneNumber
            });
            return sourceProject;
        }

        [Test]
        public void SortContacts_GoodSort_ReturnSortedContacts()
        {
            //Setup
            var sourceContacts = PrepareList();
            var expectedContacts = sourceContacts.Select(contact => 
                contact.Clone() as Contact).ToList();
            expectedContacts = (from u in expectedContacts orderby u.Surname select u).ToList();

            //Act
            var actualContacts = sourceContacts;
            actualContacts = actualContacts.SortContacts(sourceContacts);
         
            //Assert
            Assert.AreEqual(expectedContacts, actualContacts);
        }

        [Test]
        public void SortContacts_GoodSortAndFind_ReturnSortedContacts()
        {
            //Setup
            var sourceProject = PrepareList();
            var expectedContacts = new List<Contact> {sourceProject[0].Clone() as Contact};

            //Act
            var actualProject = sourceProject.SortContacts("BName", sourceProject);

            //Assert
            Assert.AreEqual(expectedContacts, actualProject);
        }

        [Test]
        public void SortContacts_SortAndFindWithEmptyString_ReturnSortedContacts()
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
                BirthDate = DateTime.Now,
                IdVk = "B434234",
                Email = "Bhogger@bk.com",
                PhoneNumber = phoneNumber
            });
            sourceProject.Contacts.Add(new Contact()
            {
                Name = "CName",
                Surname = "CSurname",
                BirthDate = DateTime.Now,
                IdVk = "C434234",
                Email = "Chogger@bk.com",
                PhoneNumber = phoneNumber
            });
            sourceProject.Contacts.Add(new Contact()
            {
                Name = "AName",
                Surname = "ASurname",
                BirthDate = DateTime.Now,
                IdVk = "A434234",
                Email = "Ahogger@bk.com",
                PhoneNumber = phoneNumber
            });
            var expectedProject = new Project();
            foreach (var contact in sourceProject.Contacts)
            {
                expectedProject.Contacts.Add(contact.Clone() as Contact);
            }

            //Act
            var actualProject = sourceProject.SortContacts("", sourceProject.Contacts);
            var expected = JsonConvert.SerializeObject(expectedProject);
            var actual = JsonConvert.SerializeObject(actualProject);

            //Assert
            Assert.AreEqual(expectedProject, actualProject);
        }

        [Test]
        public void BirthDayContactsFind_GoodBirthDayContactsFind_ReturnSortedContacts()
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
                BirthDate = DateTime.Now,
                IdVk = "B434234",
                Email = "Bhogger@bk.com",
                PhoneNumber = phoneNumber
            });
            sourceProject.Contacts.Add(new Contact()
            {
                Name = "CName",
                Surname = "CSurname",
                BirthDate = DateTime.Now,
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
            var expectedProject = new Project();
            expectedProject.Contacts.Add(sourceProject.Contacts[0].Clone() as Contact);
            expectedProject.Contacts.Add(sourceProject.Contacts[1].Clone() as Contact);

            //Act
            var actualProject = new Project
            {
                Contacts = sourceProject.FindBirthDayContacts(DateTime.Now, sourceProject.Contacts)
            };
            var expected = JsonConvert.SerializeObject(expectedProject);
            var actual = JsonConvert.SerializeObject(actualProject);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
