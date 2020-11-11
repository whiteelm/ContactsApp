using System;
using System.Linq;
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
            if (_project.Contacts.Count == 0) return;
            SortContacts(_project);
        }

        /// <summary>
        /// Вывод данных контакта на главную форму.
        /// </summary>
        private void ContactsListBoxSelected_IndexChanged(object sender, EventArgs e)
        {
            var index = ContactsListBox.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            surnameTextBox.Text = _project.Contacts[index].Surname;
            nameTextBox.Text = _project.Contacts[index].Name;
            phoneTextBox.Text = $@"+{_project.Contacts[index].PhoneNumber.Number}";
            emailTextBox.Text = _project.Contacts[index].Email;
            idVkTextBox.Text = _project.Contacts[index].IdVk;
            birthDateBox.Text = _project.Contacts[index].BirthDate.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Добавление контакта по нажатию кнопки add.
        /// </summary>
        private void AddButtonClick(object sender, EventArgs e)
        {
            AddContact();
        }

        /// <summary>
        /// Редактирование контакта по нажатию кнопки edit.
        /// </summary>
        private void EditButtonClick(object sender, EventArgs e)
        {
            EditContact();
        }

        /// <summary>
        /// Удаление контакта по нажатию кнопки remove.
        /// </summary>
        private void DeleteButtonClick(object sender, EventArgs e)
        {
            DeleteContact();
        }


        /// <summary>
        /// Поиск через текстбокс.
        /// </summary>
        private void FindTextBoxTextChanged(object sender, EventArgs e)
        {
            FoundContact();
        }

        /// <summary>
        /// Добавление контакта через меню.
        /// </summary>
        private void AddToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddContact();
        }

        /// <summary>
        /// Редактирование контакта через меню.
        /// </summary>
        private void EditContactToolStripMenuItemClick(object sender, EventArgs e)
        {
            EditContact();
        }

        /// <summary>
        /// Удаление контакта через меню.
        /// </summary>
        private void RemoveContactToolStripMenuItemClick(object sender, EventArgs e)
        {
            DeleteContact();
        }

        /// <summary>
        /// Вызов окна About.
        /// </summary>
        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            ShowAboutForm();
        }

        /// <summary>
        /// Выход через меню.
        /// </summary>
        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
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
                AboutToolStripMenuItemClick(sender, e);
            }
        }

        /// <summary>
        /// Сохранение при выходе из программы.
        /// </summary>
        private void MainForm_Closed(object sender, FormClosedEventArgs e)
        {
            ProjectManager.SaveToFile(_project, _filePath);
        }
        /// <summary>
        /// Добавление контакта.
        /// </summary>
        private void AddContact()
        {
            var newContact = new Contact {PhoneNumber = new PhoneNumber()};
            var addedContactForm = new ContactForm { TempContact = newContact };
            var dialogResult = addedContactForm.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            _project.Contacts.Add(addedContactForm.TempContact);
            ContactsListBox.Items.Add(addedContactForm.TempContact);
            ProjectManager.SaveToFile(_project, _filePath);
            ContactsListBox.SelectedIndex = 0;
            if (findTextBox.Text != "")
            {
                FoundContact();
            }
            else
            {
                SortContacts(_project);
            }
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
                if (dialogResult != DialogResult.Cancel)
                {
                    return;
                }
                _project.Contacts.RemoveAt(selectedIndex);
                ContactsListBox.Items.RemoveAt(selectedIndex);
                _project.Contacts.Insert(selectedIndex, updatedContact.TempContact);
                ContactsListBox.Items.Insert(selectedIndex, _project.Contacts[selectedIndex].Surname);
                ContactsListBox.SelectedIndex = selectedIndex;
                SortContacts(_project);
                ProjectManager.SaveToFile(_project, _filePath);
                if (findTextBox.Text != "")
                {
                    FoundContact();
                }
                else
                {
                    SortContacts(_project);
                }
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
                    {_project.Contacts[selectedIndex].Surname}", @"Confirmation",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result != DialogResult.OK)
                {
                    return;
                }
                _project.Contacts.RemoveAt(selectedIndex);
                ContactsListBox.Items.RemoveAt(selectedIndex);
                ContactsListBox.SelectedIndex = 0;
                ProjectManager.SaveToFile(_project, _filePath);
            }
        }

        /// <summary>
        /// Поиск контакта.
        /// </summary>
        private void FoundContact()
        {
            var foundContact = _project.Contacts.Where(contact =>
                contact.Surname.StartsWith(findTextBox.Text, StringComparison.OrdinalIgnoreCase));
            var findProject = new Project();
            foreach (var contact in foundContact)
            {
                findProject.Contacts.Add(contact);
            }
            SortContacts(findProject);
        }

        /// <summary>
        /// Сортировка контактов.
        /// </summary>
        private void SortContacts(Project sortProject)
        {
            ContactsListBox.Items.Clear();
            var sortedUsers = from u in sortProject.Contacts orderby u.Surname select u;
            sortProject.Contacts = sortedUsers.ToList();
            foreach (var t in sortProject.Contacts)
            {
                ContactsListBox.Items.Add(t.Surname);
            }
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
