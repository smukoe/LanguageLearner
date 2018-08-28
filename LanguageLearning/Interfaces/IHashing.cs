using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Interfaces
{
    public interface IHashing
    {
        string GetHash(string password, byte[] salt);
        byte[] GetSalt();
    }
}
