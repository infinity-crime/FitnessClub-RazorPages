using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.Services
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// Возвращает кортеж, состоящий из хеша и соли, который получается из пароля типа string.
        /// Используется хеш-функция KeyDerivationPrf.HMACSHA256 с 1000 итераций.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>(string hash, string salt)</returns>
        (string hash, string salt) Hash(string plainText);

        /// <summary>
        /// Сверяет введеный пользоваетелм пароль с его хешем и солью в БД.
        /// Если совпало - true, иначе false.
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="hash"></param>
        /// <param name="salt"></param>
        /// <returns>bool</returns>
        bool Verify(string plainText, string hash, string salt);
    }
}
