﻿using System;

namespace ContactsApp
{
    /// <summary>
    /// Класс с данными контакта и интерфейсом ICloneable.
    /// </summary>
    public class Contact : ICloneable
    {
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
                if (value.Length > 50)
                {
                    throw new ArgumentException("Surname must not exceed 50 characters");
                }

                if (value.Length == 0)
                {
                    throw new ArgumentException("Surname is not entered");
                }
                _surname = LettersRegister(value);
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
                if (value.Length > 50)
                {
                    throw new ArgumentException("Surname must not exceed 50 characters");
                }

                if (value.Length == 0)
                {
                    throw new ArgumentException("Name is not entered");
                }
                _name = LettersRegister(value);
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
                if (value.Length > 50)
                {
                    throw new ArgumentException("e-mail must not exceed 50 characters");
                }
                _email = value;
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
                if (value.Length > 30)
                {
                    throw new ArgumentException("ID_vk must not exceed 30 characters");
                }
                _idVk = value;
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
                if (value >= DateTime.Now || value.Year <= 1900)
                {
                    throw new ArgumentException(@"Date of birth cannot be more than 
the current date and cannot be less than 1900");
                }
                _birthDate = value;
            }
        }

        /// <summary>
        /// Метод изменяющий первую букву в слове на заглавную.
        /// </summary>
        /// <param name="word">Слово которое нужно изменить.</param>
        /// <returns>Возвращает изименённое слово.</returns>
        public static string LettersRegister(string word)
        {
            return char.ToUpper(word[0]) + word.Substring(1);
        }

        /// <summary>
        /// Метод клонирования объекта класса Contact.
        /// </summary>
        /// <returns>Вовзвращает копию класса Contact.</returns>
        public object Clone()
        {
            var phoneNumber = new PhoneNumber { Number = PhoneNumber.Number };
            return new Contact
            {
                Surname = Surname,
                Name = Name,
                BirthDate = BirthDate,
                Email = Email,
                IdVk = IdVk,
                PhoneNumber = phoneNumber
            };
        }

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
        private DateTime _birthDate = DateTime.Today;

        /// <summary>
        /// Возвращает и задаёт e-mail.
        /// </summary>
        private string _email;

        /// <summary>
        /// Возвращает и задаёт ID личной страницы Вконтакте.
        /// </summary>
        private string _idVk;

        /// <summary>
        /// id контакта.
        /// </summary>
        public string IdContact { get; set; }
    }
}
