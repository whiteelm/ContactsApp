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
        /// Отмена добавления/редактирования контакта.
        /// </summary>
        private void CancelButtonClick(object sender, EventArgs e)
        {
            if (surnameTextBox.Text != "" || surnameTextBox.Text != "" || phoneTextBox.Text != "" || emailTextBox.Text != "" || idVkTextBox.Text != "")
            {
                var dialogResult = MessageBox.Show(@"The entered data will not be saved.",
                    @"Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dialogResult == DialogResult.Yes)
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        /// <summary>
        /// Проверка данных на заполненность и верность заполнения.
        /// </summary>
        public void CheckEmptyCorrect()
        {
            if (surnameTextBox.BackColor == Color.LightCoral)
            {
                throw new ArgumentException(message: "Surname is not correct!");
            }
            if (nameTextBox.BackColor == Color.LightCoral)
            {
                throw new ArgumentException(message: "Name is not correct!");
            }
            if (phoneTextBox.BackColor == Color.LightCoral)
            {
                throw new ArgumentException(message: "Phone number is not correct!");
            }
            if (emailTextBox.BackColor == Color.LightCoral)
            {
                throw new ArgumentException(message: "E-mail is not correct");
            }
            if (idVkTextBox.BackColor == Color.LightCoral)
            {
                throw new ArgumentException(message: "ID_vk is not correct!");
            }
        }

        /// <summary>
        /// Проверка фамилии на ввод.
        /// </summary>
        private void SurnameCheck(object sender, EventArgs e)
        {
            const string pattern = "^[а-яА-Я]+(-[а-яА-Я]+)?$";
            Checker(surnameTextBox, pattern);
        }

        /// <summary>
        /// Проверка имени на ввод.
        /// </summary>
        private void NameCheck(object sender, EventArgs e)
        {
            const string pattern = "^[а-яА-Я]+(-[а-яА-Я]+)?$";
            Checker(nameTextBox, pattern);
        }
        
        /// <summary>
        /// Проверка номера на ввод.
        /// </summary>
        private  void PhoneCheck(object sender, EventArgs e)
        {
            const string pattern = "^7[0-9]{0,10}$";
            Checker(phoneTextBox, pattern);
        }

        /// <summary>
        /// Проверка емейла на ввод.
        /// </summary>
        private void EmailCheck(object sender, EventArgs e)
        {
            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Checker(emailTextBox, pattern);
        }

        /// <summary>
        /// Проверка айди вк на ввод.
        /// </summary>
        private void IdVkCheck(object sender, EventArgs e)
        {
            const string pattern = "^[a-zA-Z0-9]+(_[a-zA-Z0-9]+)?$";
            Checker(idVkTextBox, pattern);
        }

        /// <summary>
        /// Проверка текстбокса по паттерну.
        /// </summary>
        private static void Checker(Control textBox, string pattern)
        {
            textBox.BackColor = !Regex.IsMatch(textBox.Text, pattern) ? Color.LightCoral : Color.White;
        }
    }
}
