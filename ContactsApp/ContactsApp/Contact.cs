using System;
using System.Windows.Forms;

namespace ContactsApp 
{
    /// <summary>
    /// Класс «Контакт» с полями «Фамилия», «Имя», «Номер телефона», «Дата рождения», «e-mail», «ID ВКонтакте».
    /// </summary>
    public class Contact : ICloneable
    {
        /// <summary>
        /// Возвращает и задаёт фамилию контакта.
        /// </summary>
        private string _surname;

        /// <summary>
        /// Возвращает и задаёт имя контакта.
        /// </summary>
        private string _name;

        /// <summary>
        /// Возвращает и задаёт дату рождения контакта.
        /// </summary>
        private DateTime _birthDate;

        /// <summary>
        /// Возвращает и задаёт e-mail контакта.
        /// </summary>
        private string _email;

        /// <summary>
        /// Возвращает и задаёт ID личной страницы Вконтакте контакта.
        /// </summary>
        private string _idVk;

        /// <summary>
        /// Возвращает и задаёт номер телефона контакта.
        /// </summary>
        public PhoneNumber PhoneNumber { get; set; }

        /// <summary>
        /// Свойство фамилии контакта.
        /// Первая буква преобразовывается к верхнему регистру, а также фамилия ограничена 50 символами по длине.
        /// </summary>
        public string Surname
        {
            get => _surname;
            set
            {
                try
                {
                    if (CheckLength(value, 50))
                    {
                        _surname = LettersСase(value);
                    }
                    else
                    {
                        throw new ArgumentException("Error");
                    }
                }
                catch
                {
                    MessageBox.Show("Длинна фамилии не должна превышать 50 символов",
                        "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Свойство имени контакта.
        /// Первая буква преобразовывается к верхнему регистру, а также имя ограничено 50 символами по длине.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                try
                {
                    if (CheckLength(value, 50))
                    {
                        _name = LettersСase(value);
                    }
                    else
                    {
                        throw new ArgumentException("Error");
                    }
                }
                catch
                {
                    MessageBox.Show("Длинна имени не должна превышать 50 символов",
                        "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Свойство е-мейла контакта.
        /// Емейл ограничен 50 символами по длине.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                try
                {
                    if (CheckLength(value, 50))
                    {
                        _email = value;
                    }
                    else
                    {
                        throw new ArgumentException("Error");
                    }
                }
                catch
                {
                    MessageBox.Show("Длинна е-мейла не должна превышать 50 символов",
                        "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Свойство ID Вконтакте контакта.
        /// ID Вконтакте ограничен 15 символами по длине.
        /// </summary>
        public string IdVk
        {
            get => _idVk;
            set
            {
                try
                {
                    if (CheckLength(value, 15))
                    {
                        _idVk = value;
                    }
                    else
                    {
                        throw new ArgumentException("Error");
                    }
                }
                catch
                {
                    MessageBox.Show("Длинна ID Вконтакте не должна превышать 15 символов",
                        "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Свойство даты рождения контакта.
        /// Дата рождения не может быть более текущей даты и не может быть менее 1900 года.
        /// </summary>
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                try
                {
                    if (value < DateTime.Today && value.Year > 1900)
                    {
                        _birthDate = value;
                    }
                    else
                    {
                        throw new ArgumentException("Error");
                    }
                }
                catch
                {
                    MessageBox.Show("Дата рождения не может быть более текущей даты и не может быть менее 1900 года",
                        "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Метод проверяющий слово на определённую длину.
        /// </summary>
        /// <param name="word">Слово которое нужно проверить.</param>
        /// <param name="n">Слово не должно быть длиннее чем n.</param>
        /// <returns>Показывает, что слово меньше n.</returns>
        bool CheckLength(string word, byte n)
        {
            return word.Length < n;
        }

        /// <summary>
        /// Метод изменяющий первую букву в слове на заглавную.
        /// </summary>
        /// <param name="word">Слово которое нужно изменить.</param>
        /// <returns>Возвращает изименённое слово.</returns>
        string LettersСase(string word)
        {
            word = char.ToUpper(word[0]).ToString()+word.Substring(1);
            return word;
        }

        /// <summary>
        /// Метод клонирования объекта класса Contact.
        /// </summary>
        /// <returns>Вовзвращает копию класса Contact.</returns>
        public object Clone()
        {
            var phoneNumber = new PhoneNumber{Number = this.PhoneNumber.Number};
            return new Contact
            {
                Surname = this.Surname,
                Name = this.Name,
                BirthDate = this.BirthDate,
                Email = this.Email,
                IdVk = this.IdVk,
                PhoneNumber = phoneNumber
            };
        }
    }
}
