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
        /// Локальное хранилище контактов.
        /// </summary>
        private Project _project = new Project();

        /// <summary>
        /// Путь до сохранённого файла.
        /// </summary>
        public string FilePath()
        {
            return null;
        }

        /// <summary>
        /// Загрузка данных из файла.
        /// </summary>
        private void MainFormLoad(object sender, EventArgs e)
        {
            _project = ProjectManager.LoadFile(FilePath());
            if (_project.Contacts == null) return;
            SortContacts();
        }

        /// <summary>
        /// Сортировка контактов.
        /// </summary>
        public void SortContacts()
        {
            ContactsListBox.Items.Clear();
            var sortedUsers = from u in _project.Contacts orderby u.Surname select u;
            _project.Contacts = sortedUsers.ToList();
            foreach (var t in _project.Contacts)
            {
                ContactsListBox.Items.Add(t.Surname);
            }
        }

        /// <summary>
        /// Вывод данных контакта на главную форму.
        /// </summary>
        private void ContactsListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ContactsListBox.SelectedIndex < 0 || ContactsListBox.SelectedIndex > ContactsListBox.Items.Count)
                return;
            surnameTextBox.Text = _project.Contacts[ContactsListBox.SelectedIndex].Surname;
            nameTextBox.Text = _project.Contacts[ContactsListBox.SelectedIndex].Name;
            phoneTextBox.Text = $@"+{_project.Contacts[ContactsListBox.SelectedIndex].PhoneNumber.Number}";
            emailTextBox.Text = _project.Contacts[ContactsListBox.SelectedIndex].Email;
            idVkTextBox.Text = _project.Contacts[ContactsListBox.SelectedIndex].IdVk;
            birthDateBox.Text = _project.Contacts[ContactsListBox.SelectedIndex].BirthDate.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Кнопка добавления контакта.
        /// </summary>
        private void AddButtonClick(object sender, EventArgs e)
        {
            if (_project.Contacts.Count > 200)
            {
                MessageBox.Show(@"Maximum number of contacts reached", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
            }
            else
            {
                var newContact = new Project();
                var addedContact = new AddEditContact {TempProject = newContact, Check = false};
                addedContact.ShowDialog();
                if (addedContact.TempProject.Contacts.Count == 0) return;
                _project.Contacts.Add(addedContact.TempProject.Contacts[0]);
                ContactsListBox.Items.Add(addedContact.TempProject.Contacts[0].Surname);
                SortContacts();
                var projectManager = new ProjectManager();
                projectManager.SaveFile(_project, FilePath());
            }
        }

        /// <summary>
        /// Кнопка редактирования контакта.
        /// </summary>
        private void EditButtonClick(object sender, EventArgs e)
        {
            if (ContactsListBox.SelectedIndex == -1)
            {
                MessageBox.Show(@"Select the contact you want to edit.", @"Contact not selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var selectedIndex = ContactsListBox.SelectedIndex;
                var selectedContact = new Project();
                selectedContact.Contacts.Add(_project.Contacts[selectedIndex]);
                var updatedContact = new AddEditContact { TempProject = selectedContact, Check = true};
                updatedContact.ShowDialog();
                if (updatedContact.TempProject.Contacts.Count == 0) return;
                _project.Contacts.RemoveAt(selectedIndex);
                _project.Contacts.Insert(selectedIndex, updatedContact.TempProject.Contacts[0]);
                
                ContactsListBox.Items.RemoveAt(selectedIndex);
                ContactsListBox.Items.Insert(selectedIndex, _project.Contacts[selectedIndex].Surname);
                ContactsListBox.SelectedIndex = selectedIndex;
                SortContacts();
                var projectManager = new ProjectManager();
                projectManager.SaveFile(_project, FilePath());
            }
        }

        /// <summary>
        /// Кнопка удаления контакта.
        /// </summary>
        private void DeleteButtonClick(object sender, EventArgs e)
        {
            if (ContactsListBox.SelectedIndex == -1)
            {
                MessageBox.Show(@"Select the contact you want to delete.", @"Contact not selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var selectedIndex = ContactsListBox.SelectedIndex;
                var result = MessageBox.Show($@"Do you really want to remove this contact: {_project.Contacts[selectedIndex].Surname}",
                    @"Сonfirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result != DialogResult.OK) return;
                _project.Contacts.RemoveAt(selectedIndex);
                ContactsListBox.Items.RemoveAt(selectedIndex);
                var projectManager = new ProjectManager();
                projectManager.SaveFile(_project, FilePath());
            }
        }

        /// <summary>
        /// Добавление контакта через меню.
        /// </summary>
        private void AddToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddButtonClick(sender, e);
        }

        /// <summary>
        /// Редактирование контакта через меню.
        /// </summary>
        private void EditContactToolStripMenuItemClick(object sender, EventArgs e)
        {
            EditButtonClick(sender, e);
        }

        /// <summary>
        /// Удаление контакта через меню.
        /// </summary>
        private void RemoveContactToolStripMenuItemClick(object sender, EventArgs e)
        {
            DeleteButtonClick(sender, e);
        }

        /// <summary>
        /// Вызов окна About.
        /// </summary>
        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }

        /// <summary>
        /// Выход через меню.
        /// </summary>
        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
