using System;
using System.Windows.Forms;

namespace ContactsApp
{
    /// <summary>
    /// Класс «Номер телефона» с полем «Номер».
    /// </summary>
    public class PhoneNumber
    {
        /// <summary>
        /// Номер телефона.
        /// </summary>
        private long _number;

        /// <summary>
        /// Свойство номера телефона.
        /// Поле должно быть числовым и содержать ровно 11 цифр. Первая цифра должна быть ‘7’.
        /// </summary>
        public long Number
        {
            get => _number;
            set
            {
                try
                {
                    if ((value.ToString().Length == 11) && (value.ToString()[0] == '7'))
                    {
                        _number = value;
                    }
                    else
                    {
                        throw new ArgumentException(
                            message: "Error");
                    }
                }
                catch
                {
                    MessageBox.Show("Номер телефона должен начинаться с 7 и быть длиной в 11 цифр",
                        "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }   
    }
}