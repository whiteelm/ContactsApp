using System;
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
    }
}
