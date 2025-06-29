using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.Services
{
    public interface IPasswordHasher
    {
        (string hash, string salt) Hash(string plainText);

        bool Verify(string plainText, string hash, string salt);
    }
}
