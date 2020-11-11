using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactsApp
{
    /// <summary>
    /// Класс содержит список всех контактов.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Коллекция контактов.
        /// </summary>
        public List<Contact> Contacts = new List<Contact>();

        public static List<Contact> SortContacts(List<Contact> sortProject)
        {
            var sortedUsers = from u in sortProject orderby u.Surname select u;
            return sortedUsers.ToList();
        }

        public static List<Contact> SortContacts(string substringForSearch, Project project)
        {
            if (substringForSearch == "")
                return project.Contacts;
            var foundContacts = project.Contacts.Where(contact =>
                contact.Surname.StartsWith(substringForSearch, StringComparison.OrdinalIgnoreCase) ||
                contact.Name.StartsWith(substringForSearch, StringComparison.OrdinalIgnoreCase));
            var findProject = new Project();
            foreach (var contact in foundContacts)
            {
                findProject.Contacts.Add(contact);
            }
            var foundedList = Project.SortContacts(findProject.Contacts);
            var sortedUsers = SortContacts(foundedList);
            return sortedUsers.ToList();
        }
    }
}