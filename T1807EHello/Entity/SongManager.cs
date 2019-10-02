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

        Song Upload(Song member);

        ValidateData Validation(Song song);

    }
}
