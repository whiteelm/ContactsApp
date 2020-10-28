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
        public Project TempProject { get; set; }

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
            surnameTextBox.Text = TempProject.Contacts[0].Surname;
            nameTextBox.Text = TempProject.Contacts[0].Name;
            idVkTextBox.Text = TempProject.Contacts[0].IdVk;
            emailTextBox.Text = TempProject.Contacts[0].Email;
            phoneTextBox.Text = TempProject.Contacts[0].PhoneNumber.Number.ToString();
            birthDatePicker.Value = TempProject.Contacts[0].BirthDate;
            Text = @"Edit Contact";
        }

        /// <summary>
        /// Создание контакта для добавления или редактирования.
        /// </summary>
        private void OkButtonClick(object sender, EventArgs e)
        {
            try
            {
                var contact = new Contact();
                CheckEmptyCorrect();
                contact.Surname = surnameTextBox.Text;
                contact.Name = nameTextBox.Text;
                contact.IdVk = idVkTextBox.Text;
                contact.Email = emailTextBox.Text;
                contact.BirthDate = birthDatePicker.Value;
                var phoneNumber = new PhoneNumber
                {
                    Number = Convert.ToInt64(phoneTextBox.Text)
                };
                contact.PhoneNumber = phoneNumber;

                TempProject.Contacts.Insert(0, contact);
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Проверка имени, фамилии и номера на заполненность и верность заполнения.
        /// </summary>
        public void CheckEmptyCorrect()
        {
            if (surnameTextBox.Text == "" || nameTextBox.Text == "" || phoneTextBox.Text == "")
            {
                throw new ArgumentException(message: "Check the completion of all fields!");
            }

            if (surnameTextBox.BackColor == Color.Crimson || nameTextBox.BackColor == Color.Crimson || phoneTextBox.BackColor == Color.Crimson)
            {
                throw new ArgumentException(message: "Check the input is correct!");
            }
        }

        /// <summary>
        /// Отмена добавления/редактирования контакта.
        /// </summary>
        private void CancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Проверка фамилии на ввод.
        /// </summary>
        private void SurnameCheck(object sender, EventArgs e)
        {
            Checker(surnameTextBox);
        }

        /// <summary>
        /// Проверка имени на ввод.
        /// </summary>
        private void NameCheck(object sender, EventArgs e)
        {
            Checker(nameTextBox);
        }

        /// <summary>
        /// Проверка текста на отсутствие знаков кроме букв и "-", которое может быть только внутри слова.
        /// </summary>
        private static void Checker(Control textBox)
        {
            textBox.BackColor = !Regex.IsMatch(textBox.Text, "^[а-яА-Я]+(-[а-яА-Я]+)?$") ? Color.Crimson : Color.White;
        }

        /// <summary>
        /// Проверка номера на ввод.
        /// </summary>
        private void PhoneCheck(object sender, EventArgs e)
        {
            phoneTextBox.BackColor = !Regex.IsMatch(phoneTextBox.Text, "^7[0-9]{0,10}$") ? Color.Crimson : Color.White;
        }

        /// <summary>
        /// Проверка емейла на ввод.
        /// </summary>
        private void EmailCheck(object sender, EventArgs e)
        {
            emailTextBox.BackColor = Regex.IsMatch(emailTextBox.Text, "^[А-Яа-я]") ? Color.Crimson : Color.White;
        }

        /// <summary>
        /// Проверка айди вк на ввод.
        /// </summary>
        private void IdVkCheck(object sender, EventArgs e)
        {
            idVkTextBox.BackColor = Regex.IsMatch(idVkTextBox.Text, "^[А-Яа-я]") ? Color.Crimson : Color.White;
        }
    }
}
