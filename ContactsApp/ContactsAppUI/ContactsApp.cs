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
            KeyPreview = true;
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
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return path + @"\ContactsApp\Contacts.json";
        }

        /// <summary>
        /// Загрузка данных из файла.
        /// </summary>
        private void MainFormLoad(object sender, EventArgs e)
        {
            _project = ProjectManager.LoadFromFile(FilePath());
            if (_project.Contacts == null) return;
            SortContacts(_project);
        }

        /// <summary>
        /// Сортировка контактов.
        /// </summary>
        public void SortContacts(Project sortProject)
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
                SortContacts(_project);
                ProjectManager.SaveToFile(_project, FilePath());
                if (findTextBox.Text != "")
                {
                    FindTextBoxTextChanged(sender, e);
                }
            }
        }

        /// <summary>
        /// Кнопка редактирования контакта.
        /// </summary>
        private void EditButtonClick(object sender, EventArgs e)
        {
            if (ContactsListBox.SelectedIndex == -1)
            {
                MessageBox.Show(@"Select the contact.", @"Error",
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
                SortContacts(_project);
                ProjectManager.SaveToFile(_project, FilePath());
                if (findTextBox.Text != "")
                {
                    FindTextBoxTextChanged(sender, e);
                }
            }
        }

        /// <summary>
        /// Кнопка удаления контакта.
        /// </summary>
        private void DeleteButtonClick(object sender, EventArgs e)
        {
            if (ContactsListBox.SelectedIndex == -1)
            {
                MessageBox.Show(@"Select the contact.", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var selectedIndex = ContactsListBox.SelectedIndex;
                var result = MessageBox.Show($@"Do you really want to delete this contact: {_project.Contacts[selectedIndex].Surname}",
                    @"Сonfirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result != DialogResult.OK) return;
                _project.Contacts.RemoveAt(selectedIndex);
                ContactsListBox.Items.RemoveAt(selectedIndex);
                ProjectManager.SaveToFile(_project, FilePath());
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

        /// <summary>
        /// Сохранение при выходе из программы.
        /// </summary>
        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            ProjectManager.SaveToFile(_project, FilePath());
        }

        /// <summary>
        /// Поиск через текстбокс.
        /// </summary>
        private void FindTextBoxTextChanged(object sender, EventArgs e)
        {
            var findProject = new Project();
            foreach (var contact in _project.Contacts.Where(contact => contact.Surname.StartsWith(findTextBox.Text, StringComparison.OrdinalIgnoreCase)))
            {
                findProject.Contacts.Add(contact);
            }
            SortContacts(findProject);
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
    }
}
