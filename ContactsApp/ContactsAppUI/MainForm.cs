using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ContactsApp;

namespace ContactsAppUI
{
    /// <summary>
    /// Главное окно программы.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Путь к файлу.
        /// </summary>
        private readonly string _filePath = ProjectManager.FilePath();

        /// <summary>
        /// Локальное хранилище контактов.
        /// </summary>
        private Project _project = new Project();

        /// <summary>
        /// Загрузка данных из файла.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            _project = ProjectManager.LoadFromFile(_filePath);
            if (_project.Contacts.Count == 0)
            {
                return;
            }
            _project.Contacts = Project.SortContacts(_project.Contacts);
            UpdateContactsList(_project.Contacts);
            BirthDaysContacts();
        }

        /// <summary>
        /// Вывод данных контакта на главную форму.
        /// </summary>
        private void ContactsView(IReadOnlyList<Contact> contactsToView)
        {
            var index = ContactsListBox.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            surnameTextBox.Text = contactsToView[index].Surname;
            nameTextBox.Text = contactsToView[index].Name;
            phoneTextBox.Text = $@"+{contactsToView[index].PhoneNumber.Number}";
            emailTextBox.Text = contactsToView[index].Email;
            idVkTextBox.Text = contactsToView[index].IdVk;
            birthDateBox.Text = contactsToView[index].BirthDate.ToString("dd.MM.yyyy");

        }

        /// <summary>
        /// Добавление контакта.
        /// </summary>
        private void AddContact()
        {
            var newContact = new Contact { PhoneNumber = new PhoneNumber() };
            var addedContactForm = new ContactForm { TempContact = newContact };
            var dialogResult = addedContactForm.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            _project.Contacts.Add(addedContactForm.TempContact);
            ContactsListBox.Items.Add(addedContactForm.TempContact);
            _project.Contacts = Project.SortContacts(_project.Contacts);
            ProjectManager.SaveToFile(_project, _filePath);
            FindContact();
        }
        
        /// <summary>
        /// Редактирование контакта.
        /// </summary>
        private void EditContact()
        {
            if (ContactsListBox.SelectedIndex == -1)
            {
                MessageBox.Show(@"Select the contact.", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var selectedIndex = ContactsListBox.SelectedIndex;
                var selectedContact = _project.Contacts[selectedIndex];

                var updatedContact = new ContactForm { TempContact = selectedContact };
                var dialogResult = updatedContact.ShowDialog();
                if (dialogResult != DialogResult.OK)
                {
                    return;
                }

                _project.Contacts.RemoveAt(selectedIndex);
                ContactsListBox.Items.RemoveAt(selectedIndex);
                _project.Contacts.Insert(selectedIndex, updatedContact.TempContact);
                ContactsListBox.Items.Insert(selectedIndex, updatedContact.TempContact.Surname);
                ContactsListBox.SelectedIndex = selectedIndex;
                _project.Contacts = Project.SortContacts(_project.Contacts);
                ProjectManager.SaveToFile(_project, _filePath);
                UpdateContactsList(_project.Contacts);
            }
        }

        /// <summary>
        /// Удаление контакта.
        /// </summary>
        private void DeleteContact()
        {
            if (ContactsListBox.SelectedIndex == -1)
            {
                MessageBox.Show(@"Select the contact.", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var selectedIndex = ContactsListBox.SelectedIndex;
                var result = MessageBox.Show($@"Do you really want to delete this contact: 
                    {_project.Contacts[selectedIndex].Surname}?", @"Confirmation",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result != DialogResult.OK)
                {
                    return;
                }
                _project.Contacts.RemoveAt(selectedIndex);
                ContactsListBox.Items.RemoveAt(selectedIndex);
                ProjectManager.SaveToFile(_project, _filePath);
                if (ContactsListBox.Items.Count > 0)
                {
                    ContactsListBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Вывод контактов у которых день рождения.
        /// </summary>
        private void BirthDaysContacts()
        {
            var birthDaysContacts = new Project {Contacts = Project.BirthDayContacts(DateTime.Today, _project)};
            if (birthDaysContacts.Contacts.Count == 0)
            {
                return;
            }
            birthContactsLabel.Text = "";
            for (var index = 0; index < birthDaysContacts.Contacts.Count; index++)
            {
                var contact = birthDaysContacts.Contacts[index];
                birthContactsLabel.Text += contact.Surname;
                if (index < birthDaysContacts.Contacts.Count - 1)
                {
                    birthContactsLabel.Text += @", ";
                }
            }
        }

        /// <summary>
        /// Поиск контакта.
        /// </summary>
        private void FindContact()
        {
            UpdateContactsList(Project.SortContacts(findTextBox.Text, _project));
        }

        /// <summary>
        /// Сортировка контактов.
        /// </summary>
        private void UpdateContactsList(IEnumerable<Contact> projectToList)
        {
            ContactsListBox.Items.Clear();
            foreach (var t in projectToList)
            {
                ContactsListBox.Items.Add(t.Surname);
            }
            if (ContactsListBox.Items.Count > 0)
            {
                ContactsListBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Добавление контакта по нажатию кнопки add.
        /// </summary>
        private void AddButton_Click(object sender, EventArgs e)
        {
            AddContact();
        }

        /// <summary>
        /// Редактирование контакта по нажатию кнопки edit.
        /// </summary>
        private void EditButton_Click(object sender, EventArgs e)
        {
            EditContact();
        }

        /// <summary>
        /// Удаление контакта по нажатию кнопки remove.
        /// </summary>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteContact();
        }
        
        /// <summary>
        /// Поиск.
        /// </summary>
        private void FindTextBoxText_Changed(object sender, EventArgs e)
        {
            FindContact();
        }

        /// <summary>
        /// Добавление контакта через меню.
        /// </summary>
        private void AddToolStripMenu_ItemClick(object sender, EventArgs e)
        {
            AddContact();
        }

        /// <summary>
        /// Редактирование контакта через меню.
        /// </summary>
        private void EditContactToolStripMenu_ItemClick(object sender, EventArgs e)
        {
            EditContact();
        }

        /// <summary>
        /// Удаление контакта через меню.
        /// </summary>
        private void RemoveContactToolStripMenu_ItemClick(object sender, EventArgs e)
        {
            DeleteContact();
        }

        /// <summary>
        /// Вызов окна About.
        /// </summary>
        private void AboutToolStripMenu_ItemClick(object sender, EventArgs e)
        {
            ShowAboutForm();
        }

        /// <summary>
        /// Выход через меню.
        /// </summary>
        private void ExitToolStripMenu_ItemClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Вызов окна About по горячей клавише F1.
        /// </summary>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                ShowAboutForm();
            }
        }

        /// <summary>
        /// Вывод данных контакта на главную форму.
        /// </summary>
        private void ContactsListBoxSelected_IndexChanged(object sender, EventArgs e)
        {
            ContactsView(Project.SortContacts(findTextBox.Text, _project));
        }

        /// <summary>
        /// Сохранение при выходе из программы.
        /// </summary>
        private void MainForm_Closed(object sender, FormClosedEventArgs e)
        {
            ProjectManager.SaveToFile(_project, _filePath);
        }
        
        /// <summary>
        /// Вызов окна About.
        /// </summary>
        private static void ShowAboutForm()
        {
            var about = new AboutForm();
            about.ShowDialog();
        }
    }
}
