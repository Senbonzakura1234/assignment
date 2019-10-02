using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace T1807EHello.Entity
{
    internal interface ISongManager
    {
        string Login(string email, string password);

        Song Upload(Song member);

        Song GetInformation(string token);
        ValidateData Validation(Song song);

        StorageFile CreateTokenFile(string token);

        string ReadTokenFile(string fileName);

    }
}
