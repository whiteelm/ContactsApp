using System;

namespace ContactsApp
{
    /// <summary>
    /// Класс «Контакт» с полями «Фамилия», «Имя», «Номер телефона», «Дата рождения», «e-mail», «ID ВКонтакте».
    /// </summary>
    public class Contact : ICloneable
    {
        /// <summary>
        /// Возвращает и задаёт фамилию.
        /// </summary>
        private string _surname;

        /// <summary>
        /// Возвращает и задаёт имя.
        /// </summary>
        private string _name;

        /// <summary>
        /// Возвращает и задаёт дату рождения.
        /// </summary>
        private DateTime _birthDate;

        /// <summary>
        /// Возвращает и задаёт e-mail.
        /// </summary>
        private string _email;

        /// <summary>
        /// Возвращает и задаёт ID личной страницы Вконтакте.
        /// </summary>
        private string _idVk;

        /// <summary>
        /// Возвращает и задаёт номер телефона.
        /// </summary>
        public PhoneNumber PhoneNumber { get; set; }

        /// <summary>
        /// Свойство фамилии.
        /// Первая буква преобразовывается к верхнему регистру, а также фамилия ограничена 50 символами по длине.
        /// </summary>
        public string Surname
        {
            get => _surname;
            set
            {
                if (CheckLength(value, 50))
                {
                    _surname = LettersСase(value);
                }
                else
                {
                    throw new ArgumentException("Surname must not exceed 50 characters");
                }
            }
        }

        /// <summary>
        /// Свойство имени.
        /// Первая буква преобразовывается к верхнему регистру, а также имя ограничено 50 символами по длине.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (CheckLength(value, 50))
                {
                    _name = LettersСase(value);
                }
                else
                {
                    throw new ArgumentException("Name must not exceed 50 characters");
                }
            }
        }

        /// <summary>
        /// Свойство е-мейла.
        /// Емейл ограничен 50 символами по длине.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                if (CheckLength(value, 50))
                {
                    _email = value;
                }
                else
                {
                    throw new ArgumentException("e-mail must not exceed 50 characters");
                }
            }
        }

        /// <summary>
        /// Свойство ID Вконтакте.
        /// ID Вконтакте ограничен 30 символами по длине.
        /// </summary>
        public string IdVk
        {
            get => _idVk;
            set
            {
                if (CheckLength(value, 30))
                {
                    _idVk = value;
                }
                else
                {
                    throw new ArgumentException("ID_vk must not exceed 50 characters");
                }
            }
        }

        /// <summary>
        /// Свойство даты рождения.
        /// Дата рождения не может быть более текущей даты и не может быть менее 1900 года.
        /// </summary>
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (value < DateTime.Now && value.Year > 1900)
                {
                    _birthDate = value;
                }
                else
                {
                    throw new ArgumentException("Date of birth cannot be more than the current date and cannot be less than 1900");
                }
            }
        }

        /// <summary>
        /// Метод проверяющий слово на определённую длину.
        /// </summary>
        /// <param name="word">Слово которое нужно проверить.</param>
        /// <param name="n">Слово не должно быть длиннее чем n.</param>
        /// <returns>Показывает, что слово меньше n.</returns>
        private bool CheckLength(string word, byte n)
        {
            return word.Length < n;
        }

        /// <summary>
        /// Метод изменяющий первую букву в слове на заглавную.
        /// </summary>
        /// <param name="word">Слово которое нужно изменить.</param>
        /// <returns>Возвращает изименённое слово.</returns>
        private string LettersСase(string word)
        {
            return char.ToUpper(word[0]).ToString() + word.Substring(1);
        }

        /// <summary>
        /// Метод клонирования объекта класса Contact.
        /// </summary>
        /// <returns>Вовзвращает копию класса Contact.</returns>
        public object Clone()
        {
            var phoneNumber = new PhoneNumber { Number = this.PhoneNumber.Number };
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
