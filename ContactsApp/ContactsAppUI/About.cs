using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactsAppUI
{
    /// <summary>
    /// Класс с интерфейсом окна About.
    /// </summary>
    public partial class About : Form
    {

        public About()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Закрытие окна на кнопку.
        /// </summary>
        private void ButtonCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ClickGit(object sender, EventArgs e)
        {
            Process.Start("https://github.com/whiteelm/ContactsApp");
        }

        private async void MailClick(object sender, EventArgs e)
        {
            
            Clipboard.SetText(MailLabel.Text);
            InfoLabel.Visible = true;
            await Task.Delay(4000);
            InfoLabel.Visible = false;
        }
    }
}
