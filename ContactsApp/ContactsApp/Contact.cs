using System;

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
                if (CheckLength(value, 50))
                {
                    _surname = LettersСase(value);
                }
                else
                {
                    throw new ArgumentException("Длина фамилии не должна превышать 50 символов");
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
                if (CheckLength(value, 50))
                {
                    _name = LettersСase(value);
                }
                else
                {
                    throw new ArgumentException("Длина имени не должна превышать 50 символов");
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
                if (CheckLength(value, 50))
                {
                    _email = value;
                }
                else
                {
                    throw new ArgumentException("Длина е-мейла не должна превышать 50 символов");
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
                if (CheckLength(value, 15))
                {
                    _idVk = value;
                }
                else
                {
                    throw new ArgumentException("Длина ID Вконтакте не должна превышать 15 символов");
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
                if (value < DateTime.Now && value.Year > 1900)
                {
                    _birthDate = value;
                }
                else
                {
                    throw new ArgumentException("Дата рождения не может быть больше текущей даты и не может быть менее 1900 года");
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
            return char.ToUpper(word[0]).ToString() + word.Substring(1);
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
