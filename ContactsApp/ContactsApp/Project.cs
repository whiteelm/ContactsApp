﻿using System;
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

        /// <summary>
        /// Сортировка листа.
        /// </summary>
        /// <param name="sortProject"> Лист для сортировки.</param>
        /// <returns></returns>
        public static List<Contact> SortContacts(List<Contact> sortProject)
        {
            var sortedUsers = from u in sortProject orderby u.Surname select u;
            return sortedUsers.ToList();
        }

        /// <summary>
        /// Поиск контакта по строке.
        /// </summary>
        /// <param name="substringForSearch"> Строка по которой ведется поиск.</param>
        /// <param name="project"> Список контактов для поиска.</param>
        /// <returns></returns>
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
            var sortedUsers = SortContacts(findProject.Contacts);
            return sortedUsers.ToList();
        }

        /// <summary>
        /// Поиск контактов у которых день рождения.
        /// </summary>
        /// <param name="date"> Сегодняшний день.</param>
        /// <param name="project"> Контакты для поиска.</param>
        /// <returns></returns>
        public static List<Contact> BirthDayContacts(DateTime date, Project project)
        {
            var foundContacts = project.Contacts.Where(contact => 
                contact.BirthDate.Month == date.Month && contact.BirthDate.Day == date.Day);
            var birthDayContacts = new Project();
            foreach (var contact in foundContacts)
            {
                birthDayContacts.Contacts.Add(contact);
            }
            var sortedUsers = SortContacts(birthDayContacts.Contacts);
            return sortedUsers;
        }
    }
}