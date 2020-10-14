using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ContactsApp;

namespace ContactsAppUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PhoneNumber phoneNumber = new PhoneNumber
            {
                Number = 79996196907
            };
            Contact contact = new Contact
            {
                Surname = "дагба",
                Name = "кежик",
                PhoneNumber = phoneNumber
            };
            var birthDate = new DateTime(1995, 11, 04);
            contact.BirthDate = birthDate;
            contact.IdVk = "kezhik_dagba";
            contact.Email = "thiswhitenike@gmail.com";
            Project project = new Project();
            project.Contacts.Add(contact);
            project.Contacts.Add(contact);
            ProjectManager projectManager = new ProjectManager();
            projectManager.SaveToFile(project);
            project = projectManager.DeserializeProject();
            surnameTextBox.Text = project.Contacts[0].Email;
        }

    }
}
