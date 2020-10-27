using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ContactsApp;

namespace ContactsAppUI
{
    /// <summary>
    /// Класс для добавления и редактирования контактов.
    /// </summary>
    public partial class AddEditContact : Form
    {
        /// <summary>
        /// Временный проект для добавления или редактирования контакта.
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// Переменная для проверки типа действия.
        /// </summary>
        public bool Check;

        public AddEditContact()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При редактировании контакта выдает данные.
        /// </summary>
        private void AddEditFormLoad(object sender, EventArgs e)
        { 
            if (!Check) return;
            surnameTextBox.Text = Project.Contacts[0].Surname;
            nameTextBox.Text = Project.Contacts[0].Name;
            idVkTextBox.Text = Project.Contacts[0].IdVk;
            emailTextBox.Text = Project.Contacts[0].Email;
            phoneTextBox.Text = Project.Contacts[0].PhoneNumber.Number.ToString();
            birthDatePicker.Value = Project.Contacts[0].BirthDate;
            Project.Contacts.RemoveAt(0);
        }

        /// <summary>
        /// Создание контакта для добавления или редактирования.
        /// </summary>
        private void OkButtonClick(object sender, EventArgs e)
        {
            var phoneNumber = new PhoneNumber
            {
                Number = Convert.ToInt64(phoneTextBox.Text)
            };
            var contact = new Contact
            {
                Surname = surnameTextBox.Text,
                Name = nameTextBox.Text,
                PhoneNumber = phoneNumber,
                IdVk = idVkTextBox.Text,
                Email = emailTextBox.Text,
                BirthDate = birthDatePicker.Value
            };
            Project.Contacts.Add(contact);
            Close();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void SurnameCheck(object sender, EventArgs e)
        {
            Checker(surnameTextBox);
        }

        private void NameCheck(object sender, EventArgs e)
        {
            Checker(nameTextBox);
        }

        private void Checker(Control textBox)
        {
            textBox.BackColor = !Regex.IsMatch(textBox.Text, "^[а-яА-Я]+(-[а-яА-Я]+)?$") ? Color.Firebrick : Color.White;
        }

        private void PhoneCheck(object sender, EventArgs e)
        {
            phoneTextBox.BackColor = !Regex.IsMatch(phoneTextBox.Text, "[0-9]") ? Color.Firebrick : Color.White;
        }
    }
}
