using FitnessClub.Domain.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.ValueObjects
{
    public sealed class FullName
    {
        public string Name { get; }
        public string Surname { get; }
        public string Patronymic { get; }

        private FullName(string name, string surname, string patronymic)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(patronymic))
                throw new DomainException("Surname, name, patronymic cannot be empty!");

            Name = name;
            Surname = surname;
            Patronymic = patronymic;
        }

        public static FullName Create(string name, string surname, string patronymic)
        {
            return new FullName(name, surname, patronymic);
        }

        public override string ToString() => $"{Surname} {Name} {Patronymic}";

        public override bool Equals(object? obj)
        {
            if(obj is FullName other)
            {
                return other.Name == this.Name
                    && other.Surname == this.Surname
                    && other.Patronymic == this.Patronymic;
            }

            return false;
        }

        public override int GetHashCode() => 
            Name.GetHashCode() + Surname.GetHashCode() + Patronymic.GetHashCode();
    }
}
